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
            //StartCoroutine(DownloadImage("https://bipbap.ru/wp-content/uploads/2021/11/1619541010_52-oir_mobi-p-nyashnie-kotiki-zhivotnie-krasivo-foto-57.jpg"));
        }
    }

    void Start()
    {
        for (int i = 0; i < TextInput.models.Length; i++)
        {
            createdCard = Instantiate(cardPrefab, this.transform);
            createdCard.transform.GetChild(0).GetComponent<TMP_Text>().text = TextInput.models[i].name;
            //StartCoroutine(DownloadImage("http://api.ar-education.xyz" + TextInput.models[i].previewLink));
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

            //byte[] bytes = request.downloadHandler.data;
            //File.WriteAllBytes(Application.dataPath + "/Export.jpeg", bytes);

            if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                tex = DownloadHandlerTexture.GetContent(request);
                //tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                createdCard.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
            }
        }
    }
}
