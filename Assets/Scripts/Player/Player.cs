using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, ICollision2D
{
    private Coroutine moveForwardCoroutine;
    private Coroutine moveAroundPlanetCoroutine;
    private Rigidbody2D rigidbody;

    [SerializeField] private float flyForce;
    [SerializeField] private InputSystem buttonChange;
    [SerializeField] private float moveAroundDelay;
    [SerializeField] private float degreeStep;
    
    public event Action<Collision2D> CollisionEnter;

    private void Start()
    {
        buttonChange.LeftMouseButtonChange += StartMoveForward;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        buttonChange.LeftMouseButtonChange -= StartMoveForward;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        CollisionEnter?.Invoke(col);
    }

    private void StartMoveForward(bool canFlyToAnotherPlanet)
    {
        if (canFlyToAnotherPlanet && moveAroundPlanetCoroutine != null) 
        {
            StopACoroutine(ref moveAroundPlanetCoroutine);
            moveForwardCoroutine = StartCoroutine(MoveForward());
        }
    }
    
    public void StartMoveAround(Vector3 center)
    {
        StopACoroutine(ref moveForwardCoroutine);
        moveAroundPlanetCoroutine = StartCoroutine(MoveAround(center, GetCircleAngle(center)));
    }
    
    private IEnumerator MoveForward()
    {
        transform.SetParent(null);
        rigidbody.AddRelativeForce(Vector2.up * flyForce, ForceMode2D.Impulse);
        yield break;
    }

    private IEnumerator MoveAround(Vector3 center, float planetAngle)
    {
        var degreeAtRadians = Mathf.Deg2Rad * planetAngle;
        var fromPlayerCenterToPlanetCenter = transform.position - center;
        float radius = Mathf.Sqrt(fromPlayerCenterToPlanetCenter.x * fromPlayerCenterToPlanetCenter.x +
                                  fromPlayerCenterToPlanetCenter.y * fromPlayerCenterToPlanetCenter.y);
        
         while (true)
         {
             rigidbody.position = new Vector2(center.x + radius * Mathf.Cos(degreeAtRadians), center.y + radius * Mathf.Sin(degreeAtRadians));
             rigidbody.rotation = planetAngle - 90f;
             yield return new WaitForSeconds(moveAroundDelay);
             planetAngle += degreeStep;
             if (planetAngle >= 360)
                 planetAngle %= 360;
             degreeAtRadians = Mathf.Deg2Rad * planetAngle;
         }
    }
    
    private float GetCircleAngle(Vector3 circle)
    {
        Vector2 circlePosition = transform.position - circle;
        return Mathf.Atan2(circlePosition.y, circlePosition.x) * Mathf.Rad2Deg;
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