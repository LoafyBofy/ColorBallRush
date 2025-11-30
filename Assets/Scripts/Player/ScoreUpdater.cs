using System;
using UnityEngine;

public class ScoreUpdater : PausedMonoBehaviour
{
    private uint _score;
    private double _currentScore = 0;

    public uint Score { get { return _score; } }
    public event Action<uint> Change;
    public event Action<uint> Added;

    public void Init()
    {
        ServiceLocator.RegisterService(this);
    }

    private void Update()
    {
        if (IsPaused == false)
        {
            _currentScore += Time.deltaTime;
            _score = (uint)_currentScore;
            Change?.Invoke(_score);
        }
    }

    public void AddScore(int amount)
    {
        _currentScore += amount;
        _score = (uint)_currentScore;
        Added?.Invoke(_score);
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterService(this);
    }
}
