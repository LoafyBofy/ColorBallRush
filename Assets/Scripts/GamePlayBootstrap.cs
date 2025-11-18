using UnityEngine;

public class GamePlayBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerBall _playerBall;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private StartScreen _startScreen;

    [Header("Prefabs")]
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _wallPrefab;

    private void Awake()
    {
        _playerBall.Init();

        _playerInput.Init(new InputSystem(), _playerBall);

        var coinFactory = new CoinFactory(_coinPrefab);
        var wallFactory = new WallFactory(_wallPrefab);
        var coinsPool = new ObjectPool(coinFactory);
        var wallsPool = new ObjectPool(wallFactory);
        _spawner.Init(_playerBall, coinsPool, wallsPool);

        _startScreen.Init(_playerBall);
    }
}
