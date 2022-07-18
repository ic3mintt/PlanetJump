using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public event Action<bool> TouchChange;

    private void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                ListenToTouches();
            }
        }
    }
    
    private void ListenToTouches()
    {
        TouchChange?.Invoke(Input.GetMouseButtonDown(0));
    }
}
