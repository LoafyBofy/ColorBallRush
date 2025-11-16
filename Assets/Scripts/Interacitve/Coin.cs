using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    [SerializeField] private int _amount;

    public void Interact()
    {
        PickUp();
        Disable();
    }

    private void PickUp()
    {
        Debug.Log("Вы подобрали монетку: " +  _amount);
    }

    private void Disable() => gameObject.SetActive(false);
}

