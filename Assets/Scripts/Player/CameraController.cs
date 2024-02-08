using UnityEngine.InputSystem;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float _notAimingSensitivity = 1;
    [SerializeField] float _aimingSensitivity = 0.5f;
    [SerializeField] float _topAngle = 60;
    [SerializeField] float _bottomAngle = -45;
    [SerializeField] float _yRotationSpeed = 0.1f;
    [SerializeField] float _xRotationSpeed = 0.1f;

    float _mouseSensitivity = 0;
    float _currentXRotation;
    float _currentYRotation;
    float _wantedXRotation;
    float _wantedYRotation;
    float _rotationXVelocity;
    float _rotationYVelocity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _mouseSensitivity = _notAimingSensitivity;
        if (_camera == null) throw new UnassignedReferenceException("カメラがありません");
    }

    private void FixedUpdate()
    {
        CameraRotation();
    }

    public void GetMouseInput(InputAction.CallbackContext context)
    {
        _wantedYRotation += context.ReadValue<Vector2>().x * _mouseSensitivity;
        _wantedXRotation -= context.ReadValue<Vector2>().y * _mouseSensitivity;
        _wantedXRotation = Mathf.Clamp(_wantedXRotation, _bottomAngle, _topAngle);
    }

    private void CameraRotation()
    {
        _currentYRotation = Mathf.SmoothDamp(_currentYRotation, _wantedYRotation, ref _rotationYVelocity, _yRotationSpeed);
        _currentXRotation = Mathf.SmoothDamp(_currentXRotation, _wantedXRotation, ref _rotationXVelocity, _xRotationSpeed);
        transform.rotation = Quaternion.Euler(0, _currentYRotation, 0);
        _camera.transform.localRotation = Quaternion.Euler(_currentXRotation, 0, 0);
    }

    // リコイルを反映するメソッド
    public void ApplyRecoil(Vector2 recoilRotation)
    {
        _wantedYRotation += recoilRotation.x;
        _wantedXRotation += recoilRotation.y;
        _wantedXRotation = Mathf.Clamp(_wantedXRotation, _bottomAngle, _topAngle);
    }
}
