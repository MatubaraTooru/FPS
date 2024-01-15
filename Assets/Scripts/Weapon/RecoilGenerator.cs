using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilGenerator : MonoBehaviour
{
    /// <summary>Œ»İ‚Ì‰ñ“]‚ğ•Û‚·‚é•Ï”</summary>
    private Vector3 _currentRotation;
    /// <summary>–Ú•W‚Ì‰ñ“]‚ğ•Û‚·‚é•Ï”</summary>
    private Vector3 _targetRotation;

    [SerializeField, Tooltip("X²•ûŒü‚É‚Ç‚Ì’ö“xŠgU‚·‚é‚©")]
    private float _recoilX;
    [SerializeField, Tooltip("Y²•ûŒü‚É‚Ç‚Ì’ö“xŠgU‚·‚é‚©")]
    private float _recoilY;
    [SerializeField, Tooltip("Z²•ûŒü‚É‚Ç‚Ì’ö“xŠgU‚·‚é‚©")]
    private float _recoilZ;

    [SerializeField]
    private float _snappiness;
    [SerializeField, Tooltip("Œ³‚ÌˆÊ’u‚É–ß‚é‚Ü‚Å‚ÌƒXƒs[ƒh")]
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
