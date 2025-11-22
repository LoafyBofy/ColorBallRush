using System;
using UnityEngine;

public class ColorChanger : MonoBehaviour, IInteractable
{
    private SfxController _sfx;

    public void Init()
    {
        _sfx = ServiceLocator.GetService(_sfx);
    }

    public void Interact(Action callback = null)
    {
        callback?.Invoke();
        PickUp();
        gameObject.SetActive(false);
    }

    private void PickUp()
    {
        _sfx.PickUpBonus();
    }
}
