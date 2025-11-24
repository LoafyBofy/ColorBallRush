using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Scriptable Objects/SceneInfo")]
public class SceneInfo : ScriptableObject
{
    public string Name;
    public GameObject PlatformPrefab;
    public Material FloorMaterial;
    public bool CanFloorTextureMove = false;
    public Sprite LevelScreenShot;
    public List<GameObject> Decorations = new();

    private bool _isOpen = false;

    public void Buy(DataSaver saver)
    {
        if (_isOpen == false)
        {
            saver.SetSceneAccess(Name, true);
            saver.SetActiveScene(Name);
        }
    }

    public bool GetAccess(DataSaver saver)
    {
        int acess = saver.GetSceneAccess(Name);
        if (acess == -1) return false;
        else return true;
    }
}