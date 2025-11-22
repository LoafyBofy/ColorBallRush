using UnityEngine;

public class FogMovable : PausedMonoBehaviour
{
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
}
