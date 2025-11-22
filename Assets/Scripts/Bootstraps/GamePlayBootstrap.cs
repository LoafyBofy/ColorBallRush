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
    [SerializeField] private SpeedIncreaser _speedIncreaser;
    [SerializeField] private FogMovable _fogMovable;
    [SerializeField] private Observer _observer;
    [SerializeField] private Pause _pause;
    [SerializeField] private MusicController _musicController;
    [SerializeField] private SfxController _sfxController;

    [Header("Prefabs")]
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _colorChangerPrefab;

    private DataSaver _dataSaver;

    private void Awake()
    {
        _dataSaver = new();

        ServiceLocator.RegisterService(_dataSaver);
        ServiceLocator.RegisterService(new Wallet());

        _sfxController.Init(_dataSaver);

        _pause.Init();

        _updater.Init();

        _scoreView.Init();

        _coinsView.Init();

        _playerBall.Init();

        _playerInput.Init(new InputSystem(), _playerBall);

        var coinFactory = new CoinFactory(_coinPrefab);
        var wallFactory = new WallFactory(_wallPrefab);
        var colorChangerFactory = new ColorChangerFactory(_colorChangerPrefab);
        var coinsPool = new ObjectPool<Coin>(coinFactory);
        var wallsPool = new ObjectPool<Wall>(wallFactory);
        var colorChangerPool = new ObjectPool<ColorChanger>(colorChangerFactory);
        _spawner.Init(_playerBall, coinsPool, wallsPool, colorChangerPool);

        _speedIncreaser.Init();

        _startScreen.Init(_playerBall);

        _fogMovable.Init(_playerBall);

        _observer.Init(_playerBall, _spawner);

        _musicController.Init(_dataSaver);

    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterAll();
    }
}
