using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private PausedMonoBehaviour[] _pausedMonoBehaviours;

    public void SwitchPauseState()
    {
        for (int i = 0; i < _pausedMonoBehaviours.Length; i++)
        {
            _pausedMonoBehaviours[i].IsPaused = !_pausedMonoBehaviours[i].IsPaused;
        }
    }

    public void SetPauseState(bool isPaused)
    {
        for (int i = 0; i < _pausedMonoBehaviours.Length; i++)
        {
            _pausedMonoBehaviours[i].IsPaused = isPaused;
        }
    }
}
