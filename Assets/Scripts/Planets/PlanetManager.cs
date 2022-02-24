using System.Collections.Generic;
using UnityEngine;

//main task is spawning planets
public class PlanetManager : MonoBehaviour
{
    private List<Planet> planets;
    private ObjectPool<Planet> pool;

    [SerializeField] private InputSystem input;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int startPlanetsAmount;
    [SerializeField] private PlanetPosition planetsPosition;
    [SerializeField] private CircleScaler circlesScaler;
    [SerializeField] private PlanetColor planetColor;
    
    private void Start()
    {
        planets = new List<Planet>();
        pool = new ObjectPool<Planet>(Spawn);
        input.LeftMouseButtonChange += SpawnPlanets;
        CreateStartPlanets();
    }
    
    private void SpawnPlanets(bool allowedToSpawn)
    {
        if (allowedToSpawn)
        {
            var planet = pool.GetObject();
        }
    }

    private Planet Spawn()
    {
        //Instantiate at some position
        var planet = Instantiate(prefab).GetComponent<Planet>();
        planets.Add(planet);
        planet.EndLife += pool.ReturnObject;
        return planet;
    }

    private void CreateStartPlanets()
    {
        planetsPosition.AtStart(startPlanetsAmount);
        SpawnStartPlanets(startPlanetsAmount);
        circlesScaler.SetRandomScale(planets);
        planetsPosition.CorrectPlanetsPosition(ref planets);
        planetColor.ChangeColors(planets);
    }
    
    private void SpawnStartPlanets(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var planet = pool.GetObject();
            planet.transform.position = planetsPosition.GetRandomPlanetPosition(i);
        }
    }
    
    public void MovePlanets()
    {
        
    }
}
