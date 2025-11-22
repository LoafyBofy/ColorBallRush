using System;
using UnityEngine;

public class Wall : MonoBehaviour, IColor, IInteractable
{
    [SerializeField] private ColorsConfig _config;
    [SerializeField] private bool _alwaysDestoy = false;

    public Color CurrentColor { get; set; }

    private SfxController _sfx;
    private Renderer _renderer;

    public void Init()
    {
        _sfx = ServiceLocator.GetService(_sfx);
    }

    public void Interact(Action callback = null)
    {
        callback?.Invoke();
        _sfx.Explosion();
        gameObject.SetActive(false);
    }

    public void ChangeColorToRandom()
    {
        CurrentColor = _config.GetRandomColor();
        _renderer.material.color = CurrentColor;
    }

    private void OnEnable()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();

        if (_alwaysDestoy)
        {
            _renderer.material.color = Color.black;
            return;
        }
            
        ChangeColorToRandom();
    }
}
