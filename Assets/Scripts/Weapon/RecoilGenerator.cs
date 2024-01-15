using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilGenerator : MonoBehaviour
{
    /// <summary>現在の回転を保持する変数</summary>
    private Vector3 _currentRotation;
    /// <summary>目標の回転を保持する変数</summary>
    private Vector3 _targetRotation;

    [SerializeField, Tooltip("X軸方向にどの程度拡散するか")]
    private float _recoilX;
    [SerializeField, Tooltip("Y軸方向にどの程度拡散するか")]
    private float _recoilY;
    [SerializeField, Tooltip("Z軸方向にどの程度拡散するか")]
    private float _recoilZ;

    [SerializeField]
    private float _snappiness;
    [SerializeField, Tooltip("元の位置に戻るまでのスピード")]
    private float _returnSpeed;

    private void Update()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Lerp(_currentRotation, _targetRotation, _snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire()
    {
        _targetRotation += new Vector3(_recoilX, Random.Range(-_recoilY, _recoilY), Random.Range(-_recoilZ, _recoilZ));
    }
}
