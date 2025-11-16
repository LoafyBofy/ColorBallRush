using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerBall : MonoBehaviour
{
    [SerializeField] private float _moveOffset;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _downForce;

    private int _currentLine = 2;
    private bool _isGrounded = true;
    private Rigidbody _rb;

    public void Init()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + transform.forward * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactive")
        {
            other.GetComponent<IInteractable>().Interact();
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
        if (_currentLine == 3) return;
        _rb.MovePosition(_rb.position - new Vector3(_moveOffset, 0, 0));
        _currentLine++;
    }

    public void MoveRight()
    {
        if (_currentLine == 1) return;
        _rb.MovePosition(_rb.position + new Vector3(_moveOffset, 0, 0));
        _currentLine--;
    }

    public void Jump()
    {
        if (_isGrounded == true)
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void Down()
    {
        if (_isGrounded == false)
            _rb.AddForce(Vector3.down * _downForce, ForceMode.Impulse);
    }
}
