using System;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public int ID;
    public string WeaponType;
    public string Name;
    public int MaxDamage;
    public int TortalAmmo;
    public int MagSize;
    public int Firerate;
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
    public int TortalAmmo;
    public int MagSize;
    public int Firerate;
    public int ReloadTime;
    public int Range;
}
public enum WeaponTypeData
{
    None,
    AssultRifle,
    ShotGun,
    SniperRifle,
    SubmachineGun,
    HandGun,
    AntiTankMissile,
}
