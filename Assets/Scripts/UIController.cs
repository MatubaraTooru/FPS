using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    private WeaponController _weaponController;
    // Start is called before the first frame update
    void Start()
    {
        _weaponController = FindAnyObjectByType<WeaponController>();
        _ammoText.text = $"{_weaponController.RemainingAmmo} / {_weaponController.TortalAmmo}";
    }

    // Update is called once per frame
    void Update()
    {
        _ammoText.text = $"{_weaponController.RemainingAmmo} / {_weaponController.TortalAmmo}";
    }
}
