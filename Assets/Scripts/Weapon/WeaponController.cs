using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField, Tooltip("銃口の位置")]
    private Transform _muzzle = null;
    [SerializeField, Tooltip("武器の名前")]
    private string _weaponName;
    /// <summary>入力された名前に紐づいた武器データ</summary>
    private Weapon _weaponData = null;
    [SerializeField, Tooltip("クロスヘア")]
    private Image _crosshair = null;
    /// <summary>Rayが当たったオブジェクト</summary>
    private Transform _hitTarget = null;
    /// <summary></summary>
    private float _fireTimer = 0;
    [SerializeField, Tooltip("プレイヤーインプット")]
    private PlayerInput _playerInput = null;
    /// <summary>攻撃のアクション</summary>
    private InputAction _fireAction = null;
    /// <summary>弾倉に残っている弾の数</summary>
    private int _remainingAmmo = 0;
    /// <summary>今持っている弾の数</summary>
    private int _tortalAmmo = 0;
    /// <summary>リロードアクション</summary>
    private InputAction _reloadAction = null;
    public int RemainingAmmo { get => _remainingAmmo; }
    public int TortalAmmo { get => _tortalAmmo; }

    [SerializeField, Tooltip("リコイルジェネレーター")]
    private RecoilGenerator _recoilGenerator = null;
    [SerializeField, Tooltip("弾痕")]
    private GameObject _bulletHole;
    [SerializeField, Tooltip("弾数を無限にする")]
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