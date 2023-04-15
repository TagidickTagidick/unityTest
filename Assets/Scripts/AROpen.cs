using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AROpen : MonoBehaviour
{
    public void TaskOnClick()
    {
        SceneManager.LoadScene("AR Scene");
    }
}
