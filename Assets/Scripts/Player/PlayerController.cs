using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _splintSpeed = 3f;
    float _horizontalInput;
    float _verticalInput;
    Rigidbody _rb;
    bool _isSprinting = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        _horizontalInput = inputValue.x;
        _verticalInput = inputValue.y;
    }
    public void GetSprintInput(InputAction.CallbackContext context)
    {
        _isSprinting = context.ReadValueAsButton();
    }
    private void MovePlayer()
    {
        Vector3 dir = Vector3.forward * _verticalInput + Vector3.right * _horizontalInput;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        // if (dir != Vector3.zero) this.transform.forward = dir;
        float y = _rb.velocity.y;

        if (_verticalInput < 0)
        {
            _isSprinting = false;
        }

        if (_isSprinting)
        {
            _rb.velocity = dir.normalized * _moveSpeed * _splintSpeed + Vector3.up * y;
        }
        else
        {
            _rb.velocity = dir.normalized * _moveSpeed + Vector3.up * y;
        }
    }
}
