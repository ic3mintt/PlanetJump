using UnityEngine;

public class Circle
{
    public Vector3 GetUpPosition(Transform circle)
    {
        var upCirclePosition = circle.position;
        upCirclePosition.y += circle.localScale.y / 2;
        return upCirclePosition;
    }

    public Vector3 GetDownPosition(Transform circle)
    {
        var downCirclePosition = circle.position;
        downCirclePosition.y -= circle.localScale.y / 2;
        return downCirclePosition;
    }
    
    public Vector3 GetLeftPosition(Transform circle)
    {
        var leftCirclePosition = circle.position;
        leftCirclePosition.x -= circle.localScale.x / 2;
        return leftCirclePosition;
    }

    public Vector3 GetRightPosition(Transform circle)
    {
        var leftCirclePosition = circle.position;
        leftCirclePosition.x += circle.localScale.x / 2;
        return leftCirclePosition;
    }
}