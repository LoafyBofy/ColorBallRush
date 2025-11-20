using UnityEngine;
using System.Collections.Generic;
using System;

public class ObjectPool<T> where T : MonoBehaviour
{
    public event Action<GameObject> Disabled;

    private List<T> _pool = new();
    private Factory<T> _factory;

    public ObjectPool(Factory<T> factory)
    {
        _factory = factory;
    }

    public GameObject Create(bool addInPool = false)
    {
        var newObject = _factory.GetItem();

        if (addInPool)
            Add(newObject);

        return newObject.gameObject;
    }

    public void Add(T obj)
    {
        _pool.Add(obj);
    }

    public GameObject GetFree()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            var obj = _pool[i].gameObject;

            if (obj.activeSelf == false)
            {
                _pool.Remove(_pool[i]);
                return obj;
            }
        }

        return Create();
    }

    public void Destroy(T obj)
    {
        _pool.Remove(obj);
        GameObject.Destroy(obj);
    }
}
