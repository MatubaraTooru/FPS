using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField, Tooltip("����f�[�^")] Weapon _weaponData = null;
    [SerializeField, Tooltip("�e���̈ʒu")] Transform _muzzle = null;
    [SerializeField, Tooltip("�N���X�w�A")] Image _crosshair = null;
    [SerializeField, Tooltip("�v���C���[�C���v�b�g")] PlayerInput _playerInput = null;
    [SerializeField, Tooltip("���R�C���W�F�l���[�^�[")] RecoilGenerator _recoilGenerator = null;
    [SerializeField, Tooltip("�e��")] GameObject _bulletHole;
    [SerializeField, Tooltip("�e���𖳌��ɂ���")] bool _isInfinity;
    [SerializeField] Animator _animator;

    float _fireTimer = 0;
    /// <summary>�U���̃A�N�V����</summary>
    InputAction _fireAction = null;
    /// <summary>�e�q�Ɏc���Ă���e�̐�</summary>
    int _remainingAmmo = 0;
    /// <summary>�������Ă���e�̐�</summary>
    int _totalAmmo = 0;
    /// <summary>�ő�_���[�W</summary>
    int _maxDamage = 0;
    /// <summary>�����[�h�A�N�V����</summary>
    InputAction _reloadAction = null;
    public int RemainingAmmo { get => _remainingAmmo; }
    public int TotalAmmo { get => _totalAmmo; }
    private void Awake()
    {
        if (_weaponData == null) throw new NullReferenceException("WeaponData������܂���");
        _totalAmmo = _weaponData.TotalAmmo;
        _remainingAmmo = _weaponData.MagSize;
        _maxDamage = _weaponData.MaxDamage;
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

        if (_playerInput.actions["Fire"].IsPressed() && (_fireTimer > 60f / _weaponData.FireRate) && (_remainingAmmo > 0)) Fire();
    }
    private void Fire()
    {
        _animator.SetTrigger(0);
        _recoilGenerator.RecoilFire();

        if (!_isInfinity) _remainingAmmo--;

        Ray ray = new Ray(_camera.ScreenToWorldPoint(_crosshair.rectTransform.position), transform.forward * -1);
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

        _fireTimer = 0;
    }
    private void OnReload(InputAction.CallbackContext callback)
    {
        if (_totalAmmo <= 0) return;
        int temp = _totalAmmo - (_weaponData.MagSize - _remainingAmmo);
        _totalAmmo = temp;
        _remainingAmmo = _weaponData.MagSize;
    }
}