using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Siccity.GLTFUtility;

public class ModelDownloader : MonoBehaviour
{

    public static GameObject wrapper, model;
    string filePath, path;

    private void Start()
    {
        filePath = $"{Application.persistentDataPath}/Files/";
        wrapper = new GameObject
        {
            name = "Model"
        };

        DownloadFile("http://api.ar-education.xyz/project/models/2/content");
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
            ddd.SetRequestHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICI5TUlQMTRJR09meGxZUHBHRFFtdnhzaFhYVVM2LXpWZFJmdzY0djB3Z1hzIn0.eyJleHAiOjE2ODIxNzcxNDEsImlhdCI6MTY4MjE3Njg0MSwianRpIjoiYzNkZWViMWYtMDZiOS00OTEyLThlZGYtMmU1OTZlZjdjYTQ2IiwiaXNzIjoiaHR0cDovL2F1dGguYXItZWR1Y2F0aW9uLnh5ei9yZWFsbXMvYXJfZWR1IiwiYXVkIjoiYWNjb3VudCIsInN1YiI6ImQwMzY0MzVlLWU2YjMtNDBiYi04ZDg3LTlhY2NiMmZhYzU2MiIsInR5cCI6IkJlYXJlciIsImF6cCI6IkFyQ29yZSIsInNlc3Npb25fc3RhdGUiOiJlMmI5MjFlZC0yZDBmLTQwNTMtOTNkYS03ZjMyN2UyYmFhZTEiLCJhY3IiOiIxIiwiYWxsb3dlZC1vcmlnaW5zIjpbIioiXSwicmVhbG1fYWNjZXNzIjp7InJvbGVzIjpbImRlZmF1bHQtcm9sZXMtYXItZWR1Iiwib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiIsIlN0dWRlbnQiXX0sInJlc291cmNlX2FjY2VzcyI6eyJBckNvcmUiOnsicm9sZXMiOlsiTGVzc29uVmlldyJdfSwiYWNjb3VudCI6eyJyb2xlcyI6WyJtYW5hZ2UtYWNjb3VudCIsIm1hbmFnZS1hY2NvdW50LWxpbmtzIiwidmlldy1wcm9maWxlIl19fSwic2NvcGUiOiJlbWFpbCBwcm9maWxlIFNjaG9vbCIsInNpZCI6ImUyYjkyMWVkLTJkMGYtNDA1My05M2RhLTdmMzI3ZTJiYWFlMSIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwicHJlZmVycmVkX3VzZXJuYW1lIjoiNDU3MTAxYWYtNmI5OC00NTg3LTkzYzQtNmIyMTYzMjVkMGI1Iiwic2Nob29sTmFtZSI6Ik5pemhuaXlOb3Znb3JvZC8xIn0.iND2I4kBkW4M4zB2IGK464Rmsgqq03TuNq3P11cVSSogqFzGl2ZM5rtyk6XGBgWVe0vSHPpyAhVkkERDZc7PrF4XoHr4sZAjQJNv3eCbCHTH_QGUJcIIyeNTRzVQzvR-bFjEm8pX2cJ5hHxtVaummD-APsc2kpFj4K7b0kM9dcliz_zK3W0TTkzyRs88tHzMQqnlGMbJqXwnAA9Qv4zuqp5hzej8c11IsKQyRcASxYDua4rI_opE9cqkTT-cWlSfrW52akqyblRz_G1OypXkCVRJjP8NDQTaXjmpgosOBOIGVWJ1SuduUoXPQjW37fFndrUzxkmnywc5yhIC3xwBcg");
            yield return ddd.SendWebRequest();

            if (ddd.result == UnityWebRequest.Result.Success)
            {
                //path = GetFilePath(Application.dataPath + "Export");
                byte[] bytes = ddd.downloadHandler.data;
                File.WriteAllBytes(Application.dataPath + "/Export.glb", bytes);
                model = Importer.LoadFromFile(Application.dataPath + "/Export.glb");
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
