using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovableFloor : PausedMonoBehaviour, ISpeed
{
    [SerializeField] private float _textureSpeedDivider = 2f;
    [SerializeField] private List<Transform> _spawnPoints = new();

    public bool CanFloorTextureMove { get; set; } = false;
    public int SpawnPointCount { get { return _spawnPoints.Count; } }

    public float Speed { get; set; }

    private List<Transform> _spawnPointsTemp = new();
    private Transform _target;
    private Renderer _renderer;
    private Vector2 _offset;

    public void Init(PlayerBall player)
    {
        _target = player.transform;
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (IsPaused == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _target.position.z);

            if (CanFloorTextureMove && _textureSpeedDivider > 0)
            {
                _offset.y -= (Time.deltaTime * Speed) / _textureSpeedDivider;
                _renderer.material.mainTextureOffset = _offset;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Decoration")
        {
            var newPosition = _spawnPoints.GetRandom();
            other.transform.position = newPosition.position;
            var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            other.transform.rotation = randomRotation;
        }
    }

    public void SetDecorationObjects(List<GameObject> objectsList, int amount)
    {
        if (_spawnPoints.Count > 0 && amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject newObj = objectsList.GetRandom();
                newObj = Instantiate(newObj);
                newObj.transform.position = _spawnPoints[i].transform.position;
            }
        }
    }
}
