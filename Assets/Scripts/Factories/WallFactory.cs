using UnityEngine;

public class WallFactory : Factory
{
    private GameObject _wallPrefab;

    public WallFactory(GameObject wallPrefab)
    {
        _wallPrefab = wallPrefab;
    }

    public override GameObject GetItem()
    {
        GameObject newObject = GameObject.Instantiate(_wallPrefab);
        newObject.SetActive(false);
        return newObject;
    }
}
