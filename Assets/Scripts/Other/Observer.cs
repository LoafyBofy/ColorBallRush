using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour
{
    [Header("Death Panel")]
    [SerializeField] private GameObject _deathPanel;

    [Header("Result Panel")]
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI _resultScore;
    [SerializeField] private TextMeshProUGUI _resultCoins;

    private DataSaver _saver;
    private Pause _pause;
    private Wallet _wallet;
    private ScoreUpdater _currentScore;
    private PlayerBall _player;
    private Spawner _spawner;
    private bool _canRevive = true;

    public void Init(PlayerBall player, Spawner spawner)
    {
        _player = player;
        _spawner = spawner;
        _pause = ServiceLocator.GetService(_pause);
        _saver = ServiceLocator.GetService(_saver);
        _currentScore = ServiceLocator.GetService(_currentScore);
        _wallet = ServiceLocator.GetService(_wallet);
    }

    private void OnEnable()
    {
        _player.Died += PlayerDied;
    }

    private void OnDisable()
    {
        _player.Died -= PlayerDied;
    }

    public void PlayerDied()
    {
        _pause.SetPauseState(true);
        _deathPanel.SetActive(true);
        _pause.SetPauseState(true);
    }

    public void Revive()
    {
        if (_canRevive == false) return;

        _player.gameObject.SetActive(true);
        var respawnPoint = _spawner.GetNearestRespawnPoint();
        _player.transform.position = respawnPoint.position;
        _pause.SetPauseState(false);
        _canRevive = false;

        _deathPanel.SetActive(false);
    }

    public void Restart()
    {
        SaveProgress();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // не делаем так!
    }

    public void ShowResultPanel()
    {
        SaveProgress();

        _resultScore.text = _currentScore.Score.ToString();
        _resultCoins.text = _wallet.Coins.ToString();

        _deathPanel.SetActive(false);
        _resultPanel.SetActive(true);
    }

    private void SaveProgress()
    {
        int bestResult = _saver.GetScore();
        if (bestResult < (int)_currentScore.Score)
            _saver.SetScore((int)_currentScore.Score);

        int currentCoinsAmount = _saver.GetCoins();
        _saver.SetCoins(currentCoinsAmount + (int)_wallet.Coins);
    }

    public void GiveUp()
    {
        SaveProgress();

        SceneManager.LoadScene("MainMenu");
    }
}
