using System;
using UnityEngine;
using DG.Tweening;

public class Wall : MonoBehaviour, IColor, IInteractable
{
    [SerializeField] private int _scoreForDectroy = 50;
    [SerializeField] private ColorsConfig _config;
    [SerializeField] private bool _alwaysDestoy = false;
    [SerializeField] private float _animationTime = 1f;

    public Color CurrentColor { get; set; }

    private Tween _animation;
    private Vector3 _startScale;
    private SfxController _sfx;
    private Renderer _renderer;
    private ScoreUpdater _scoreUpdater;

    public void Init()
    {
        _startScale = transform.localScale;
        _sfx = ServiceLocator.GetService(_sfx);
        _scoreUpdater = ServiceLocator.GetService(_scoreUpdater);
    }

    public void Interact(Action callback = null)
    {
        callback?.Invoke();
        _scoreUpdater.AddScore(_scoreForDectroy);
        _sfx.Explosion();
        DisableAnimation();
    }

    public void ChangeColorToRandom()
    {
        CurrentColor = _config.GetRandomColor();
        _renderer.material.color = CurrentColor;
    }

    private void DisableAnimation()
    {
        _animation = transform
            .DOScale(0, _animationTime)
            .From(transform.localScale)
            .SetEase(Ease.Linear)
            .OnComplete(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();

        if (_alwaysDestoy)
        {
            _renderer.enabled = false;
            return;
        }
        
        
        ChangeColorToRandom();
    }

    private void OnDisable()
    {
        _animation.Kill();
        transform.localScale = _startScale;
    }
}
