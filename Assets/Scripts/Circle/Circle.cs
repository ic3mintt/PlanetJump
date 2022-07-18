using UnityEngine;
using Random = UnityEngine.Random;

public class Circle 
{
    private readonly float minScale;
    private readonly float maxScale;

    public Circle(){}
    
    public Circle(in float minScale, in float maxScale)
    {
        this.minScale = minScale;
        this.maxScale = maxScale;
    }
    
    public Vector3 GetRandomScale()
    {
        if (minScale == 0 && maxScale == 0)
            return Vector3.zero;
        var diameter = Random.Range(minScale, maxScale);
        return new Vector3(diameter, diameter, 0);
    }

    public Vector4 GetColor() => new Vector4(Random.value, Random.value, Random.value, 1);
    
    public Vector3 GetUpCirclePosition(Transform center)
    {
        var downCirclePosition = center.position;
        downCirclePosition.y += center.localScale.y / 2;
        return downCirclePosition;
    }
}