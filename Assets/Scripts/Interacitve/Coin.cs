using DG.Tweening;
using System;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    [SerializeField] private int _amount;
    [SerializeField] private float _animationSpeed = 1f;
    [SerializeField] private float _upPoint = 0.7f;
    [SerializeField] private float _downPoint = -0.2f;
    [SerializeField] private float _rotationDuration = 1f;
    [SerializeField] private Transform _model;

    private Tween _moveTween;
    private Tween _rotationTween;
    private SfxController _sfx;

    public void Init()
    {
        _sfx = ServiceLocator.GetService(_sfx);
    }

    public void Interact(Action callback = null)
    {
        PickUp();
        callback?.Invoke();
        gameObject.SetActive(false);
    }

    private void PickUp()
    {
        _sfx.PickUpCoin();
        // выбрасывать текст над моделькой
    }

    private void OnEnable()
    {
        _moveTween = _model
            .DOLocalMoveY(_upPoint, _animationSpeed)
            .From(_downPoint)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);

        _rotationTween = _model
            .DORotate(new Vector3(0, 360, 0), _rotationDuration, RotateMode.FastBeyond360)
            .From(new Vector3(0, 0, 0)) 
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear); 
    }

    private void OnDisable()
    {
        _moveTween.Kill();
        _rotationTween.Kill();
    }
}

