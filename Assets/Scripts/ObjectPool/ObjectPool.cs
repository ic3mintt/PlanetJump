using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{
    public readonly List<T> DisabledPlanets;
    public List<T> EnabledPlanets { get; private set; }
    private readonly Func<T> createObject;
    
    public ObjectPool(Func<T> createFunction)
    {
        DisabledPlanets = new List<T>();
        EnabledPlanets = new List<T>();
        createObject = createFunction;
    }

    public T GetObject()
    {
        T poolObject;
        if (DisabledPlanets.Count > 0)
        {
            poolObject = DisabledPlanets[0];
            DisabledPlanets.Remove(poolObject);
        }
        else
        {
            poolObject = CreateObject();
        }
        EnabledPlanets.Add(poolObject);
        poolObject.Show();
        return poolObject;
    }

    public void ReturnObject(T returnObject)
    {
        DisabledPlanets.Add(returnObject);
        EnabledPlanets.Remove(returnObject);
        returnObject.Hide();
    }

    private T CreateObject() => createObject.Invoke();
}