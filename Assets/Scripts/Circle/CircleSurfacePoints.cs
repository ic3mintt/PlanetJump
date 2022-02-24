using UnityEngine;

public class CircleSurfacePoints
{
    public Vector3 GetUpCirclePosition(Transform circle)
    {
        var upCirclePosition = circle.position;
        upCirclePosition.y += circle.localScale.y / 2;
        return upCirclePosition;
    }

    public Vector3 GetDownCirclePosition(Transform circle)
    {
        var downCirclePosition = circle.position;
        downCirclePosition.y -= circle.localScale.y / 2;
        return downCirclePosition;
    }
    
    public Vector3 GetLeftCirclePosition(Transform circle)
    {
        var leftCirclePosition = circle.position;
        leftCirclePosition.x -= circle.localScale.x / 2;
        return leftCirclePosition;
    }

    public Vector3 GetRightCirclePosition(Transform circle)
    {
        var leftCirclePosition = circle.position;
        leftCirclePosition.x += circle.localScale.x / 2;
        return leftCirclePosition;
    }
}