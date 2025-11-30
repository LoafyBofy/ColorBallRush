using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private float _textFinalScale = 1f;
    [SerializeField] private float _textStartScale = 1.1f;
    [SerializeField] private float _animationTime = 0.3f;

    private ScoreUpdater _updater;

    public void Init()
    {
        _updater = ServiceLocator.GetService(_updater);
    }

    public void SetScore(uint score)
    {
        _scoreText.text = score.ToString();
    }

    public void AddScore(uint score)
    {
        _scoreText.text = score.ToString();

        _scoreText.transform
            .DOScale(_textFinalScale, _animationTime)
            .From(_textStartScale)
            .SetEase(Ease.OutBounce);
    }

    private void OnEnable()
    {
        _updater.Change += SetScore;
        _updater.Added += AddScore;
    }

    private void OnDisable()
    {
        _updater.Change -= SetScore;
        _updater.Added -= AddScore;
    }
}
