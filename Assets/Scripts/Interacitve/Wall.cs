using System;
using UnityEngine;

public class Wall : MonoBehaviour, IColor
{
    [SerializeField] private ColorsConfig _config;

    public Color CurrentColor { get; set; }

    private Renderer _renderer;

    public void Init()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColorToRandom()
    {
        CurrentColor = _config.GetRandomColor();
        _renderer.material.color = CurrentColor;
    }

    private void OnEnable()
    {
        if (_renderer == null)
            Init();

        ChangeColorToRandom();
    }
}
