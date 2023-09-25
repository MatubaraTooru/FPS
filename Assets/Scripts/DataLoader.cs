using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : MonoBehaviour
{
    [SerializeField] string _url;
    private void Start()
    {
        StartCoroutine(LoadDataAsync());
    }

    private IEnumerator LoadDataAsync()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(_url);

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("HTTPリクエストエラー: " + webRequest.error);
        }
        else
        {
            string jsonData = webRequest.downloadHandler.text;

            // JSONデータをAssetsフォルダー直下にテキストファイルとして保存
            string filePath = Path.Combine(Application.dataPath, "data.json");
            File.WriteAllText(filePath, jsonData);

            Debug.Log("JSONデータを保存しました：" + filePath);
        }
    }
}
