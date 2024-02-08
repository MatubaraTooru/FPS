using UnityEngine;

public class RecoilGenerator : MonoBehaviour
{
    /// <summary>現在の回転を保持する変数</summary>
    Vector3 _currentRotation = default;
    /// <summary>目標の回転を保持する変数</summary>
    Vector3 _targetRotation = default;

    [SerializeField, Tooltip("X軸方向にどの程度拡散するか"), Range(-0.1f, -10f)] float _recoilX;
    [SerializeField, Tooltip("Y軸方向にどの程度拡散するか"), Range(0.1f, 10f)] float _recoilY;
    [SerializeField, Tooltip("Z軸方向にどの程度拡散するか"), Range(0.1f, 10f)] float _recoilZ;
    [SerializeField, Tooltip("反動の速さ"), Range(1f, 10f)] float _snappiness;
    [SerializeField, Tooltip("元の位置に戻るまでのスピード"), Range(0f, 10f)]float _returnSpeed;
    [SerializeField] CameraController _cameraController;

    private void Update()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Lerp(_currentRotation, _targetRotation, _snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire()
    {
        _targetRotation += new Vector3(_recoilX, Random.Range(-_recoilY, _recoilY), Random.Range(-_recoilZ, _recoilZ));
        _cameraController.ApplyRecoil(new Vector2(_targetRotation.y, _targetRotation.x));
    }
}
