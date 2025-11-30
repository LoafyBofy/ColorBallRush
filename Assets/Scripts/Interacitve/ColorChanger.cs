using System;
using UnityEngine;

public class ColorChanger : MonoBehaviour, IInteractable
{
    [SerializeField] private int _scoreForPickUp = 100;
    
    private SfxController _sfx;
    private ScoreUpdater _scoreUpdater;

    public void Init()
    {
        _sfx = ServiceLocator.GetService(_sfx);
        _scoreUpdater = ServiceLocator.GetService(_scoreUpdater);
    }

    public void Interact(Action callback = null)
    {
        callback?.Invoke();
        _scoreUpdater.AddScore(_scoreForPickUp);
        PickUp();
        gameObject.SetActive(false);
    }

    private void PickUp()
    {
        _sfx.PickUpBonus();
    }
}
