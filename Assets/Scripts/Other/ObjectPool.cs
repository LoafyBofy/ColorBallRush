using UnityEngine;
using System.Collections.Generic;

public class ObjectPool
{
    private List<GameObject> _pool = new();
    private Factory _factory;

    public ObjectPool(Factory factory, int prepareAmount = 0)
    {
        _factory = factory;

        if (prepareAmount > 0)
            PrepareObjects(prepareAmount);
    }

    public GameObject Create()
    {
        var newObject = _factory.GetItem();
        Add(newObject);
        return newObject;
    }

    public void Add(GameObject obj)
    {
        _pool.Add(obj);
    }

    public GameObject GetFree()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i].activeSelf == false)
            {
                return _pool[i];
            }
        }

        return Create();
    }

    public void Destroy(GameObject obj)
    {
        _pool.Remove(obj);
        GameObject.Destroy(obj);
    }

    private void PrepareObjects(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Add(_factory.GetItem());
        }
    }
}
