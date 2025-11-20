using TMPro;
using UnityEngine;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsAmountText;

    private Wallet _collector;

    public void Init()
    {
        _collector = ServiceLocator.GetService(_collector);
    }

    public void SetCoins(uint amount)
    {
        _coinsAmountText.text = amount.ToString();
    }

    private void OnEnable()
    {
        _collector.Change += SetCoins;
    }

    private void OnDisable()
    {
        _collector.Change -= SetCoins;
    }
}
