using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputSystem _inputSystem;
    private PlayerBall _player;

    public void Init(InputSystem inputSystem, PlayerBall player) 
    {
        _inputSystem = inputSystem;
        _player = player;

        _inputSystem = new();
        _inputSystem.Enable();
    }

    private void OnEnable()
    {
        _inputSystem.Gameplay.Left.performed += PressLeft;
        _inputSystem.Gameplay.Right.performed += PressRight;
        _inputSystem.Gameplay.Jump.performed += Jump;
        _inputSystem.Gameplay.Down.performed += Down;
    }

    private void OnDisable()
    {
        _inputSystem.Gameplay.Left.performed -= PressLeft;
        _inputSystem.Gameplay.Right.performed -= PressRight;
        _inputSystem.Gameplay.Jump.performed -= Jump;
        _inputSystem.Gameplay.Down.performed -= Down;
    }

    private void PressLeft(InputAction.CallbackContext obj)
    {
        _player.MoveLeft();
    }

    private void PressRight(InputAction.CallbackContext obj)
    {
        _player.MoveRight();
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        _player.Jump();
    }

    private void Down(InputAction.CallbackContext obj)
    {
        _player.Down();
    }
}
