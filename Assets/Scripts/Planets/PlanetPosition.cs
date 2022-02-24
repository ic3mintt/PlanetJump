using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//in ViewportPoint coordinates
[CreateAssetMenu(fileName = "Planet position", menuName = "Planet data")]
public class PlanetPosition : ScriptableObject
{
    private Layout layout;
    private Camera camera;
    private List<Planet> planets;
    
    [SerializeField] private Grid grid;
    [Range(0.5f,1f)] //at units 
    [SerializeField] private float minDistanceBetweenPlanetsSurfaces;

    public void AtStart(int planetAmount)
    {
        camera = FindObjectOfType<Camera>();
        grid.Spawn(planetAmount);
        layout = grid.Layout;
    }
    
    public Vector3 GetRandomPlanetPosition(int planetNumber)
    {
        var spawnLineNumber = planetNumber * 2;
        if (planetNumber == 0)
        {
            return camera.ViewportToWorldPoint(new Vector3(layout.Middle,
                (layout.YSpawnLines[1] - layout.YSpawnLines[0]) / 2, 10));
        }
        else
        {
            return camera.ViewportToWorldPoint(new Vector3(Random.Range(layout.Left, layout.Right),
                Random.Range(layout.YSpawnLines[spawnLineNumber], layout.YSpawnLines[spawnLineNumber + 1]), 10));
        }
    }
    
    public void CorrectPlanetsPosition(ref List<Planet> planets)
    {
        this.planets = planets;
        for (int i = 1; i < planets.Count; i++)
        {
            Debug.Log($"I'm here at {i + 1} time");
            CorrectDistanceBetweenPlanets(i);
        }
    }

    private void CorrectDistanceBetweenPlanets(int currentPlanet)
    {
        if (GetDistanceBetweenPlanetsSurfaces(currentPlanet - 1, currentPlanet) < minDistanceBetweenPlanetsSurfaces)
        {
            planets[currentPlanet].transform.position = GetRandomPlanetPosition(currentPlanet);
            CorrectDistanceBetweenPlanets(currentPlanet);
        }
    }
    
    private float GetDistanceBetweenPlanetsSurfaces(int previousID, int currentID)
    {
        var previous = planets[previousID].transform;
        var current = planets[currentID].transform;
        var x = Mathf.Abs(current.position.x - previous.position.x);
        var y = Mathf.Abs(current.position.y - previous.position.y);
        var distanceBetweenCenters = Mathf.Sqrt(x * x + y * y);
        return distanceBetweenCenters - Mathf.Abs(previous.localScale.x / 2) - Mathf.Abs(current.localScale.x / 2);;
    }
}