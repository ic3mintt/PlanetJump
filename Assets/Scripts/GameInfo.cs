using UnityEngine;

[CreateAssetMenu(fileName = "Game Info", menuName = "Game Info")]
public class GameInfo : ScriptableObject
{
    public float MinPlanetScale;
    public float MaxPlanetScale;
    public int StartPlanetsAmount;

    public Planet PlanetPrefab;
    public Vector3 StartPlanetPosition;
    [Range(0.01f,0.2f)]
    public float PercentXOffsetFromTheLeft;
    [Range(0.5f,2f)] 
    public float MinDistanceBetweenPlanetsSurfaces;
}