using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private DataSaver _saver;

    public void Init(DataSaver saver)
    {
        _saver = saver;
        int bestScore = _saver.GetScore();
        if (bestScore == -1) bestScore = 0;
        SetText(bestScore);
    }

    private void SetText(int score)
    {
        _scoreText.text = $"Лучший результат:\n {score}";
    }
}
