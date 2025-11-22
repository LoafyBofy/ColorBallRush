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

    private ObjectPool<Coin> _coinsPool;
    private ObjectPool<Wall> _wallsPool;
    private ObjectPool<ColorChanger> _colorChangerPool;

    public void Init(ObjectPool<Coin> coinsPool, ObjectPool<Wall> wallsPool, ObjectPool<ColorChanger> changer)
    {
        _coinsPool = coinsPool;
        _wallsPool = wallsPool;
        _colorChangerPool = changer;

        InitDictionary();
    }

    public void SpawnObjects<T>(ObjectPool<T> pool, int amount) where T : MonoBehaviour
    {
        for (int i = 0; i < amount; i++)
        {
            var emptyPoints = _usedPoints.Where(x => x.Value == false).ToList();
            if (emptyPoints.Count == 0) return;
            GameObject objectInPool = pool.GetFree();
            _spawnedObjects.Add(objectInPool);
            var randomTransformKey = emptyPoints.GetRandom();
            _usedPoints[randomTransformKey.Key] = true;
            objectInPool.SetActive(true);
            objectInPool.transform.position = randomTransformKey.Key.transform.position;
        }
    }

    public void ClearSpawnPoints()
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            _usedPoints[_keys[i]] = false;
        }

        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            _spawnedObjects[i].SetActive(false);

            if (_spawnedObjects[i].TryGetComponent(out Coin coin))
            {
                _coinsPool.Add(coin);
            }
            else if (_spawnedObjects[i].TryGetComponent(out Wall wall))
            {
                _wallsPool.Add(wall);
            }
            else if (_spawnedObjects[i].TryGetComponent(out ColorChanger changer))
            {
                _colorChangerPool.Add(changer);
            }
        }

        _spawnedObjects.Clear();
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
