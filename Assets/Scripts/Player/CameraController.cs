using UnityEngine.InputSystem;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _notAimingSensitivity = 1;
    [SerializeField] float _aimingSensitivity = 0.5f;
    [SerializeField] float _maxVerticalAngle = 60;
    [SerializeField] float _minVerticalAngle = -45;
    [SerializeField] float _yRotationSpeed = 0.1f;
    [SerializeField] float _xRotationSpeed = 0.1f;

    float _mouseSensitivity = 0;
    float _currentVerticalRotation;
    float _currentHorizontalRotation;
    float _desiredVerticalRotation;
    float _desiredHorizontalRotation;
    float _rotationXVelocity;
    float _rotationYVelocity;



    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _mouseSensitivity = _notAimingSensitivity;
    }

    private void FixedUpdate()
    {
        RotateCamera();
    }

    public void GetMouseInput(InputAction.CallbackContext context)
    {
        _desiredHorizontalRotation += context.ReadValue<Vector2>().x * _mouseSensitivity; // X軸の移動が水平方向の回転に適用される
        _desiredVerticalRotation -= context.ReadValue<Vector2>().y * _mouseSensitivity; // Y軸の移動が垂直方向の回転に適用される
        _desiredVerticalRotation = Mathf.Clamp(_desiredVerticalRotation, _minVerticalAngle, _maxVerticalAngle);
    }

    private void RotateCamera()
    {
        _currentVerticalRotation = Mathf.SmoothDamp(_currentVerticalRotation, _desiredVerticalRotation, ref _rotationYVelocity, _yRotationSpeed);
        _currentHorizontalRotation = Mathf.SmoothDamp(_currentHorizontalRotation, _desiredHorizontalRotation, ref _rotationXVelocity, _xRotationSpeed);

        transform.rotation = Quaternion.Euler(0, _currentHorizontalRotation, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(_currentVerticalRotation, 0, 0);
    }

    // リコイルを反映するメソッド
    public void ApplyRecoil(Vector2 recoilRotation)
    {
        Debug.Log($"{recoilRotation.x} {recoilRotation.y}");
        _desiredHorizontalRotation -= recoilRotation.y;
        _desiredVerticalRotation -= recoilRotation.x;
        _desiredVerticalRotation = Mathf.Clamp(_desiredVerticalRotation, _minVerticalAngle, _maxVerticalAngle);
    }
}
