using UnityEngine;

public class GamePlayBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerBall _playerBall;
    [SerializeField] private PlatformSpawner _spawner;

    [Header("Prefabs")]
    [SerializeField] private GameObject _coinPrefab;

    private void Awake()
    {
        _playerBall.Init();

        _playerInput.Init(new InputSystem(), _playerBall);

        var coinFactory = new CoinFactory(_coinPrefab);
        var coinsPool = new ObjectPool(coinFactory);
        _spawner.Init(coinsPool);
    }
}
