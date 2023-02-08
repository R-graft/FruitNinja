using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : IPoolable
{
    private Queue<T> _objectsQueue;

    private Action<T, Vector3> OnGet;

    private Action<T> OnCreate;

    private Func<T> OnGetNewObject;

    private Action<T> OnDisableObject;

    public ObjectPool(Func<T> onGetNewObject, Action<T> onCreate, Action<T,Vector3> onGet, Action<T> OnDisable)
    {
        _objectsQueue = new Queue<T>();

        OnGetNewObject = onGetNewObject;

        OnCreate = onCreate;

        OnGet = onGet;

        OnDisableObject = OnDisable;
    }

    public void CreatePoolObject()
    {
        T newObject = OnGetNewObject();

        Add(newObject);
    }

    public void Add(T addedObject)
    {
        _objectsQueue.Enqueue(addedObject);

        OnCreate(addedObject);
    }
    public T Get(Vector2 pos)
    {
        if (_objectsQueue.Count == 0)
        {
            CreatePoolObject();
        }

        T spawnObject = _objectsQueue.Dequeue();

        OnGet(spawnObject, pos);

        return spawnObject;
    }
    public void Disable(T disabledObject)
    {
        _objectsQueue.Enqueue(disabledObject);

        OnDisableObject(disabledObject);
    }
}
