using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class LessonScript : MonoBehaviour
{
    public GameObject nameWidget, descriptionWidget;

    void Start()
    {
        nameWidget.GetComponent<TMP_Text>().text = TextInput.name;
        descriptionWidget.GetComponent<TMP_Text>().text = TextInput.description;
    }
}