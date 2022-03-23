using System.Collections.Generic;
using UnityEngine;

public class PlanetColor
{
    public void ChangeColors(IEnumerable<Planet> planets)
    {
        foreach (var planet in planets)
        {
            planet.GetComponent<SpriteRenderer>().color = GetColor();
        }
    }
    
    private Vector4 GetColor()
    {
        return new Vector4(Random.value, Random.value, Random.value, 1);
    }
}
