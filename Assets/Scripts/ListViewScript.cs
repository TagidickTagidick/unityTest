using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;

public class ListViewScript : MonoBehaviour
{

    public GameObject cardPrefab, createdCard;
    public Texture tex;

    void Start()
    {
        for (int i = 0; i < TextInput.models.Length; i++)
        {
            createdCard = Instantiate(cardPrefab, this.transform);
            createdCard.transform.GetChild(0).GetComponent<TMP_Text>().text = TextInput.models[i].name;
            Debug.Log("http://api.ar-education.xyz" + TextInput.models[i].previewLink);
            StartCoroutine(DownloadImage("http://api.ar-education.xyz" + TextInput.models[i].previewLink));
            createdCard.transform.GetChild(2).GetComponent<TMP_Text>().text = TextInput.models[i].description;
        }
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            createdCard.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
        }
    }
}
