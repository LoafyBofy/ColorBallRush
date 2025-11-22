using UnityEngine;

public class SpeedIncreaser : PausedMonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _endSpeed;
    [SerializeField] private float _timeBetweenInSecond;
    [SerializeField] private GameObject[] _iSpeedObjects;

    private float _step = 0f;
    private ISpeed[] _iSpeedArray;

    public void Init()
    {
        _iSpeedArray = new ISpeed[_iSpeedObjects.Length];

        for (int i = 0; i < _iSpeedObjects.Length; i++)
        {
            _iSpeedArray[i] = _iSpeedObjects[i].GetComponent<ISpeed>();
            _iSpeedArray[i].Speed = _startSpeed;
        }

        CalculateStep();
    }

    private void Update()
    {
        if (IsPaused == false && _timeBetweenInSecond > 0)
        {
            _startSpeed += _step * Time.deltaTime;

            for (int i = 0; i < _iSpeedArray.Length; i++)
            {
                _iSpeedArray[i].Speed = _startSpeed;
            }

            if (_startSpeed >= _endSpeed) gameObject.SetActive(false);
        }
    }

    private void CalculateStep()
    {
        _step = (_endSpeed - _startSpeed) / _timeBetweenInSecond;
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterService(this);
    }
}
