using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private ScoreUpdater _updater;

    public void Init()
    {
        _updater = ServiceLocator.GetService(_updater);
    }

    public void SetScore(uint score)
    {
        _scoreText.text = score.ToString();
    }

    private void OnEnable()
    {
        _updater.Change += SetScore;
    }

    private void OnDisable()
    {
        _updater.Change -= SetScore;
    }
}
