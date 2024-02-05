using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private float _splintSpeed = 3f;

    Vector2 _inputVector;
    Rigidbody _rb;
    bool _isSprinting = false;
    Vector3 _normalVector = default;
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
        _inputVector = Vector2.zero;
        _inputVector = context.ReadValue<Vector2>();
        Debug.Log($"Y : {_inputVector.y} X : {_inputVector.x}");
    }
    public void GetSprintInput(InputAction.CallbackContext context)
    {
        _isSprinting = context.ReadValueAsButton();
    }
    private void MovePlayer()
    {
        Vector3 dir = Vector3.forward * _inputVector.y + Vector3.right * _inputVector.x;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        Vector3 onPlane = Vector3.ProjectOnPlane(_inputVector, _normalVector);
        Debug.DrawRay(this.transform.position, onPlane, Color.red, 20);

        float y = _rb.velocity.y;

        if (_isSprinting && _inputVector.y > 0)
        {
            _rb.velocity = dir.normalized * _moveSpeed * _splintSpeed + Vector3.up * y;
        }
        else
        {
            _rb.velocity = dir.normalized * _moveSpeed + Vector3.up * y;
        }
    }

    private void GetNormal()
    {
        // Ray ray = 
    }

    private void OnCollisionStay(Collision collision)
    {
        _normalVector = collision.contacts[0].normal;
    }
}
