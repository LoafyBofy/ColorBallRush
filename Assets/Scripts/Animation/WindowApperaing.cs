using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WindowApperaing : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _startScale = 0.7f;
    [SerializeField] private float _finalScale = 1f;
    [SerializeField] private float _animationTime = 0.3f;

    private Tween _scaleAnimation;
    private RectTransform _rect;

    private void OnEnable()
    {
        if (_rect == null) _rect = GetComponent<RectTransform>();

        _scaleAnimation = _rect
            .DOScale(_finalScale, _animationTime)
            .From(_startScale)
            .SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _scaleAnimation.Kill();
    }
}
