using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D _rigidbody2D;
    
    private Vector2 _movementInput;

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _movementInput * speed;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        _movementInput = value.ReadValue<Vector2>();
    }
}
