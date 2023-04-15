using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using UnityEngine.SceneManagement;

public class TextInput : MonoBehaviour
{
    public static string name;
    public static string description;
    public static Model[] models;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadStringInput(string s)
    {
        if (s.Length == 8)
        {
            StartCoroutine(GetRequest("http://auth.ar-education.xyz/realms/ar_edu/protocol/openid-connect/token", s));
        }
    }

    IEnumerator GetRequest(string uri, string value)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "password");
        form.AddField("client_id", "ArCore");
        form.AddField("username", "457101af-6b98-4587-93c4-6b216325d0b5");
        form.AddField("password", "123");
        form.AddField("scope", "School");

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string token = JsonUtility.FromJson<Token>(www.downloadHandler.text).access_token;
                using (UnityWebRequest ddd = UnityWebRequest.Get($"http://api.ar-education.xyz/school/lessons/active/{value}"))
                {
                    ddd.SetRequestHeader("Authorization", $"Bearer {token}");
                    yield return ddd.SendWebRequest();

                    if (ddd.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log(ddd.downloadHandler.text);
                        name = Lesson.CreateFromJSON(ddd.downloadHandler.text).name;
                        description = Lesson.CreateFromJSON(ddd.downloadHandler.text).description;
                        models = Lesson.CreateFromJSON(ddd.downloadHandler.text).models;
                        Debug.Log(models[0].name);
                        SceneManager.LoadScene("Scene B");
                    }
                    else
                    {
                        Debug.Log(ddd.error);
                    }
                }
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
}

public class Token
{
    public string access_token;
}

[System.Serializable]
public class Lesson
{
    public string name;
    public string description;
    public Model[] models;

    public static Lesson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Lesson>(jsonString);
    }
}

[System.Serializable]
public class Model
{
    public string name;
    public string description;

    public static Model CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Model>(jsonString);
    }
}