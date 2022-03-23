using System;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private List<Planet> planets;
    private ObjectPool<Planet> pool;
    private int currentPlayerPlanet;
    private PlanetColor planetColor;

    [SerializeField] private InputSystem input;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int startPlanetsAmount;
    [SerializeField] private PlanetPosition planetsPosition;
    [SerializeField] private CircleScaler circlesScaler;
    
    public event Action AttachToPlanetAtStart;

    private void Awake()
    {
        //input.LeftMouseButtonChange += SpawnPlanets;
    }

    private void Start()
    {
        planets = new List<Planet>();
        planetColor = new PlanetColor();
        pool = new ObjectPool<Planet>(Spawn);
        CreateStartPlanets();
        
        AttachToPlanetAtStart?.Invoke();
    }
    
    private void SpawnPlanets(bool allowedToSpawn)
    {
        if (allowedToSpawn)
        {
            var planet = GetPlanetFromPool();
        }
    }

    private Planet Spawn()
    {
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
            var planet = GetPlanetFromPool();
            planet.transform.position = planetsPosition.GetRandomPlanetPosition(i);
        }
    }
    
    private Planet GetPlanetFromPool() => pool.GetObject();
    
    public Planet GetPlanetFromList() => planets[currentPlayerPlanet++];
}
