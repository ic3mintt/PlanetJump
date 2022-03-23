using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

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
    
    [Range(0.01f,0.15f)]
    [SerializeField] private float viewportXOffset;
    
    public void Spawn(int amount)
    {
        Layout = new Layout();
        camera = FindObjectOfType<Camera>();
        MakeGrid(amount);
    }
    
    private void MakeGrid(int amount)
    {
        FormLayoutData(amount);
        CalculateSpawnLines();
    }

    private void FormLayoutData(int planetAmount)
    {
        Layout.ObjectsAmount = planetAmount;
        Layout.YSpawnLines = new List<float>();
        Layout.Up = 1f;
        Layout.Left = viewportXOffset;
        Layout.Right = 1f - viewportXOffset; 
        Layout.Middle = 0.5f;
    }

    private void CalculateSpawnLines()
    {
        var leftDown = camera.ViewportToWorldPoint(new Vector3(Layout.Left, 0, 10));
        var leftUp = camera.ViewportToWorldPoint(new Vector3(Layout.Left, 1, 10));
        var rightDown = camera.ViewportToWorldPoint(new Vector3(Layout.Right, 0, 10));
        var rightUp = camera.ViewportToWorldPoint(new Vector3(Layout.Right, 1, 10));
        Debug.DrawLine(leftDown, leftUp, Color.red,20f);
        Debug.DrawLine(rightDown, rightUp, Color.red,20f);
        
        var division = (Layout.Up - Layout.Down) / Layout.ObjectsAmount;
        var spawnLineCoordinate = Layout.Down;
        
        for (int i = 0; i <= Layout.ObjectsAmount; i++, spawnLineCoordinate += division)
        {
            Layout.YSpawnLines.Add(spawnLineCoordinate);
        
            var b = camera.ViewportToWorldPoint(new Vector3(0, spawnLineCoordinate, 10));
            var c = camera.ViewportToWorldPoint(new Vector3(1, spawnLineCoordinate, 10));
            Debug.DrawLine(b, c, Color.red,20f);
        }
    }
}