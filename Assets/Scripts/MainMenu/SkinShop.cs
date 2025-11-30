using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkinShop : MonoBehaviour
{
    [SerializeField] private List<SkinInfo> _skins = new();
    [SerializeField] private int _price = 5000;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _skinName;
    [SerializeField] private Image _skinImage;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _buyButtonText;
    [SerializeField] private TextMeshProUGUI _coinAmount;


    private int _coins = 0;
    private int _index = 0;
    private DataSaver _saver;

    public void Init(DataSaver saver)
    {
        _saver = saver;

        SetPriceText(_price);
        LoadSkinInfo();
        LoadCoinsAmount();
    }

    public void NextSkin()
    {
        if (_index + 1 < _skins.Count)
        {
            _index++;
            LoadSkinInfo();
        }
    }

    public void PrevSkin()
    {
        if (_index - 1 >= 0)
        {
            _index--;
            LoadSkinInfo();
        }
    }

    private void LoadSkinInfo()
    {
        _skinName.text = _skins[_index].Name;
        _skinImage.sprite = _skins[_index].ScreenShot;
        _buyButton.onClick.RemoveAllListeners();

        string activeSkinName = _saver.GetActiveSkin();

        if (_skins[_index].Name == activeSkinName)
        {
            _buyButtonText.text = "Используется";
            _buyButton.interactable = false;
            _priceText.gameObject.SetActive(false);
        }
        else if (_skins[_index].GetAccess(_saver))
        {
            _buyButtonText.text = "Открыто";
            _buyButton.interactable = true;
            _priceText.gameObject.SetActive(false);
            _buyButton.onClick.AddListener
                (() =>
                {
                    _saver.SetActiveSkin(_skins[_index].Name);
                    _buyButtonText.text = "Используется";
                    _buyButton.interactable = false;
                }
                );
        }
        else
        {
            _buyButtonText.text = "Купить";
            _buyButton.interactable = true;
            _buyButton.onClick.AddListener(BuySelectedSkin);
            _priceText.gameObject.SetActive(true);
        }
    }

    private void BuySelectedSkin()
    {
        if (_coins >= _price)
        {
            if (_skins[_index].GetAccess(_saver) == false)
            {
                _skins[_index].Buy(_saver);
                _coins -= _price;
                SetCoinsAmount();
                _saver.SetCoins(_coins);
                _buyButton.interactable = false;
                _buyButtonText.text = "Используется";
                _priceText.gameObject.SetActive(false);
            }
        }
    }

    private void LoadCoinsAmount()
    {
        int loadedCoin = _saver.GetCoins();
        if (loadedCoin == -1) _coins = 0;
        else _coins = loadedCoin;
        SetCoinsAmount();
    }

    public void SetCoinsAmount()
    {
        _coinAmount.text = _coins.ToString();
    }

    private void SetPriceText(int newPrice)
    {
        _priceText.text = $"Цена: <color=yellow>{newPrice}</color>";
    }
}
