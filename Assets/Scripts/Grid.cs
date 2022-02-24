using System.Collections.Generic;
using UnityEngine;

//is everything ok with the structure?
public struct Layout 
{
    public float Up;
    public float Down;
    public float Left;
    public float Right;
    public float Middle;
    public int ObjectsAmount;
    public List<float> YSpawnLines;
}

[CreateAssetMenu(fileName = "Grid", menuName = "Grid")]
public class Grid : ScriptableObject
{
    private Camera camera;
    public Layout Layout;
    private float leftOffset = 0.1f;
    
    [Range(0.01f,0.15f)] //Viewport
    [SerializeField] private float viewportYOffsetPoint;
    [SerializeField] private CircleScaler circleScaler;
    
    public void Spawn(int amount)
    {
        Layout = new Layout();
        camera = FindObjectOfType<Camera>();
        leftOffset = WorldScaleToViewportXOffset(circleScaler.GetRadius());
        MakeGrid(amount);
    }
    
    private void MakeGrid(int amount)
    {
        FormLayoutData(amount);
        CalculateSides();
        CalculateSpawnLines();
    }

    private void FormLayoutData(int planetAmount)
    {
        Layout.ObjectsAmount = planetAmount;
        Layout.YSpawnLines = new List<float>();
        Layout.Up = 1f;
        Layout.Right = 1f;
        Layout.Middle = 0.5f;
    }
    
    private void CalculateSides()
    {
        Layout.Left += leftOffset;
        Layout.Right -= leftOffset;   
    }

    private void CalculateSpawnLines()
    {
        var upOffset = WorldScaleToViewportUpOffset(circleScaler.GetRadius());
        
        var division = (Layout.Up - Layout.Down) / Layout.ObjectsAmount;
        var spawnLineCoordinate = Layout.Down;
        
        Layout.YSpawnLines.Add(spawnLineCoordinate);
        spawnLineCoordinate += division;
        
        for (int i = 1; i < Layout.ObjectsAmount; i++, spawnLineCoordinate += division)
        {
            Layout.YSpawnLines.Add(spawnLineCoordinate - viewportYOffsetPoint);
            Layout.YSpawnLines.Add(spawnLineCoordinate + viewportYOffsetPoint);
        }

        Layout.YSpawnLines.Add(spawnLineCoordinate - upOffset);
    }

    private float WorldScaleToViewportXOffset(float worldOffset)
    {
        var startCameraXPos = camera.ViewportToWorldPoint(Vector3.zero).x;
        return camera.WorldToViewportPoint(new Vector3(startCameraXPos + worldOffset, 0, 0)).x;
    }
    
    private float WorldScaleToViewportUpOffset(float scale)
    {
        scale += 0.1f; 
        var startCameraYPos = camera.ViewportToWorldPoint(Vector3.zero).y;
        var result = camera.WorldToViewportPoint(new Vector3(0,startCameraYPos + scale,0)).y;
        return result;
    }
}