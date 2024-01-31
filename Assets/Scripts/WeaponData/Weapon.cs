using System;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public int ID;
    public string WeaponType;
    public string Name;
    public int MaxDamage;
    public int TotalAmmo;
    public int MagSize;
    public int FireRate;
    public int ReloadTime;
    public int Range;
}

[Serializable]
public class WeaponCollection
{
    public WeaponData[] Data;
}

public class Weapon : ScriptableObject
{ 
    public int ID;
    public WeaponTypeData WeaponType;
    public string Name;
    public int MaxDamage;
    public int TotalAmmo;
    public int MagSize;
    public int FireRate;
    public int ReloadTime;
    public int Range;
}
public enum WeaponTypeData
{
    None,
    AssaultRifle,
    ShotGun,
    SniperRifle,
    SubmachineGun,
    HandGun,
    AntiTankMissile,
}
