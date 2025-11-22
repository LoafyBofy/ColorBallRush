using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerBall : PausedMonoBehaviour, IColor, ISpeed
{
    [SerializeField] private float _moveOffset;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _downForce;
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private ColorsConfig _colorConfig;
    [SerializeField] private LayerMask _surfaceLayerMask;
    [SerializeField] private float _raycastDistance = 2f;

    public bool UseGravity
    {
        get
        {
            return _rb.useGravity;
        }
        set
        {
            _rb.useGravity = value;

            if (value == false)
            {
                _stopedVelocity = _rb.linearVelocity;
                _rb.linearVelocity = Vector3.zero;
            }
            else
            {
                _rb.linearVelocity = _stopedVelocity;
            }
                
        }
    }
    public bool CanMove { get; set; } = false;
    public Color CurrentColor { get; set; }
    public float Speed
    {
        get
        {
            return _moveSpeed;
        }
        set
        {
            _moveSpeed = value;
        }
    }

    private Vector3 _stopedVelocity;
    private Renderer _renderer;
    private int _currentLine = 2;
    private bool _isGrounded = true;
    private Rigidbody _rb;
    private float _currentOffset = 0f;
    private Wallet _collector;
    private SfxController _sfx;

    public event Action Died;

    public void Init()
    {
        _sfx = ServiceLocator.GetService(_sfx);
        _collector = ServiceLocator.GetService(_collector);
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponentInChildren<Renderer>();
        ChangeColorToRandom();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _raycastDistance, _surfaceLayerMask))
            _isGrounded = true;
        else
            _isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (CanMove == false || IsPaused == true) return;

        _rb.MovePosition(_rb.position + transform.forward * _moveSpeed * Time.fixedDeltaTime);

        var newPosition = new Vector3(_currentOffset, _rb.position.y, _rb.position.z);
        _rb.position = Vector3.MoveTowards(_rb.position, newPosition, _transitionSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coin.Interact(_collector.AddCoin);
        }
        else if (other.TryGetComponent(out ColorChanger changer))
        {
            changer.Interact(ChangeColorToRandom);
        }
        else if (other.TryGetComponent(out Wall wall))
        {
            if (wall.CurrentColor == CurrentColor)
            {
                wall.Interact();
            }
            else
            {
                _sfx.Hit();
                Die();
            }
        }
    }

    public void MoveLeft()
    {
        if (CanMove == false || IsPaused == true) return;
        if (_currentLine == 3) return;

        _currentOffset -= _moveOffset;
        _currentLine++;
    }

    public void MoveRight()
    {
        if (CanMove == false || IsPaused == true) return;
        if (_currentLine == 1) return;

        _currentOffset += _moveOffset;
        _currentLine--;
    }

    public void Jump()
    {
        if (CanMove == false || IsPaused == true) return;

        if (_isGrounded == true)
        {
            _sfx.Jump();
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void Down()
    {
        if (CanMove == false || IsPaused == true) return;

        if (_isGrounded == false)
            _rb.AddForce(Vector3.down * _downForce, ForceMode.Impulse);
    }

    public void ChangeColorToRandom()
    {
        var newColor = _colorConfig.GetRandomColor();

        while (newColor == CurrentColor)
        {
            newColor = _colorConfig.GetRandomColor();
        }

        CurrentColor = newColor;
        _renderer.material.color = CurrentColor;
    }

    private void Die()
    {
        Died?.Invoke();
        _currentLine = 2;
        _currentOffset = 0f;
        _isGrounded = true;
        gameObject.SetActive(false);
    }
}
