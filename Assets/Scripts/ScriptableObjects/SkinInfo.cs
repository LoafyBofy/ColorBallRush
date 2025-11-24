using UnityEngine;

[CreateAssetMenu(fileName = "SkinInfo", menuName = "Scriptable Objects/SkinInfo")]
public class SkinInfo : ScriptableObject
{
    public string Name;
    public Material Material;
    public Sprite ScreenShot;

    private bool _isOpen = false;

    public void Buy(DataSaver saver)
    {
        if (_isOpen == false)
        {
            saver.SetSkinAccess(Name, true);
            saver.SetActiveSkin(Name);
        }
    }

    public bool GetAccess(DataSaver saver)
    {
        int acess = saver.GetSkinAccess(Name);
        if (acess == -1) return false;
        else return true;
    }
}