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
    string filePath, path;

    private void Start()
    {
        filePath = $"{Application.persistentDataPath}/Files/";
        wrapper = new GameObject
        {
            name = "Model"
        };

        //StartCoroutine(GetFileRequest("http://api.ar-education.xyz/project/models/4/content"));
        StartCoroutine(GetFileRequest("http://api.ar-education.xyz/project/models/11/content"));
        //DownloadFile("https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/BoxVertexColors/glTF-Embedded/BoxVertexColors.gltf");
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
        //ResetWrapper();
        GameObject model = Importer.LoadFromFile(path);
        //model.transform.SetParent(wrapper.transform);
    }

    /*IEnumerator GetFileRequest(string url)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.downloadHandler = new DownloadHandlerFile(GetFilePath(url));
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError)
            {
                // Log any errors that may happen
                Debug.Log($"{req.error} : {req.downloadHandler.text}");
            }
            else
            {
                // Save the model into a new wrapper
                LoadModel(path);
            }
        }
    }*/

    IEnumerator GetFileRequest(string url)
    {

        using (UnityWebRequest ddd = UnityWebRequest.Get(url))
        {
            ddd.SetRequestHeader("Authorization", $"Bearer {TextInput.token}");
            yield return ddd.SendWebRequest();

            if (ddd.result == UnityWebRequest.Result.Success)
            {
                //path = GetFilePath(Application.dataPath + "Export");
                Debug.Log(ddd.downloadHandler.data);
                byte[] bytes = ddd.downloadHandler.data;
                File.WriteAllBytes(Application.dataPath + "/Export.gltf", bytes);
                model = Importer.LoadFromFile(Application.dataPath + "/Export.gltf");
                
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
