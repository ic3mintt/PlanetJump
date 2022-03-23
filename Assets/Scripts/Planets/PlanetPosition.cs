using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Planet position", menuName = "Planet data")]
public class PlanetPosition : ScriptableObject
{
    private Layout layout;
    private Camera camera;
    private List<Planet> planets;
    private float minDistance;
    
    [SerializeField] private Grid grid;
    [Range(0.5f,1f)] //at units 
    [SerializeField] private float minDistanceBetweenPlanetsSurfaces;
    [SerializeField] private CircleScaler circleScaler;
    
    public void AtStart(int planetAmount)
    {
        camera = FindObjectOfType<Camera>();
        grid.Spawn(planetAmount);
        layout = grid.Layout;
        minDistance = camera.ViewportToWorldPoint(new Vector3(minDistanceBetweenPlanetsSurfaces, 0, 0)).x;
    }
    
    public Vector3 GetRandomPlanetPosition(int planetNumber)
    {
        float x, y;
        if (planetNumber == 0)
        {
            x = 0.5f;
            y = layout.YSpawnLines[0] + (layout.YSpawnLines[1] - layout.YSpawnLines[0]) / 2;
        }
        else
        {
            x = Random.Range(layout.Left, layout.Right);
            y = Random.Range(layout.YSpawnLines[planetNumber], layout.YSpawnLines[planetNumber + 1]);
        }
        return camera.ViewportToWorldPoint(new Vector3(x, y, 10));
    }
    
    public void CorrectPlanetsPosition(ref List<Planet> planets)
    {
        this.planets = planets;
        for (int i = 1; i < planets.Count; i++)
        {
            CorrectDistanceBetweenPlanets(i);
        }
    }

    private void CorrectDistanceBetweenPlanets(int currentPlanet)
    {
        if (GetDistanceBetweenPlanetsSurfaces(currentPlanet - 1, currentPlanet) < minDistance)
        {
            planets[currentPlanet].transform.position = GetRandomPlanetPosition(currentPlanet);
            planets[currentPlanet].transform.localScale =
                circleScaler.GetCorrectedScale(planets[currentPlanet].transform.localScale);
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
        return distanceBetweenCenters - previous.localScale.x / 2 - current.localScale.x / 2;;
    }
}