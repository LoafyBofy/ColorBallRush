using UnityEngine;
using UnityEngine.UI;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private MusicController _musicController;
    [SerializeField] private SfxController _sfxController;

    private DataSaver _dataSaver;

    private void Awake()
    {
        _dataSaver = new DataSaver();

        _musicController.Init(_dataSaver);
        _sfxController.Init(_dataSaver);
    }
}
