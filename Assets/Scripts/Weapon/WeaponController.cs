using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Tooltip("武器データ")]
    private Weapon _weaponData = null;

    [SerializeField, Tooltip("銃口の位置")]
    private Transform _muzzle = null;

    [SerializeField, Tooltip("クロスヘア")]
    private Image _crosshair = null;

    [SerializeField, Tooltip("プレイヤーインプット")]
    private PlayerInput _playerInput = null;

    [SerializeField, Tooltip("リコイルジェネレーター")]
    private RecoilGenerator _recoilGenerator = null;

    [SerializeField, Tooltip("弾痕")]
    private GameObject _bulletHole;

    [SerializeField, Tooltip("弾数を無限にする")]
    private bool _isInfinity;

    private float _fireTimer = 0;
    /// <summary>攻撃のアクション</summary>
    private InputAction _fireAction = null;
    /// <summary>弾倉に残っている弾の数</summary>
    private int _remainingAmmo = 0;
    /// <summary>今持っている弾の数</summary>
    private int _totalAmmo = 0;
    /// <summary>最大ダメージ</summary>
    private int _maxDamage = 0;
    /// <summary>リロードアクション</summary>
    private InputAction _reloadAction = null;
    public int RemainingAmmo { get => _remainingAmmo; }
    public int TotalAmmo { get => _totalAmmo; }
    private void Awake()
    {
        if (_weaponData == null) throw new NullReferenceException("WeaponDataがありません");
        _fireAction = _playerInput.actions["Fire"];
        _reloadAction = _playerInput.actions["Reload"];
        _totalAmmo = _weaponData.TotalAmmo;
        _remainingAmmo = _weaponData.MagSize;
        _maxDamage = _weaponData.MaxDamage;
    }
    private void Update()
    {
        _fireTimer += Time.deltaTime;

        if (_fireAction.IsPressed() && _fireTimer > 60f / _weaponData.FireRate && _remainingAmmo > 0)
        {
            Fire();
            _fireTimer = 0;
        }
        if (_reloadAction.IsPressed() && _totalAmmo > 0)
        {
            Reload();
        }
    }
    private void Fire()
    {
        _recoilGenerator.RecoilFire();

        if (!_isInfinity) _remainingAmmo--;

        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(_crosshair.rectTransform.position),  transform.forward * -1);
        Debug.DrawRay(_muzzle.position, ray.direction * _weaponData.Range, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _weaponData.Range))
        {
            if (hit.transform)
            {
                // Instantiate(_bulletHole, hit.point + hit.normal * 0.5f, Quaternion.LookRotation(-hit.normal), hit.collider.transform);
                Instantiate(_bulletHole, hit.point, Quaternion.identity);
            }
            if (hit.collider.TryGetComponent(out HPManager hpManager))
            {
                var dis = Vector3.Distance(hit.point, _muzzle.position);
                var damage = (int)Math.Floor((1 - dis / _weaponData.Range) * _maxDamage);
                hpManager.GetDamage(damage);
                Debug.Log(damage);
            }
        }
    }
    private void Reload()
    {
        int temp = _totalAmmo - (_weaponData.MagSize - _remainingAmmo);
        _totalAmmo = temp;
        _remainingAmmo = _weaponData.MagSize;
    }
}