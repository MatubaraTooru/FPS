using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Tooltip("����f�[�^")]
    private Weapon _weaponData = null;

    [SerializeField, Tooltip("�e���̈ʒu")]
    private Transform _muzzle = null;

    [SerializeField, Tooltip("�N���X�w�A")]
    private Image _crosshair = null;

    [SerializeField, Tooltip("�v���C���[�C���v�b�g")]
    private PlayerInput _playerInput = null;

    [SerializeField, Tooltip("���R�C���W�F�l���[�^�[")]
    private RecoilGenerator _recoilGenerator = null;

    [SerializeField, Tooltip("�e��")]
    private GameObject _bulletHole;

    [SerializeField, Tooltip("�e���𖳌��ɂ���")]
    private bool _isInfinity;

    private float _fireTimer = 0;
    /// <summary>�U���̃A�N�V����</summary>
    private InputAction _fireAction = null;
    /// <summary>�e�q�Ɏc���Ă���e�̐�</summary>
    private int _remainingAmmo = 0;
    /// <summary>�������Ă���e�̐�</summary>
    private int _totalAmmo = 0;
    /// <summary>�ő�_���[�W</summary>
    private int _maxDamage = 0;
    /// <summary>�����[�h�A�N�V����</summary>
    private InputAction _reloadAction = null;
    public int RemainingAmmo { get => _remainingAmmo; }
    public int TotalAmmo { get => _totalAmmo; }
    private void Awake()
    {
        if (_weaponData == null) throw new NullReferenceException("WeaponData������܂���");
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