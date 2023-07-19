using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    [SerializeField, Header("����̎��")]
    private WeaponType _weaponType = WeaponType.None;
    [SerializeField, Header("����̖��O")]
    private string _weaponName = "None";
    [SerializeField, Header("����̍ő�_���[�W")]
    private float _weaponMaxDamage = 0.0f;
    [SerializeField, Header("�ő及���e��")]
    private int _maxAmmo = 0;
    [SerializeField, Header("�e�q�̑傫��")]
    private int _magSize = 0;
    [SerializeField, Header("�A�ˑ��x")]
    private float _firerate = 0;
    [SerializeField, Header("�đ��U���x")]
    private float _reloadSpeed = 0;
    [SerializeField, Header("�L���˒�")]
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
