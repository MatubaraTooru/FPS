using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Transform _muzzle;
    [SerializeField]
    private WeaponData _weaponData;
    [SerializeField]
    private Image _crosshair;
    private Transform _hitTarget;
    private float _fireTimer = 0;
    private bool _isFirePossible = false;
    private void Awake()
    {
        
    }
    private void Update()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer > _weaponData.Firerate)
        {
            _isFirePossible = true;
            _fireTimer = 0;
        }
    }
    public void Fire(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _weaponData.EffectiveRange))
        {
            if (hit.rigidbody)
                hit.rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        _isFirePossible = false;
    }
}