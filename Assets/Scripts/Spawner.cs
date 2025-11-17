using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Platforms")]
    [SerializeField] private float _distanceForDisable = 140f;
    [SerializeField] private float _spawnOffset = 200f;
    [SerializeField] private List<Platform> _platforms = new();

    [Header("Player")]
    [SerializeField] private float _respawnTime = 2f; // временное решение

    [Header("Spawn Item Amount")]
    [SerializeField, Range(0, 20)] private int _cointSpawnAmountMin;
    [SerializeField, Range(1, 20)] private int _cointSpawnAmountMax;
    [SerializeField, Range(0, 20)] private int _wallSpawnAmountMin;
    [SerializeField, Range(1, 20)] private int _wallSpawnAmountMax;

    private PlayerBall _player;
    private ObjectPool _coinsPool;
    private ObjectPool _wallsPool;

    private void OnValidate()
    {
        if (_cointSpawnAmountMin >= _cointSpawnAmountMax)
            _cointSpawnAmountMin = _cointSpawnAmountMax - 1;

        if (_wallSpawnAmountMin >= _wallSpawnAmountMax)
            _wallSpawnAmountMin = _wallSpawnAmountMax - 1;
    }

    public void Init(PlayerBall playerBall, ObjectPool coinsPool, ObjectPool wallsPool)
    {
        _player = playerBall;
        _coinsPool = coinsPool;
        _wallsPool = wallsPool;

        InitPlatforms();
    }

    private void Update()
    {
        if (Vector3.Distance(_platforms.First().transform.position, _player.transform.position) > _distanceForDisable)
        {
            var farther = _platforms.First();
            var last = _platforms.Last();
            _platforms.Remove(farther);
            farther.transform.position = last.transform.position + new Vector3(0, 0, _spawnOffset);
            _platforms.Add(farther);
            farther.SetDictionaryDefaultValues();
            farther.DisableSpawnedObjects();
            farther.SpawnObjects(_coinsPool, Random.Range(_cointSpawnAmountMin,  _cointSpawnAmountMax));
            farther.SpawnObjects(_wallsPool, Random.Range(_wallSpawnAmountMin,  _wallSpawnAmountMax));
        }
    }

    private void OnEnable()
    {
        _player.Died += PlayerDisabled;
    }

    private void OnDisable()
    {
        _player.Died -= PlayerDisabled;
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < _platforms.Count; i++)
        {
            _platforms[i].Init();

            if (i > 0)
            {
                _platforms[i].SpawnObjects(_coinsPool, Random.Range(_cointSpawnAmountMin, _cointSpawnAmountMax));
                _platforms[i].SpawnObjects(_wallsPool, Random.Range(_wallSpawnAmountMin, _wallSpawnAmountMax));
            }
        }
    }

    public void PlayerDisabled()
    {
        StartCoroutine(ActivatePlayer());
    }

    private IEnumerator ActivatePlayer()
    {
        yield return new WaitForSeconds(_respawnTime);

        var respawnPoint = _platforms.First().RespawnPoint;

        _player.transform.position = respawnPoint.transform.position;

        _player.gameObject.SetActive(true);
    }
}
