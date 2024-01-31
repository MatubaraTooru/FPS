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
            Debug.LogError("HTTPリクエストエラー: " + webRequest.error);
        }
        else
        {
            string jsonData = webRequest.downloadHandler.text;

            // JSONデータをResourcesフォルダーに保存
            string resourcesPath = "Assets/Resources/WeaponData.json";
            File.WriteAllText(resourcesPath, jsonData);

            // AssetDatabaseを更新してUnityエディタ内でファイルを確認できるようにする
            UnityEditor.AssetDatabase.Refresh();

            Debug.Log("JSONファイルを保存しました：" + resourcesPath);

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
        string t = "スプレッドシート読み込み";

        EditorGUI.BeginDisabledGroup(l.IsInvoking());
        if (GUILayout.Button(t))
        {
            l.DataLoad();
        }
        EditorGUI.EndDisabledGroup();
    }
}
