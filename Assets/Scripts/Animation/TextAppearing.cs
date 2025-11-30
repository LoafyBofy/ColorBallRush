using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class TextAppearing : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _startScale = 1.5f;
    [SerializeField] private float _finalScale = 1f;
    [SerializeField] private float _animationTime = 1f;

    [Header("Event")]
    [SerializeField] private UnityEvent _event;

    private Tween _animation;
    private RectTransform _rect;

    private void OnEnable()
    {
        if (_rect == null) _rect = GetComponent<RectTransform>();

        _animation = _rect
            .DOScale(_finalScale, _animationTime)
            .From(_startScale)
            .SetEase(Ease.OutBounce)
            .OnComplete( () => _event?.Invoke() );
    }

    private void OnDisable()
    {
        _animation.Kill();
    }
}
