using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    private Coroutine moveForwardCoroutine;
    private Coroutine moveAroundPlanetCoroutine;

    [SerializeField] private float flyForce;
    [SerializeField] private float degreeStep;
    [SerializeField] private float moveAroundDelay;
    
    public event Action<Collision2D> CollisionEnter;
    
    private void Awake()
    {
        FindObjectOfType<InputSystem>().TouchChange += StartMoveForward;
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D col) => CollisionEnter?.Invoke(col);
    
    #region Moving
    
    private void StartMoveForward(bool canFlyToAnotherPlanet)
    {
        if (canFlyToAnotherPlanet && moveAroundPlanetCoroutine != null) 
        {
            StopACoroutine(ref moveAroundPlanetCoroutine);
            moveForwardCoroutine = StartCoroutine(MoveForward());
        }
    }
    
    public void StartMoveAround(Transform center)
    {
        StopACoroutine(ref moveForwardCoroutine);
        moveAroundPlanetCoroutine = StartCoroutine(MoveAround(center, GetCircleAngle(center.position)));
    }
    
    private IEnumerator MoveForward()
    {
        transform.SetParent(null);
        Rigidbody.velocity = Vector2.zero;
        Rigidbody.AddRelativeForce(Vector2.up * flyForce, ForceMode2D.Impulse);
        yield break;
    }

    private IEnumerator MoveAround(Transform center, float planetAngle)
    {
        var fromPlayerCenterToPlanetCenter = transform.position -  center.position;
        float radius = Mathf.Sqrt(fromPlayerCenterToPlanetCenter.x * fromPlayerCenterToPlanetCenter.x +
                                  fromPlayerCenterToPlanetCenter.y * fromPlayerCenterToPlanetCenter.y);
        
         while (true)
         {
             Rigidbody.position = new Vector2(center.position.x + radius * Mathf.Cos(planetAngle),
                 center.position.y + radius * Mathf.Sin(planetAngle));
             Rigidbody.rotation = planetAngle * Mathf.Rad2Deg - 90f;
             yield return new WaitForSeconds(moveAroundDelay);
             planetAngle += degreeStep;
             if (planetAngle >= 2 * Mathf.PI)
                 planetAngle %= 2 * Mathf.PI;
         }
    }

    #endregion
    
    private float GetCircleAngle(in Vector3 circle)
    {
        Vector2 circlePosition = transform.position - circle;
        return Mathf.Atan2(circlePosition.y, circlePosition.x);
    }
    
    private void StopACoroutine(ref Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}