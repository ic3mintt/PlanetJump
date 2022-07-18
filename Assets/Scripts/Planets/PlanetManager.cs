using System.Collections;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private Player player;
    private Camera camera;
    private float lineToMove;
    private Coroutine moveToCoroutine;
    private readonly Circle circle = new Circle();
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float checkPlanetsPositionDelay;
    [SerializeField] private PlanetSpawnSystem planetSpawnSystem;
    
    public void AtStart()
    {
        camera = FindObjectOfType<Camera>();
        player = FindObjectOfType<Player>();
        
        player.CollisionEnter += CreateAndMovePlanets;
        
        var ySpawnLines = planetSpawnSystem.GetGrid().Layout.YSpawnLines;
        lineToMove = ySpawnLines[0] + (ySpawnLines[1] - ySpawnLines[0])/2;
        StartCoroutine(CheckPlanets());
    }
    
    private void CreateAndMovePlanets(Collision2D other)
    {
        planetSpawnSystem.CreatePlanets(other);
        
        if (moveToCoroutine != null)
        {
            StopCoroutine(moveToCoroutine);
            moveToCoroutine = null;
            StopPlanets();
        }
        
        var direction = player.transform.position.y <= lineToMove ? Vector2.up : Vector2.down;
        var planets = planetSpawnSystem.GetEnabledPlanetsList();
        
        if (planets[1].Rigidbody.velocity == Vector2.zero)
        {
             for (int i = 0; i < planets.Count; i++)
            {
                planets[i].Rigidbody.velocity = direction * moveSpeed;
            }
        }
        
        moveToCoroutine = StartCoroutine(MoveTo(other.transform));
    }

    private IEnumerator MoveTo(Transform planetCenter)
    {
        while (true)
        {
            if (planetCenter.position.y <= lineToMove)
            {
                StopPlanets();
                yield break;
            }
            yield return new WaitForSeconds(checkPlanetsPositionDelay);
        }   
    }

    private IEnumerator CheckPlanets()
    {
        while (true)
        {
            var planets = planetSpawnSystem.GetEnabledPlanetsList();
            for (int i = 0; i < planets.Count; i++)
            {
                if (camera.WorldToViewportPoint(circle.GetUpCirclePosition(planets[i].transform)).y < 0)
                {
                    planets[i].EndLifeInvoke();
                }
            }
            yield return new WaitForSeconds(checkPlanetsPositionDelay);   
        }
    } 
    
    private void StopPlanets()
    {
        foreach (var planet in planetSpawnSystem.GetEnabledPlanetsList())
        {
            planet.Rigidbody.velocity = Vector2.zero;
        }
    }
}
