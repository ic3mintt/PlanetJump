using System.Collections.Generic;
using UnityEngine;

public struct Layout 
{
    public float Up;
    public float Down;
    public float Left;
    public float Right;
    public int ObjectsAmount;
    public List<float> YSpawnLines;
}

public class Grid 
{
    public Layout Layout;
    private float division;
    private readonly Camera camera;
    private readonly float viewportXOffset;
    private List<float> outOfScreenYSpawnLines;

    private Vector3 outOfScreenLeftUp;
    private Vector3 outOfScreenRightUp;
    
    public Grid( in Camera camera, in int amount, in float offset)
    {
        this.camera = camera;
        viewportXOffset = offset;
        Layout = new Layout();
        
        FormLayoutData(amount);
        CalculateSpawnLines();
        AddOutOfScreenYSpawnLines(amount);
    }

    private void FormLayoutData(in int planetAmount)
    {
        Layout.ObjectsAmount = planetAmount;
        Layout.YSpawnLines = new List<float>();
        outOfScreenYSpawnLines = new List<float>();
        Layout.Up = 1f;
        Layout.Left = camera.ViewportToWorldPoint(new Vector3(viewportXOffset,0,0)).x;
        Layout.Right = camera.ViewportToWorldPoint(new Vector3(1f - viewportXOffset,0,0)).x; 
        
        outOfScreenLeftUp = new Vector3(0, 1, 10);
        outOfScreenRightUp = new Vector3(1, 1, 10);
        outOfScreenYSpawnLines.Add(camera.ViewportToWorldPoint(outOfScreenLeftUp).y);
    }

    private void CalculateSpawnLines()
    {
        division = (Layout.Up - Layout.Down) / Layout.ObjectsAmount;
        var spawnLineCoordinate = Layout.Down;
        
        for (int i = 0; i <= Layout.ObjectsAmount; i++, spawnLineCoordinate += division)
        {
            Layout.YSpawnLines.Add(camera.ViewportToWorldPoint(new Vector3(0,spawnLineCoordinate,0)).y);
        }
    }

    private void AddOutOfScreenYSpawnLines(in int amount)
    {
        for (int i = 0; i < amount; i++)
        { 
            outOfScreenLeftUp.y += division;
            outOfScreenRightUp.y += division;
            outOfScreenYSpawnLines.Add(camera.ViewportToWorldPoint(new Vector3(0, outOfScreenLeftUp.y, 10)).y);
        }
    }

    public List<float> GetOutOfScreenYSpawnLines() => outOfScreenYSpawnLines;
}