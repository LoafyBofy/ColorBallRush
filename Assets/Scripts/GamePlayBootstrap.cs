using UnityEngine;

public class GamePlayBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerBall _playerBall;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private CoinsView _coinsView;
    [SerializeField] private ScoreUpdater _updater;

    [Header("Prefabs")]
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _wallPrefab;

    private void Awake()
    {
        ServiceLocator.RegisterService(new Wallet());

        _updater.Init();

        _scoreView.Init();

        _coinsView.Init();

        _playerBall.Init();

        _playerInput.Init(new InputSystem(), _playerBall);

        var coinFactory = new CoinFactory(_coinPrefab);
        var wallFactory = new WallFactory(_wallPrefab);
        var coinsPool = new ObjectPool<Coin>(coinFactory);
        var wallsPool = new ObjectPool<Wall>(wallFactory);
        _spawner.Init(_playerBall, coinsPool, wallsPool);

        _startScreen.Init(_playerBall);

        
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterAll();
    }
}
