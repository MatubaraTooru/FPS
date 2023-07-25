using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    [SerializeField, Header("íÌID")]
    private int _weaponID = -1;
    [SerializeField, Header("íÌíÞ")]
    private WeaponType _weaponType = WeaponType.None;
    [SerializeField, Header("íÌ¼O")]
    private string _weaponName = "None";
    [SerializeField, Header("íÌÅå_[W")]
    private float _weaponMaxDamage = 0.0f;
    [SerializeField, Header("Ååeò")]
    private int _maxAmmo = 0;
    [SerializeField, Header("eqÌå«³")]
    private int _magSize = 0;
    [SerializeField, Header("AË¬x")]
    private float _firerate = 0;
    [SerializeField, Header("ÄU¬x")]
    private float _reloadSpeed = 0;
    [SerializeField, Header("LøËö")]
    private float _effectiveRange;

    public int WeaponID { get => _weaponID; }
    public WeaponType WeaponTypeData { get => _weaponType; }
    public string WeaponName { get => _weaponName; }
    public float WeaponMaxDamage { get => _weaponMaxDamage; }
    public int MaxAmmo { get => _maxAmmo; }
    public int MagSize { get => _magSize; }
    public float Firerate { get => _firerate; }
    public float ReloadSpeed { get => _reloadSpeed; }
    public float EffectiveRange { get => _effectiveRange; }

    public enum WeaponType
    {
        None,
        AssultRifle,
        ShotGun,
        SniperRifle,
        SubmachineGun,
        HandGun,
        AntiTankMissile,
    }
}
