using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Platforms")]
    [SerializeField] private float _distanceForDisable = 140f;
    [SerializeField] private float _spawnOffsetZ = 200f;
    [SerializeField] private float _spawnOffsetY = 30f;
    [SerializeField] private float _spawnOffsetYMin = 0f;
    [SerializeField] private float _spawnOffsetYMax = 0f;
    [SerializeField] private List<Platform> _platforms = new();

    [Header("Spawn Item Amount")]
    [SerializeField, Range(0, 20)] private int _cointSpawnAmountMin;
    [SerializeField, Range(1, 20)] private int _cointSpawnAmountMax;
    [SerializeField, Range(0, 20)] private int _wallSpawnAmountMin;
    [SerializeField, Range(1, 20)] private int _wallSpawnAmountMax;
    [SerializeField, Range(1, 10)] private int _colorChangerSpawnAmountMin;
    [SerializeField, Range(1, 10)] private int _colorChangerSpawnAmountMax;

    private PlayerBall _player;
    private ObjectPool<Coin> _coinsPool;
    private ObjectPool<Wall> _wallsPool;
    private ObjectPool<ColorChanger> _colorChangerPool;

    private void OnValidate()
    {
        if (_cointSpawnAmountMin >= _cointSpawnAmountMax)
            _cointSpawnAmountMin = _cointSpawnAmountMax - 1;

        if (_wallSpawnAmountMin >= _wallSpawnAmountMax)
            _wallSpawnAmountMin = _wallSpawnAmountMax - 1;

        if (_colorChangerSpawnAmountMin >= _colorChangerSpawnAmountMax)
            _colorChangerSpawnAmountMin = _colorChangerSpawnAmountMax - 1;
    }

    public void Init(PlayerBall playerBall, ObjectPool<Coin> coinsPool, ObjectPool<Wall> wallsPool, ObjectPool<ColorChanger> colorChangerPool)
    {
        _player = playerBall;

        _coinsPool = coinsPool;
        _wallsPool = wallsPool;
        _colorChangerPool = colorChangerPool;

        InitPlatforms();
    }

    private void Update()
    {
        if (Vector3.Distance(_platforms.First().transform.position, _player.transform.position) > _distanceForDisable)
        {
            var farther = _platforms.First();
            var last = _platforms.Last();
            _platforms.Remove(farther);
            float offsetY = GetRandomOffsetY();
            if (last.transform.localPosition.y + offsetY >= _spawnOffsetYMax || last.transform.localPosition.y + offsetY <= _spawnOffsetYMin) offsetY = 0;
            farther.transform.position = last.transform.position + new Vector3(0, offsetY, _spawnOffsetZ);
            _platforms.Add(farther);

            farther.ClearSpawnPoints();
            farther.SpawnObjects(_colorChangerPool, Random.Range(_colorChangerSpawnAmountMin, _colorChangerSpawnAmountMax));
            farther.SpawnObjects(_coinsPool, Random.Range(_cointSpawnAmountMin,  _cointSpawnAmountMax));
            farther.SpawnObjects(_wallsPool, Random.Range(_wallSpawnAmountMin,  _wallSpawnAmountMax));
        }
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < _platforms.Count; i++)
        {
            _platforms[i].Init(_coinsPool, _wallsPool, _colorChangerPool);

            if (i > 0)
            {
                _platforms[i].SpawnObjects(_colorChangerPool, Random.Range(_colorChangerSpawnAmountMin, _colorChangerSpawnAmountMax));
                _platforms[i].SpawnObjects(_coinsPool, Random.Range(_cointSpawnAmountMin, _cointSpawnAmountMax));
                _platforms[i].SpawnObjects(_wallsPool, Random.Range(_wallSpawnAmountMin, _wallSpawnAmountMax));
            }
        }
    }

    public Transform GetNearestRespawnPoint()
    {
        return _platforms.First().RespawnPoint;
    }

    private float GetRandomOffsetY()
    {
        int rnd = Random.Range(1, 3);

        switch (rnd)
        {
            case 1: return _spawnOffsetY;
            case 2: return -_spawnOffsetY;
            default: return 0;
        }
    }
}
