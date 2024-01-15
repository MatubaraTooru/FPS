using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Tooltip("�e���̈ʒu")]
    private Transform _muzzle = null;
    [SerializeField, Tooltip("����̖��O")]
    private string _weaponName;
    /// <summary>���͂��ꂽ���O�ɕR�Â�������f�[�^</summary>
    private Weapon _weaponData = null;
    [SerializeField, Tooltip("�N���X�w�A")]
    private Image _crosshair = null;
    /// <summary>Ray�����������I�u�W�F�N�g</summary>
    private Transform _hitTarget = null;
    /// <summary></summary>
    private float _fireTimer = 0;
    [SerializeField, Tooltip("�v���C���[�C���v�b�g")]
    private PlayerInput _playerInput = null;
    /// <summary>�U���̃A�N�V����</summary>
    private InputAction _fireAction = null;
    /// <summary>�e�q�Ɏc���Ă���e�̐�</summary>
    private int _remainingAmmo = 0;
    /// <summary>�������Ă���e�̐�</summary>
    private int _tortalAmmo = 0;
    /// <summary>�����[�h�A�N�V����</summary>
    private InputAction _reloadAction = null;
    public int RemainingAmmo { get => _remainingAmmo; }
    public int TortalAmmo { get => _tortalAmmo; }

    [SerializeField, Tooltip("���R�C���W�F�l���[�^�[")]
    private RecoilGenerator _recoilGenerator = null;
    [SerializeField, Tooltip("�e��")]
    private GameObject _bulletHole;
    [SerializeField, Tooltip("�e���𖳌��ɂ���")]
    private bool _isInfinity;
    private void Awake()
    {
        _weaponData = (Weapon)Resources.Load(_weaponName);
        _playerInput = FindAnyObjectByType<PlayerInput>();
        _fireAction = _playerInput.actions["Fire"];
        _reloadAction = _playerInput.actions["Reload"];
        _tortalAmmo = _weaponData.TortalAmmo;
        _remainingAmmo = _weaponData.MagSize;
    }
    private void Update()
    {
        if (_weaponData == null) return;
        _fireTimer += Time.deltaTime;

        if (_fireAction.IsPressed() && _fireTimer > 60f / _weaponData.Firerate)
        {
            Fire();
            _fireTimer = 0;
        }
        if (_reloadAction.IsPressed())
        {
            Reload();
        }
    }
    private void Fire()
    {
        if (_remainingAmmo <= 0) return;
        _recoilGenerator.RecoilFire();
        if (!_isInfinity) _remainingAmmo--;
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(_crosshair.rectTransform.position),  transform.right);
        Debug.DrawRay(ray.origin, ray.direction * _weaponData.Range, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _weaponData.Range))
        {
            if (hit.transform)
            {
                Instantiate(_bulletHole, hit.point + hit.normal * 0.5f, Quaternion.LookRotation(-hit.normal), hit.collider.transform); ;
            }
        }
    }
    private void Reload()
    {
        if (_tortalAmmo <= 0) return;
        int temp = _tortalAmmo - (_weaponData.MagSize - _remainingAmmo);
        _tortalAmmo = temp;
        _remainingAmmo = _weaponData.MagSize;
    }
}