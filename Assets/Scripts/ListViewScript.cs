using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using UnityEngine.SceneManagement;

public class ListViewScript : MonoBehaviour
{

    public GameObject cardPrefab;
    public Texture2D tex;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < TextInput.models.Length; i++)
        {
            GameObject createdCard = Instantiate(cardPrefab, this.transform);
            createdCard.transform.GetChild(0).GetComponent<TMP_Text>().text = TextInput.models[i].name;
            Debug.Log(TextInput.models[i].previewLink);
            StartCoroutine(LoadTextureFromServer("http://api.ar-education.xyz" + TextInput.models[i].previewLink));
            createdCard.transform.GetChild(1).GetComponent<Renderer>().material.mainTexture = tex;
            createdCard.transform.GetChild(2).GetComponent<TMP_Text>().text = TextInput.models[i].description;
        }
    }

    IEnumerator LoadTextureFromServer(string url)
    {
        var request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            tex = DownloadHandlerTexture.GetContent(request);
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

            tex = null;
        }

        request.Dispose();
    }


}
