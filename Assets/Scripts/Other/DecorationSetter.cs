using UnityEngine;
using System.Collections.Generic;

public class DecorationSetter : MonoBehaviour
{
    [SerializeField] private MovableFloor _movableFloor;
    [SerializeField] private Renderer _floorRenderer;
    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private List<SceneInfo> _scenes = new();
    [SerializeField] private List<SkinInfo> _skins = new();

    private SkinInfo _skinInfo;
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

        string activeSkin = _saver.GetActiveSkin();

        foreach (var skin in _skins)
        {
            if (skin.Name == activeSkin)
            {
                _skinInfo = skin;
                break;
            }
        }

        SetPlatform();
        SetPlayerMaterial();
        SetFloorMaterial();
        SetDecorationsOnFloor();
    }

    private void SetPlatform()
    {
        _spawner.PlatformPrefab = _sceneInfo.PlatformPrefab;
    }

    private void SetPlayerMaterial()
    {
        _playerRenderer.material = _skinInfo.Material;
    }

    private void SetFloorMaterial()
    {
        _floorRenderer.material = _sceneInfo.FloorMaterial;
        _movableFloor.CanFloorTextureMove = _sceneInfo.CanFloorTextureMove;
    }

    private void SetDecorationsOnFloor()
    {
        _movableFloor.SetDecorationObjects(_sceneInfo.Decorations);
    }
}
