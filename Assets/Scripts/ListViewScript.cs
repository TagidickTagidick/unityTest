using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListViewScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject modelsWidget = transform.GetChild(0).gameObject;
        for (int i = 0; i < TextInput.models.Length; i++)
        {
            GameObject modelWidget = Instantiate(modelsWidget, transform);
            modelWidget.transform.GetChild(0).GetComponent<TMP_Text>().text = TextInput.models[i].name;
            modelWidget.GetComponent<Button>().onClick.AddListener(TaskOnClick);
            Destroy(modelsWidget);
        }
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("AR Scene");
    }
}
