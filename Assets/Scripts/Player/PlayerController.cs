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
    [SerializeField] float _jumpHeight;
    [SerializeField] float _gravity;
    [SerializeField] GunController _gunController;

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
        CalculateVertical();
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
            _rb.velocity = dir.normalized * _moveSpeed * _splintSpeed + Vector3.up * _verticalVelocity;
        }
        else
        {
            _rb.velocity = dir.normalized * _moveSpeed + Vector3.up * _verticalVelocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("AmmoBox"))
        //{
        //    var supplyAmmo = other.GetComponent<AmmoBoxController>().SupplyAmmo;

        //    if (_gunController.TotalAmmo + supplyAmmo > _gunController.MaxAmmo)
        //    {
        //        _gunController.TotalAmmo = _gunController.MaxAmmo;
        //        Destroy(this.gameObject);
        //    }
        //    else
        //    {
        //        _gunController.TotalAmmo += supplyAmmo;
        //        Destroy(this.gameObject);
        //    }
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        _normalVector = collision.contacts[0].normal;
    }

    private void CalculateVertical()
    {
        if (_isGrounded)
        {
            if (_verticalVelocity < 0f)
            {
                _verticalVelocity = -2f;
            }
            if (_isJumping)
            {
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
        }
        else
        {
            _isJumping = false;
        }

        _verticalVelocity += _gravity * Time.deltaTime;

        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + _groundedOffset, transform.position.z);
        _isGrounded = Physics.CheckSphere(spherePosition, 0.5f, _groundLayers, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = Color.green;
        Color transparentRed = Color.red;

        if (_isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + _groundedOffset, transform.position.z), _groundedRadius);
    }
}
