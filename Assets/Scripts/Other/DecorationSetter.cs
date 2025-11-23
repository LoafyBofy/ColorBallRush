using UnityEngine;
using System.Collections.Generic;

public class DecorationSetter : MonoBehaviour
{
    [SerializeField] private Renderer _floorRenderer;
    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private List<SceneInfo> _scenes = new();

    private SceneInfo _sceneInfo;
    private Spawner _spawner;
    private DataSaver _saver;

    public void Init(Spawner spawner)
    {
        _spawner = spawner;
        _saver = ServiceLocator.GetService(_saver);

        string activeScene = _saver.GetActiveScene();

        foreach (var scene in _scenes)
        {
            if (scene.Name == activeScene)
            {
                _sceneInfo = scene;
                break;
            }
        }

        SetPlatform();
        //SetPlayerMaterial();
        SetFloorMaterial();
    }

    private void SetPlatform()
    {
        _spawner.PlatformPrefab = _sceneInfo.PlatformPrefab;
    }

    private void SetPlayerMaterial()
    {
        // тут пока ничего нет, но обязательно будет :)
    }

    private void SetFloorMaterial()
    {
        _floorRenderer.material = _sceneInfo.FloorMaterial;
    }

    private void SetDecorationsOnFloor()
    {
        // тут пока ничего нет, но обязательно будет :)
    }
}
