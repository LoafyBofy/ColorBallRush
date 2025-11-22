using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private PausedMonoBehaviour[] _pausedMonoBehaviours;

    public void Init()
    {
        ServiceLocator.RegisterService(this);
    }

    public void SetPauseState(bool isPaused)
    {
        for (int i = 0; i < _pausedMonoBehaviours.Length; i++)
        {
            _pausedMonoBehaviours[i].IsPaused = isPaused;
        }
    }
}
