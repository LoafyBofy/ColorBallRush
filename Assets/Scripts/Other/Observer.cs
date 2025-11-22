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

    private Pause _pause;
    private Wallet _wallet;
    private ScoreUpdater _scoreResult;
    private PlayerBall _player;
    private Spawner _spawner;
    private bool _canRevive = true;

    public void Init(PlayerBall player, Spawner spawner)
    {
        _player = player;
        _spawner = spawner;
        _pause = ServiceLocator.GetService(_pause);
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
        // делаем тоже самое что и в Revive, но только сбрасываес до нуля счёт и монетки
        // !!! предварительно записав их в PlayerPrefs

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // не делаем так!
    }

    public void ShowResultPanel()
    {
        _deathPanel.SetActive(false);
        _resultPanel.SetActive(true);

        _scoreResult = ServiceLocator.GetService(_scoreResult);
        _wallet = ServiceLocator.GetService(_wallet);

        _resultScore.text = _scoreResult.Score.ToString();
        _resultCoins.text = _wallet.Coins.ToString();
    }

    public void GiveUp()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
