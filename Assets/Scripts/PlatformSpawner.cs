using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _distanceForDisable = 15f;
    [SerializeField] private float _spawnOffset = 100f;
    [SerializeField] private List<Platform> _platforms = new();

    [Header("Spawn Item Amount")]
    [SerializeField, Range(0, 30)] private int _cointSpawnAmountMin;
    [SerializeField, Range(1, 30)] private int _cointSpawnAmountMax;

    private ObjectPool _coinsPool;

    private void OnValidate()
    {
        if (_cointSpawnAmountMin >= _cointSpawnAmountMax)
            _cointSpawnAmountMin = _cointSpawnAmountMax - 1;
    }

    public void Init(ObjectPool coinsPool)
    {
        _coinsPool = coinsPool;

        InitPlatforms();
    }

    private void Update()
    {
        if (Vector3.Distance(_platforms.First().transform.position, _player.position) > _distanceForDisable)
        {
            var farther = _platforms.First();
            var last = _platforms.Last();
            _platforms.Remove(farther);
            farther.transform.position = last.transform.position + new Vector3(0, 0, _spawnOffset);
            _platforms.Add(farther);
            farther.SetDictionaryDefaultValues();
            farther.DisableSpawnedObjects();
            farther.SpawnObjects(_coinsPool, Random.Range(_cointSpawnAmountMin,  _cointSpawnAmountMax));
        }
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < _platforms.Count; i++)
        {
            _platforms[i].Init();

            if (i > 0)
            {
                _platforms[i].SpawnObjects(_coinsPool, Random.Range(_cointSpawnAmountMin, _cointSpawnAmountMax));
            }
        }
    }
}
