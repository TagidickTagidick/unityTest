using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class LessonScript : MonoBehaviour
{
    void Start()
    {
        GameObject nameWidget = transform.GetChild(0).gameObject;
        nameWidget.GetComponent<TMP_Text>().text = TextInput.name;
        GameObject descriptionWidget = transform.GetChild(1).gameObject;
        descriptionWidget.GetComponent<TMP_Text>().text = TextInput.description;

        //Destroy(buttonTemplate);
    }
}