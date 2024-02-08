using UnityEngine;

public class RecoilGenerator : MonoBehaviour
{
    /// <summary>���݂̉�]��ێ�����ϐ�</summary>
    Vector3 _currentRotation = default;
    /// <summary>�ڕW�̉�]��ێ�����ϐ�</summary>
    Vector3 _targetRotation = default;

    [SerializeField, Tooltip("X�������ɂǂ̒��x�g�U���邩"), Range(-0.1f, -10f)] float _recoilX;
    [SerializeField, Tooltip("Y�������ɂǂ̒��x�g�U���邩"), Range(0.1f, 10f)] float _recoilY;
    [SerializeField, Tooltip("Z�������ɂǂ̒��x�g�U���邩"), Range(0.1f, 10f)] float _recoilZ;
    [SerializeField, Tooltip("�����̑���"), Range(1f, 10f)] float _snappiness;
    [SerializeField, Tooltip("���̈ʒu�ɖ߂�܂ł̃X�s�[�h"), Range(0f, 10f)]float _returnSpeed;
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
