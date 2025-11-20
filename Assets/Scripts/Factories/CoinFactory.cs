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
        GameObject coin = GameObject.Instantiate(_coinPrefab);
        coin.gameObject.SetActive(false);
        return coin.GetComponent<Coin>();
    }
}
