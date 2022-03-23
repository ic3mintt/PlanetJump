using System;
using UnityEngine;

public interface ICollision2D
{
    public event Action<Collision2D> CollisionEnter;
}