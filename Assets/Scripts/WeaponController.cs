using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Transform _muzzle = null;
    [SerializeField]
    private WeaponDataAsset _weaponDataAsset = null;
    private WeaponData _weaponData = null;
    [SerializeField]
    private int _weaponID = -1;
    [SerializeField]
    private Image _crosshair = null;
    private Transform _hitTarget = null;
    private float _fireTimer = 0;
    private bool _isFirePossible = false;
    private PlayerInput _playerInput = null;
    private InputAction _fireAction = null;
    private int _zandansuu = 0;
    private int _maxAmmo = 0;
    private void Awake()
    {
        if (_weaponID != -1)
            _weaponData = _weaponDataAsset.WeaponDatas[_weaponID];
        _playerInput = FindAnyObjectByType<PlayerInput>();
        _fireAction = _playerInput.actions["Fire"];
        _maxAmmo = _weaponData.MaxAmmo;
        _zandansuu = _weaponData.MagSize;
    }
    private void Update()
    {
        if (_weaponData == null)
            return;
        _fireTimer += Time.deltaTime;

        if (_fireTimer > 60f / _weaponData.Firerate)
        {
            _isFirePossible = true;
            _fireTimer = 0;
        }

        if (_fireAction.IsPressed() && _isFirePossible)
        {
            Fire();
        }
    }
    private void Fire()
    {
        if (_zandansuu == 0)
            return;
        _zandansuu--;
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
        Debug.DrawRay(ray.origin, ray.direction * _weaponData.EffectiveRange, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _weaponData.EffectiveRange))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(Vector3.up * 2, ForceMode.Impulse);
            }
        }

        _isFirePossible = false;
    }
    private void Reload()
    {
        int temp = _weaponData.MagSize - _zandansuu;

    }
}