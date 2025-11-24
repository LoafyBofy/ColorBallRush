using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SceneShop : MonoBehaviour
{
    [SerializeField] private List<SceneInfo> _scenes = new();
    [SerializeField] private int _price = 5000;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _sceneName;
    [SerializeField] private Image _sceneImage;
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
        LoadSceneInfo();
        LoadCoinsAmount();
    }

    public void NextScene()
    {
        if (_index + 1 < _scenes.Count)
        {
            _index++;
            LoadSceneInfo();
        }
    }

    public void PrevScene()
    {
        if (_index - 1 >= 0)
        {
            _index--;
            LoadSceneInfo();
        }
    }

    private void LoadSceneInfo()
    {
        _sceneName.text = _scenes[_index].Name;
        _sceneImage.sprite = _scenes[_index].LevelScreenShot;
        _buyButton.onClick.RemoveAllListeners();

        string activeSceneName = _saver.GetActiveScene();

        if (_scenes[_index].Name == activeSceneName)
        {
            _buyButtonText.text = "Используется";
            _buyButton.interactable = false;
            _priceText.gameObject.SetActive(false);
        }
        else if (_scenes[_index].GetAccess(_saver))
        {
            _buyButtonText.text = "Открыто";
            _buyButton.interactable = true;
            _priceText.gameObject.SetActive(false);
            _buyButton.onClick.AddListener
                ( () =>
                    {
                        _saver.SetActiveScene(_scenes[_index].Name);
                        _buyButtonText.text = "Используется";
                        _buyButton.interactable = false;
                    }
                );
        }
        else
        {
            _buyButtonText.text = "Купить";
            _buyButton.interactable = true;
            _buyButton.onClick.AddListener(BuySelectedScene);
            _priceText.gameObject.SetActive(true);
        }
    }

    private void BuySelectedScene()
    {
        if (_coins >= _price)
        {
            if (_scenes[_index].GetAccess(_saver) == false)
            {
                _scenes[_index].Buy(_saver);
                _coins -= _price;
                _coinAmount.text = _coins.ToString();
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
        _coinAmount.text = _coins.ToString();
    }

    private void SetPriceText(int newPrice)
    {
        _priceText.text = $"Цена: <color=yellow>{newPrice}</color>";
    }
}
