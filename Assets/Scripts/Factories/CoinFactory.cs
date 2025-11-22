using UnityEngine;

public class CoinFactory : Factory<Coin>
{
    private GameObject _coinPrefab;

    public CoinFactory(GameObject coinPrefab)
    {
        _coinPrefab = coinPrefab;
    }

    public override Coin GetItem()
    {
        GameObject obj = GameObject.Instantiate(_coinPrefab);
        var coin = obj.GetComponent<Coin>();
        coin.Init();
        coin.gameObject.SetActive(false);
        return coin;
    }
}
