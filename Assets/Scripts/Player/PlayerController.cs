using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _splintSpeed = 3f;
    [SerializeField] float _groundedOffset = 0.15f;
    [SerializeField] float _groundedRadius = 0.5f;
    [SerializeField] LayerMask _groundLayers;

    Vector3 _inputVector;
    Rigidbody _rb;
    bool _isSprinting = false;
    Vector3 _normalVector = default;
    bool _isGrounded = false;
    float _verticalVelocity = 0;
    bool _isJumping = false;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        _inputVector.z = context.ReadValue<Vector2>().y;
        _inputVector.x = context.ReadValue<Vector2>().x;
    }
    public void GetSprintInput(InputAction.CallbackContext context)
    {
        _isSprinting = context.ReadValueAsButton();
    }
    public void GetJumpInput(InputAction.CallbackContext context)
    {
        _isJumping = true;
    }
    private void Move()
    {
        Vector3 dir = Vector3.forward * _inputVector.z + Vector3.right * _inputVector.x;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir = Vector3.ProjectOnPlane(dir, _normalVector);

        if (_isSprinting && _inputVector.z > 0)
        {
            _rb.velocity = dir.normalized * _moveSpeed * _splintSpeed;
        }
        else
        {
            _rb.velocity = dir.normalized * _moveSpeed;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        _normalVector = collision.contacts[0].normal;
    }
}
