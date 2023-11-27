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
                // �����̃X�N���v�^�u���I�u�W�F�N�g��T��
                var existingObject = Array.Find(existingAssets, obj => obj.name == weaponData.Name);
                if (existingObject != null && existingObject is Weapon)
                {
                    // �����̃X�N���v�^�u���I�u�W�F�N�g���X�V
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

                    EditorUtility.SetDirty(weapon); // �ύX��ۑ�
                }
                else
                {
                    // �X�N���v�^�u���I�u�W�F�N�g�����݂��Ȃ��ꍇ�͐V�K�쐬
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

            Debug.Log("JSON�f�[�^��ǂݍ��݁AScriptableObject�ɕϊ����܂����B");
        }
        else
        {
            Debug.LogError("JSON�t�@�C�����A�^�b�`����Ă��܂���B");
        }
    }
}
