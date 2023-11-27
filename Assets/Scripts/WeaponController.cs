using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Transform _muzzle = null;
    [SerializeField]
    private string _weaponName;
    private Weapon _weaponData = null;
    [SerializeField]
    private Image _crosshair = null;
    private Transform _hitTarget = null;
    private float _fireTimer = 0;
    [SerializeField]
    private PlayerInput _playerInput = null;
    private InputAction _fireAction = null;
    private int _remainingAmmo = 0;
    private int _tortalAmmo = 0;
    private InputAction _reloadAction = null;
    public int RemainingAmmo { get => _remainingAmmo; }
    public int TortalAmmo { get => _tortalAmmo; }

    [SerializeField]
    private RecoilGenerator _recoilGenerator = null;
    [SerializeField]
    private GameObject _bulletHole;
    [SerializeField]
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
        if (_weaponData == null)
            return;
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
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
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