using UnityEngine;

[CreateAssetMenu(fileName = "Spawner",menuName = "Spawner")]
public class GameObjectHandler : ScriptableObject
{
    private PlanetManager planetManager;
    private PlayerManager playerManager;
    public Player Player { get; private set; }
    
    [SerializeField] private Player playerPrefab;
    [SerializeField] private PlanetManager manager;
    [SerializeField] private PlanetSpawnSystem planetSpawnSystem;
    [SerializeField] private Vector3 startPlayerPosition;
    
    public void SpawnGameObjects()
    {
        Player = Instantiate(playerPrefab, startPlayerPosition, Quaternion.identity);
        planetManager = Instantiate(manager);
        playerManager = new PlayerManager(Player);
        
        planetSpawnSystem.SpawnPlanetsAtStart();
        planetManager.AtStart();
        playerManager.AttachPlayerToPlanet(planetSpawnSystem.GetEnabledPlanetsList()[0].transform);
    }

    public void Destroy()
    {
        Destroy(Player.gameObject);
        Destroy(planetManager.gameObject);
        DestroyPlanets();
    }

    private void DestroyPlanets()
    {
        foreach (var planet in planetSpawnSystem.GetEnabledPlanetsList())
        {
            Destroy(planet.gameObject);
        }
        
        foreach (var planet in planetSpawnSystem.GetDisabledPlanets())
        {
            Destroy(planet.gameObject);
        }
    }
}
