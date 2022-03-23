using System;
using UnityEngine;

public class Planet : MonoBehaviour, IPoolable
{
    public event Action<Planet> EndLife;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}