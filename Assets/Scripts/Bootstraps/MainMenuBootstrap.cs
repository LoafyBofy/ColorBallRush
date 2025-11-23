using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private MusicController _musicController;
    [SerializeField] private SfxController _sfxController;
    [SerializeField] private SceneShop _sceneShop;

    [Space]
    [SerializeField] private SceneInfo _standartScene;

    private DataSaver _dataSaver;

    private void Awake()
    {
        _dataSaver = new DataSaver();

        LoadBaseParams();

        _musicController.Init(_dataSaver);
        _sfxController.Init(_dataSaver);
        _sceneShop.Init(_dataSaver);
    }

    private void LoadBaseParams()
    {
        _dataSaver.SetSceneAccess(_standartScene.Name, true);
        string activeScene = _dataSaver.GetActiveScene();
        if (string.IsNullOrEmpty(activeScene))
        {
            _dataSaver.SetActiveScene(_standartScene.Name);
        }
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterAll();
    }
}
