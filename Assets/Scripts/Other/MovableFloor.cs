using System.Collections.Generic;
using UnityEngine;

public class MovableFloor : PausedMonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints = new();

    private Transform _target;

    public void Init(PlayerBall player)
    {
        _target = player.transform;
    }

    private void Update()
    {
        if (IsPaused == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _target.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Decoration")
        {
            var newPosition = _spawnPoints.GetRandom();
            other.transform.position = newPosition.position;
        }
    }
}
