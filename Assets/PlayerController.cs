using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _splintSpeed = 3f;
    float _h;
    float _v;
    Rigidbody _rb;
    bool _isSplint = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        OnMove();
    }

    public void GetInputMove(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        _h = inputValue.x;
        _v = inputValue.y;
    }
    public void GetInputSplint(InputAction.CallbackContext context)
    {
        _isSplint = context.ReadValueAsButton();
    }
    private void OnMove()
    {
        Vector3 dir = Vector3.forward * _v + Vector3.right * _h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        if (dir != Vector3.zero) this.transform.forward = dir;

        float y = _rb.velocity.y;

        if (_isSplint)
        {
            _rb.velocity = dir * _moveSpeed * _splintSpeed + Vector3.up * y;
        }
        else
        {
            _rb.velocity = dir * _moveSpeed + Vector3.up * y;
        }
    }
}
