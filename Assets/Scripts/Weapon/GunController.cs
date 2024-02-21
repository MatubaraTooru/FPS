using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [SerializeField] FireMode _firemode;
    [SerializeField, Tooltip("武器データ")] Weapon _weaponData = null;
    [SerializeField, Tooltip("銃口の位置")] Transform _muzzle = null;
    [SerializeField, Tooltip("クロスヘア")] Image _crosshair = null;
    [SerializeField, Tooltip("プレイヤーインプット")] PlayerInput _playerInput = null;
    [SerializeField, Tooltip("弾痕")] GameObject _bulletHole;
    [SerializeField, Tooltip("弾数を無限にする")] bool _isInfinity = false;
    [SerializeField] bool _isToggleAim = false;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _muzzleFlash;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] AudioSource _audioSource;

    float _fireTimer = 0;
    /// <summary>弾倉に残っている弾の数</summary>
    int _remainingAmmo = 0;
    /// <summary>今持っている弾の数</summary>
    int _totalAmmo = 0;
    /// <summary>最大ダメージ</summary>
    int _maxDamage = 0;

    int _maxAmmo = 0;
    [SerializeField] bool _isAiming = false;
    bool _isReloading = false;

    public int RemainingAmmo { get => _remainingAmmo; }
    public int TotalAmmo { get => _totalAmmo; set => _totalAmmo += value; }
    public int MaxAmmo { get => _maxAmmo; }

    [Header("Recoil")]
    [SerializeField, Tooltip("X軸方向にどの程度拡散するか")] float _notAimingRecoilX;
    [SerializeField, Tooltip("Y軸方向にどの程度拡散するか")] float _notAimingRecoilY;
    [SerializeField] float _aimingRecoilX;
    [SerializeField] float _aimingRecoilY;
    float _currentRecoilX;
    float _currentRecoilY;
    [SerializeField] float _accuracy;
    [SerializeField] CameraController _cameraController;

    /// <summary>現在の回転を保持する変数</summary>
    Vector2 _currentRotation = default;
    float _currentRotationX = 0f;
    float _currentRotationY = 0f;
    /// <summary>目標の回転を保持する変数</summary>
    Vector2 _targetRotation = default;
    enum FireMode
    {
        None, SemiAuto, FullAuto, BoltAction
    }
    private void Awake()
    {
        if (_weaponData == null) throw new NullReferenceException("WeaponDataがありません");
        _totalAmmo = _weaponData.TotalAmmo;
        _maxAmmo = _weaponData.TotalAmmo;
        _remainingAmmo = _weaponData.MagSize;
        _maxDamage = _weaponData.MaxDamage;
        // _muzzleFlash?.SetActive(false);
    }
    private void OnEnable()
    {
        _playerInput.actions["Reload"].started += OnReload;
    }
    private void OnDisable()
    {
        _playerInput.actions["Reload"].started -= OnReload;
    }
    private void Update()
    {
        _fireTimer += Time.deltaTime;
        if (_playerInput.actions["Aim"].WasPressedThisFrame()) _isAiming = true;
        if (_playerInput.actions["Aim"].WasReleasedThisFrame()) _isAiming = false;
        if (_playerInput.actions["Fire"].IsPressed() && (_fireTimer > 60f / _weaponData.FireRate) && (_remainingAmmo > 0) && !_isReloading) Fire();
        // else if (!_playerInput.actions["Fire"].IsPressed())
    }
    private void Fire()
    {
        RecoilFire();
        // _muzzleFlash?.SetActive(true);
        _audioSource?.PlayOneShot(_audioSource.clip);
        if (!_isInfinity) _remainingAmmo--;

        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(_crosshair.rectTransform.position), transform.forward * -1);
        Debug.DrawRay(_muzzle.position, ray.direction * _weaponData.Range, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _weaponData.Range, _layerMask))
        {
            //if (hit.transform)
            //{
            //    // Instantiate(_bulletHole, hit.point + hit.normal * 0.5f, Quaternion.LookRotation(-hit.normal), hit.collider.transform);
            //    Instantiate(_bulletHole, hit.point, Quaternion.identity);
            //}
            if (hit.collider.TryGetComponent(out HPManager hpManager))
            {
                var dis = Vector3.Distance(hit.point, _muzzle.position);
                var damage = (int)Math.Floor((1 - dis / _weaponData.Range) * _maxDamage);
                hpManager.GetDamage(damage);
                Debug.Log(damage);
            }
        }

        // _muzzleFlash?.SetActive(false);
        _fireTimer = 0;
    }

    private void OnReload(InputAction.CallbackContext callback)
    {
        if (_totalAmmo <= 0) return;
        _isReloading = true;
        _animator.SetBool("IsReloading", _isReloading);
    }

    private void Reload()
    {
        int temp = _totalAmmo - (_weaponData.MagSize - _remainingAmmo);

        if (temp < 0)
        {
            _remainingAmmo += _totalAmmo;
            _totalAmmo = 0;
        }
        else
        {
            _totalAmmo = temp;
            _remainingAmmo = _weaponData.MagSize;
        }

        _isReloading = false;
        _animator.SetBool("IsReloading", _isReloading);
    }

    private void AimDownSights()
    {
        
    }

    public void RecoilFire()
    {
        _currentRotationX -= (UnityEngine.Random.value - 0.5f) * _notAimingRecoilX;
        _currentRotationY -= (UnityEngine.Random.value - 0.5f) * _notAimingRecoilY;
        _cameraController.ApplyRecoil(new Vector2(Mathf.Abs(_currentRotationX * _accuracy), _currentRotationY * _accuracy));
    }

    //private void GunPosition()
    //{
    //    transform.position = Vector3.SmoothDamp(transform.position,
    //        Camera.main.transform.position -
    //        (Camera.main.transform.right * (currentGunPosition.x + currentRecoilXPos)) +
    //        (Camera.main.transform.up * (currentGunPosition.y + currentRecoilYPos)) +
    //        (Camera.main.transform.forward * (currentGunPosition.z + currentRecoilZPos)),
    //        ref velV, 0);

    //}
}