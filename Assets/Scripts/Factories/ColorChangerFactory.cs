using UnityEngine;

public class ColorChangerFactory : Factory<ColorChanger>
{
    private GameObject _prefab;

    public ColorChangerFactory(GameObject prefab)
    {
        _prefab = prefab;
    }

    public override ColorChanger GetItem()
    {
        GameObject obj = GameObject.Instantiate(_prefab);
        var changer = obj.GetComponent<ColorChanger>();
        changer.Init();
        obj.SetActive(false);
        return changer;
    }
}
