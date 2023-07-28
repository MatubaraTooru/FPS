using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/CreateWeaponDataAsset")]
public class WeaponDataAsset : ScriptableObject
{
    [SerializeField]
    private List<WeaponData> _WeaponDataList = new List<WeaponData>();

    public List<WeaponData> WeaponDatas { get => _WeaponDataList; }
}

[Serializable]
public class WeaponData
{
    [SerializeField, Header("�����ID")]
    private int _weaponID = -1;
    [SerializeField, Header("����̎��")]
    private WeaponType _weaponType = WeaponType.None;
    [SerializeField, Header("����̉摜")]
    private Image _weaponImage = null;
    [SerializeField, Header("����̖��O")]
    private string _weaponName = "None";
    [SerializeField, Header("����̍ő�_���[�W")]
    private float _weaponMaxDamage = 0.0f;
    [SerializeField, Header("�ő及���e�q��")]
    private int _maxAmmo = 0;
    [SerializeField, Header("�e�q�̑傫��")]
    private int _magSize = 0;
    [SerializeField, Header("�A�ˑ��x")]
    private float _firerate = 0;
    [SerializeField, Header("�đ��U���x")]
    private float _reloadSpeed = 0;
    [SerializeField, Header("�L���˒�")]
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
