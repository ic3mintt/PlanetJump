using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public event Action<bool> LeftMouseButtonChange;

    private void Update()
    {
        ListenToLeftButton();
    }
    
    private void ListenToLeftButton()
    {
        LeftMouseButtonChange?.Invoke(Input.GetMouseButtonDown(0));
    }
}
