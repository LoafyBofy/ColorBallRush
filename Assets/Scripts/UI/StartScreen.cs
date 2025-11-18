using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartScreen : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _maxTextSize = 1.1f;
    [SerializeField] private float _textAnimationTime = 1f;
    [SerializeField] private float _blackScreenDisappearingTime = 0.8f;
    [SerializeField] private float _playerChangeColorDelay = 0.3f;
    [SerializeField] private float _playerAppearingTimes = 1f;
    [SerializeField] private float _playerFinalScale = 1f;
    [SerializeField] private int _playerChangeColorTimes = 5;

    [Header("UI")]
    [SerializeField] private Button _startButtonPanel;
    [SerializeField] private RectTransform _pressAnuButtonTextTransform;
    [SerializeField] private Image _blackScreenPanel;

    private PlayerBall _player;
    private Tween _textTween;
    private Tween _blackScreenTween;
    private Tween _playerAppearingTween;

    public void Init(PlayerBall player)
    {
        _player = player;

        SetListenerForButton();
        AnimateText();
        HideBlackPanel();
    }

    private void SetListenerForButton()
    {
        _startButtonPanel.onClick.AddListener
            ( () =>
                {
                    AnimatePlayer();
                    _startButtonPanel.gameObject.SetActive(false);
                    _pressAnuButtonTextTransform.gameObject.SetActive(false);
                }
            );
    }

    private void AnimateText()
    {
        _textTween = _pressAnuButtonTextTransform
            .DOScale(_maxTextSize, _textAnimationTime)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void HideBlackPanel()
    {
        _blackScreenTween = _blackScreenPanel
            .DOFade(0, _blackScreenDisappearingTime)
            .From(1)
            .OnComplete(DisableBlackScreen);
    }

    private void DisableBlackScreen() 
    {
        _blackScreenPanel.gameObject.SetActive(false);
    }

    private void AnimatePlayer()
    {
        _playerAppearingTween = _player.transform
            .DOScale(_playerFinalScale, _playerAppearingTimes)
            .From(0)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => { StartCoroutine(ActivatePlayer()); });
    }

    private IEnumerator ActivatePlayer()
    {
        for (int i = 0; i < _playerChangeColorTimes; i++)
        {
            _player.ChangeColorToRandom();
            yield return new WaitForSeconds(_playerChangeColorDelay);
        }

        _player.IsPaused = false;
        _player.CanMove = true;
        _player.UseGravity = true;

        _playerAppearingTween.Kill();
        _textTween.Kill();
        _blackScreenTween.Kill();

        gameObject.SetActive(false);
    }
}
