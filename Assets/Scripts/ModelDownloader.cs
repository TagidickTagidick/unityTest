using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Siccity.GLTFUtility;

public class ModelDownloader : MonoBehaviour
{
    [SerializeField]
    public static GameObject wrapper, model;
    string filePath, path, token;
    public static bool isModelDownloaded = false;

    private void Start()
    {
        isModelDownloaded = false;
        filePath = $"{Application.persistentDataPath}/Files/";
        wrapper = new GameObject
        {
            name = "Model"
        };

        StartCoroutine(GetToken("http://auth.ar-education.xyz/realms/ar_edu/protocol/openid-connect/token"));
    }
    public void DownloadFile(string url)
    {
        path = GetFilePath(url);
        if (File.Exists(path))
        {
            Debug.Log("Found file locally, loading...");
            LoadModel(path);
            return;
        }

        StartCoroutine(GetFileRequest(url));
    }

    string GetFilePath(string url)
    {
        string[] pieces = url.Split('/');
        string filename = pieces[pieces.Length - 1];

        return $"{filePath}{filename}";
    }

    void LoadModel(string path)
    {
        GameObject model = Importer.LoadFromFile(path);
    }

    IEnumerator GetToken(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "password");
        form.AddField("client_id", "ArCore");
        form.AddField("username", "457101af-6b98-4587-93c4-6b216325d0b5");
        form.AddField("password", "123");
        form.AddField("scope", "School");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.Success)
            {
                token = JsonUtility.FromJson<Token>(www.downloadHandler.text).access_token;
                StartCoroutine(GetFileRequest("http://api.ar-education.xyz/project/models/12/content"));
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }

    IEnumerator GetFileRequest(string url)
    {
        isModelDownloaded = false;
        using (UnityWebRequest ddd = UnityWebRequest.Get(url))
        {
            Debug.Log(token);
            ddd.SetRequestHeader("Authorization", $"Bearer {token}");
            yield return ddd.SendWebRequest();

            if (ddd.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(ddd.downloadHandler.data);
                byte[] bytes = ddd.downloadHandler.data;
                File.WriteAllBytes(Application.dataPath + "/Export.gltf", bytes);
                model = Importer.LoadFromFile(Application.dataPath + "/Export.gltf");
                isModelDownloaded = true;
                model.SetActive(false);
                //AnchorCreator.m_AnchorPrefab = model;
            }
            else
            {
                Debug.Log(ddd.error);
            }
        }
    }

    void ResetWrapper()
    {
        if (wrapper != null)
        {
            foreach (Transform trans in wrapper.transform)
            {
                Destroy(trans.gameObject);
            }
        }
    }
}
