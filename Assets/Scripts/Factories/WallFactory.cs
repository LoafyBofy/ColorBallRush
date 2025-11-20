using UnityEngine;

public class WallFactory : Factory<Wall>
{
    private GameObject _wallPrefab;

    public WallFactory(GameObject wallPrefab)
    {
        _wallPrefab = wallPrefab;
    }

    public override Wall GetItem()
    {
        GameObject newObject = GameObject.Instantiate(_wallPrefab);
        newObject.SetActive(false);
        return newObject.GetComponent<Wall>();
    }
}
