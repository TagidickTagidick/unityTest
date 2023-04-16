using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public void AROpen()
    {
        SceneManager.LoadScene("AR Scene");
    }

    public void CodeEnterOpen()
    {
        SceneManager.LoadScene("CodeInput Scene");
    }

    public void LessonOpen()
    {
        SceneManager.LoadScene("Lesson Scene");
    }
}
