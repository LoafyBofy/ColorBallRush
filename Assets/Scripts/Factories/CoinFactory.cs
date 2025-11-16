using UnityEngine;

public class CoinFactory : Factory
{
    private GameObject _coinPrefab;

    public CoinFactory(GameObject coinPrefab)
    {
        _coinPrefab = coinPrefab;
    }

    public override GameObject GetItem()
    {
        GameObject coin = GameObject.Instantiate(_coinPrefab);
        coin.gameObject.SetActive(false);
        return coin;
    }
}
