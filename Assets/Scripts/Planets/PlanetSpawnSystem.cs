using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet spawn system", menuName = "Planet spawn system")]
public class PlanetSpawnSystem : ScriptableObject
{
    private Grid grid;
    private Circle circle;
    private ObjectPool<Planet> pool;
    private PlanetPositionHandler planetsPositionHandler;
    
    [SerializeField] private GameInfo gameInfo;

    public void SpawnPlanetsAtStart()
    {
        circle = new Circle(gameInfo.MinPlanetScale, gameInfo.MaxPlanetScale);
        grid = new Grid( FindObjectOfType<Camera>(), gameInfo.StartPlanetsAmount, gameInfo.PercentXOffsetFromTheLeft);
        pool = new ObjectPool<Planet>(Spawn);
        planetsPositionHandler = new PlanetPositionHandler(gameInfo.MinDistanceBetweenPlanetsSurfaces, grid);
        
        CreatePlanets(gameInfo.StartPlanetsAmount);
    }
    
    public void CreatePlanets(Collision2D other)
    {
        var planetsAmountToSpawn = GetNumberOfPlanetsToSpawn(other.transform.position);
        CreatePlanets(planetsAmountToSpawn);
    }

    private void CreatePlanets(in int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            pool.GetObject();
        }
    }

    private Planet Spawn()
    {
        var planet = Instantiate(gameInfo.PlanetPrefab, gameInfo.StartPlanetPosition, Quaternion.identity).GetComponent<Planet>();
        
        planet.EndLife += pool.ReturnObject;
        planet.OnColorChanged += circle.GetColor;
        planet.OnScaleChanged += circle.GetRandomScale;
        planet.OnPositionChanged += planetsPositionHandler.SetRandomPlanetPosition;
        return planet;
    }

    public List<Planet> GetDisabledPlanets() => pool.DisabledPlanets;

    public List<Planet> GetEnabledPlanetsList() => pool.EnabledPlanets;

    public Grid GetGrid() => grid;
    
    private int GetNumberOfPlanetsToSpawn(in Vector3 playerPosition)
    {
        if (playerPosition.y < planetsPositionHandler.Layout.YSpawnLines[1])
            return 0;

        for (int i = 2; i < planetsPositionHandler.Layout.YSpawnLines.Count; i++)
        {
            if (playerPosition.y < planetsPositionHandler.Layout.YSpawnLines[i])
            {
                return i - 1;
            }
        }

        return 0;
    }
}