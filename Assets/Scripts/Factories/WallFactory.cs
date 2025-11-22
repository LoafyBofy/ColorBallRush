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
        var wall = newObject.GetComponent<Wall>();
        wall.Init();
        newObject.SetActive(false);
        return wall;
    }
}
