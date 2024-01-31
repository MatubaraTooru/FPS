using UnityEngine.InputSystem;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _notAimingSensitivity = 300;
    [SerializeField] private float _aimingSensitivity = 50;
    [SerializeField] private float _topAngle = 60;
    [SerializeField] private float _bottomAngle = -45;
    [SerializeField] private float _yRotationSpeed = 0.1f;
    [SerializeField] private float _xRotationSpeed = 0.1f;

    private float _mouseSensitivity = 0;
    private float _currentXRotation;
    private float _currentYRotation;
    private float _wantedXRotation;
    private float _wantedYRotation;
    private float _rotationXVelocity;
    private float _rotationYVelocity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _mouseSensitivity = _notAimingSensitivity;
    }

    private void FixedUpdate()
    {
        Test();
    }

    public void GetMouseInput(InputAction.CallbackContext context)
    {
        _wantedYRotation += context.ReadValue<Vector2>().x * _mouseSensitivity;
        _wantedXRotation -= context.ReadValue<Vector2>().y * _mouseSensitivity;
        _wantedXRotation = Mathf.Clamp(_wantedXRotation, _bottomAngle, _topAngle);
    }

    private void Test()
    {
        _currentYRotation = Mathf.SmoothDamp(_currentYRotation, _wantedYRotation, ref _rotationYVelocity, _yRotationSpeed);
        _currentXRotation = Mathf.SmoothDamp(_currentXRotation, _wantedXRotation, ref _rotationXVelocity, _xRotationSpeed);
        transform.rotation = Quaternion.Euler(0, _currentYRotation, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(_currentXRotation, 0, 0);
    }
}
