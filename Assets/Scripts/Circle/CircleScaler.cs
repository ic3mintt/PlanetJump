using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Planet scale", menuName = "Planet scale")]
public class CircleScaler : ScriptableObject
{
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;

    public void SetRandomScale(IEnumerable<Planet> circleList)
    {
        foreach (var circle in circleList)
        {
            var diameter = GetRandomScale();
            circle.transform.localScale = new Vector3(diameter, diameter, 0);
        }
    }

    private float GetRandomScale() => Random.Range(minScale, maxScale);

    public float GetRadius() => maxScale / 2;
}