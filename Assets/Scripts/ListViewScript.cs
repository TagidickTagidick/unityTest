using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.IO;

public class ListViewScript : MonoBehaviour
{

    public GameObject cardPrefab, createdCard;
    public Texture tex;

    void Awake()
    {
        for (int i = 0; i < TextInput.models.Length; i++)
        {
            StartCoroutine(DownloadImage("http://api.ar-education.xyz" + TextInput.models[i].previewLink));
        }
    }

    void Start()
    {
        for (int i = 0; i < TextInput.models.Length; i++)
        {
            createdCard = Instantiate(cardPrefab, this.transform);
            createdCard.transform.GetChild(0).GetComponent<TMP_Text>().text = TextInput.models[i].name;
            createdCard.transform.GetChild(2).GetComponent<TMP_Text>().text = TextInput.models[i].description;
        }
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        Debug.Log(MediaUrl);
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl))
        {
            request.SetRequestHeader("Authorization", $"Bearer {TextInput.token}");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                tex = DownloadHandlerTexture.GetContent(request);
                createdCard.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
            }
        }
    }
}
