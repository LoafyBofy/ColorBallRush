using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image _blackScreenObject;
    [SerializeField] private float _blackScreenFadeDuraction;

    private Tween _blackScreenTween;

    public void Play()
    {
        _blackScreenObject.gameObject.SetActive(true);
        _blackScreenTween = _blackScreenObject
            .DOFade(1, _blackScreenFadeDuraction)
            .From(0)
            .OnComplete(LoadGameScene);
    }

    private void LoadGameScene()
    {
        _blackScreenTween.Kill();
        SceneManager.LoadSceneAsync("GameScene");
    }
}
