using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints = new();

    [field: SerializeField] public Transform RespawnPoint { get; private set; }

    private Dictionary<Transform, bool> _usedPoints = new();
    private List<GameObject> _spawnedObjects = new();
    private List<Transform> _keys = new();

    public void Init()
    {
        InitDictionary();
    }

    public void SpawnObjects(ObjectPool pool, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var emptyPoints = _usedPoints.Where(x => x.Value == false).ToList();
            if (emptyPoints.Count == 0) return;
            var objectInPool = pool.GetFree();
            if (_spawnedObjects.Contains(objectInPool) == false)
                _spawnedObjects.Add(objectInPool);
            var randomTransformKey = emptyPoints.GetRandom();
            _usedPoints[randomTransformKey.Key] = true;
            objectInPool.SetActive(true);
            objectInPool.transform.position = randomTransformKey.Key.transform.position;
        }
    }

    public void SetDictionaryDefaultValues()
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            _usedPoints[_keys[i]] = false;
        }
    }

    public void DisableSpawnedObjects()
    {
        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            _spawnedObjects[i].SetActive(false);
        }
    }

    private void InitDictionary()
    {
        foreach (Transform t in _spawnPoints)
        {
            _usedPoints.Add(t, false);
        }

        _keys = _usedPoints.Keys.ToList();
    }
}
