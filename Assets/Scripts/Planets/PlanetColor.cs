using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color", menuName = "Planet color")]
public class PlanetColor : ScriptableObject
{
    public void ChangeColors(IEnumerable<Planet> planets)
    {
        foreach (var planet in planets)
        {
            planet.GetComponent<SpriteRenderer>().color = GetColor();
            Debug.Log("I'ma color");
        }
    }
    
    private Vector4 GetColor()
    {
        return new Vector4(Random.value, Random.value, Random.value, 1);
    }
}
