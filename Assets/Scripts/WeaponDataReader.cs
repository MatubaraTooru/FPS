using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;
using UnityEditor;

public class WeaponDataReader : MonoBehaviour
{
    private IEnumerator LoadDataAsync()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("https://script.google.com/macros/s/AKfycbzfhDceeVA8wvRpsjh0pXNtQ7KzgNbD0tfItGHS6BOTzGuOU4HDk0yZlTliecA__q9u/exec");

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("HTTP���N�G�X�g�G���[: " + webRequest.error);
        }
        else
        {
            string jsonData = webRequest.downloadHandler.text;

            // JSON�f�[�^��Resources�t�H���_�[�ɕۑ�
            string resourcesPath = "Assets/Resources/WeaponData.json";
            File.WriteAllText(resourcesPath, jsonData);

            // AssetDatabase���X�V����Unity�G�f�B�^���Ńt�@�C�����m�F�ł���悤�ɂ���
            UnityEditor.AssetDatabase.Refresh();

            Debug.Log("JSON�t�@�C����ۑ����܂����F" + resourcesPath);

            WeaponDataJsonLoader.LoadData();
        }
    }
    public void DataLoad() => StartCoroutine(LoadDataAsync());
}

[CustomEditor(typeof(WeaponDataReader))]
public class WeaponDataLoaderInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WeaponDataReader l = target as WeaponDataReader;
        string t = "�X�v���b�h�V�[�g�ǂݍ���";

        EditorGUI.BeginDisabledGroup(l.IsInvoking());
        if (GUILayout.Button(t))
        {
            l.DataLoad();
        }
        EditorGUI.EndDisabledGroup();
    }
}
