using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    [SerializeField, Header("武器の種類")]
    private WeaponType _weaponType = WeaponType.None;
    [SerializeField, Header("武器の名前")]
    private string _weaponName = "None";
    [SerializeField, Header("武器の最大ダメージ")]
    private float _weaponMaxDamage = 0.0f;
    [SerializeField, Header("最大所持弾薬数")]
    private int _maxAmmo = 0;
    [SerializeField, Header("弾倉の大きさ")]
    private int _magSize = 0;
    [SerializeField, Header("連射速度")]
    private float _firerate = 0;
    [SerializeField, Header("再装填速度")]
    private float _reloadSpeed = 0;
    [SerializeField, Header("有効射程")]
    private float _effectiveRange;

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
