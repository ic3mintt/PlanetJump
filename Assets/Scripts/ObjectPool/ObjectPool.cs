using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{
    private Queue<T> pool;
    private Func<T> createObject;
    public int Count
    {
        get { return pool.Count;}
    }
    
    public ObjectPool(Func<T> createFunction)
    {
        pool = new Queue<T>();
        createObject = createFunction;
    }

    public T GetObject()
    {
        var poolObject = pool.Count > 0 ? pool.Dequeue() : CreateObject();
        poolObject.Show();
        return poolObject;
    }

    public void ReturnObject(T returnObject)
    {
        returnObject.Hide();
        pool.Enqueue(returnObject);
    }

    private T CreateObject() => createObject.Invoke();
}