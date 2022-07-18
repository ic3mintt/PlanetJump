using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Planet : MonoBehaviour, IPoolable
{
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D Rigidbody;
    
    public event Action<Planet> EndLife;
    public event Func<Vector4> OnColorChanged;
    public event Func<Vector3> OnScaleChanged;
    public event Action<Planet> OnPositionChanged;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        var scale = OnScaleChanged?.Invoke();
        if (scale != Vector3.zero)
            transform.localScale = scale.Value;
        var color = OnColorChanged?.Invoke();
        if (color != Vector4.zero)
            spriteRenderer.color = color.Value;
        OnPositionChanged?.Invoke(this);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void EndLifeInvoke() => EndLife?.Invoke(this);
}