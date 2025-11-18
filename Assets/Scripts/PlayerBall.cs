using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerBall : PausedMonoBehaviour, IColor
{
    [SerializeField] private float _moveOffset;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _downForce;
    [SerializeField] private ColorsConfig _colorConfig;

    public bool UseGravity
    {
        get
        {
            return _rb.useGravity;
        }
        set
        {
            _rb.useGravity = value;
        }
    }
    public bool CanMove { get; set; } = false;
    public Color CurrentColor { get; set; }

    private Renderer _renderer;
    private int _currentLine = 2;
    private bool _isGrounded = true;
    private Rigidbody _rb;

    public event Action Died;

    public void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponentInChildren<Renderer>();
        ChangeColorToRandom();
    }

    private void FixedUpdate()
    {
        if (CanMove == false || IsPaused == true) return;

        _rb.MovePosition(_rb.position + transform.forward * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactiveObject))
        {
            interactiveObject.Interact();
        }
        else if (other.TryGetComponent(out Wall wall))
        {
            if (wall.CurrentColor == CurrentColor)
            {
                wall.gameObject.SetActive(false);
            }
            else
            {
                Die();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            _isGrounded = false;
        }
    }

    public void MoveLeft()
    {
        if (CanMove == false || IsPaused == true) return;

        if (_currentLine == 3) return;
        _rb.MovePosition(_rb.position - new Vector3(_moveOffset, 0, 0));
        _currentLine++;
    }

    public void MoveRight()
    {
        if (CanMove == false || IsPaused == true) return;

        if (_currentLine == 1) return;
        _rb.MovePosition(_rb.position + new Vector3(_moveOffset, 0, 0));
        _currentLine--;
    }

    public void Jump()
    {
        if (CanMove == false || IsPaused == true) return;

        if (_isGrounded == true)
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
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
        gameObject.SetActive(false);
    }
    
}
