using System;
using UnityEditor;
using UnityEngine;

public static class WeaponDataJsonLoader
{
    public static void LoadData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("WeaponData");

        if (jsonFile != null)
        {
            var path = "Assets/Resources";
            var existingAssets = AssetDatabase.LoadAllAssetsAtPath($"{path}/WeaponData.asset");

            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            var weaponCollection = JsonUtility.FromJson<WeaponCollection>(jsonFile.text);

            foreach (var weaponData in weaponCollection.Data)
            {
                // 既存のスクリプタブルオブジェクトを探す
                var existingObject = Array.Find(existingAssets, obj => obj.name == weaponData.Name);
                if (existingObject != null && existingObject is Weapon)
                {
                    // 既存のスクリプタブルオブジェクトを更新
                    var weapon = (Weapon)existingObject;
                    weapon.ID = weaponData.ID;
                    weapon.WeaponType = (WeaponTypeData)Enum.Parse(typeof(WeaponTypeData), weaponData.WeaponType);
                    weapon.Name = weaponData.Name;
                    weapon.MaxDamage = weaponData.MaxDamage;
                    weapon.TortalAmmo = weaponData.TortalAmmo;
                    weapon.MagSize = weaponData.MagSize;
                    weapon.Firerate = weaponData.Firerate;
                    weapon.ReloadTime = weaponData.ReloadTime;
                    weapon.Range = weaponData.Range;

                    EditorUtility.SetDirty(weapon); // 変更を保存
                }
                else
                {
                    // スクリプタブルオブジェクトが存在しない場合は新規作成
                    var obj = ScriptableObject.CreateInstance<Weapon>();
                    obj.ID = weaponData.ID;
                    obj.WeaponType = (WeaponTypeData)Enum.Parse(typeof(WeaponTypeData), weaponData.WeaponType);
                    obj.Name = weaponData.Name;
                    obj.MaxDamage = weaponData.MaxDamage;
                    obj.TortalAmmo = weaponData.TortalAmmo;
                    obj.MagSize = weaponData.MagSize;
                    obj.Firerate = weaponData.Firerate;
                    obj.ReloadTime = weaponData.ReloadTime;
                    obj.Range = weaponData.Range;

                    AssetDatabase.CreateAsset(obj, $"{path}/{weaponData.Name}.asset");
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("JSONデータを読み込み、ScriptableObjectに変換しました。");
        }
        else
        {
            Debug.LogError("JSONファイルがアタッチされていません。");
        }
    }
}
