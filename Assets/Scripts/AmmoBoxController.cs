using UnityEngine;

public class AmmoBoxController : MonoBehaviour
{
    public int SupplyAmmo;
    [SerializeField] int _supplyAmmo = 15;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var gunController = other.GetComponentInChildren<GunController>();

            if (gunController.TotalAmmo + _supplyAmmo > gunController.MaxAmmo)
            {
                gunController.TotalAmmo = gunController.MaxAmmo;
                Destroy(this.gameObject);
            }
            else
            {
                gunController.TotalAmmo += _supplyAmmo;
                Destroy(this.gameObject);
            }
        }
    }
}
