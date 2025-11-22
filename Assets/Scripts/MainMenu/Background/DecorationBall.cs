using UnityEngine;

public class DecorationBall : MonoBehaviour
{
    [SerializeField] private float _minOffset = -15f;
    [SerializeField] private float _maxOffset = 15f;
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _minDistanceToTarget = 0.1f;

    RectTransform _rectTransform;
    private Vector2 _startPosition;
    private Vector2 _newPosition;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.anchoredPosition;
        
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, _rectTransform.position, _camera, out _startPosition);
        _newPosition = GetNewPosition();
    }

    private void Update()
    {
        _rectTransform.anchoredPosition = Vector3.MoveTowards(_rectTransform.anchoredPosition, _newPosition, _speed * Time.deltaTime);

        if (Vector3.Distance(_rectTransform.anchoredPosition, _newPosition) <= _minDistanceToTarget)
            _newPosition = GetNewPosition();
    }

    private Vector2 GetNewPosition()
    {
        var newOffset = new Vector2
            (
                Random.Range(_minOffset, _maxOffset),
                Random.Range(_minOffset, _maxOffset)
            );

        return _startPosition + newOffset;
    }
}
