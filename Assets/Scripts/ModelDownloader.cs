using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ModelDownloader : MonoBehaviour
{

    public string modelUrl = "https://free3d.com/dl-files.php?p=54e78ba726be8b012c8b4567&f=0"; // URL of the model on the web server
    private GameObject loadedModel; // the loaded model game object

    void Start()
    {
        StartCoroutine(LoadModel());
    }

    IEnumerator LoadModel()
    {
        WWW www = new WWW(modelUrl);
        yield return www;

        loadedModel = new GameObject();
        loadedModel.AddComponent<MeshFilter>();
        loadedModel.AddComponent<MeshRenderer>();

        loadedModel.GetComponent<MeshFilter>().mesh = new Mesh();
        loadedModel.GetComponent<MeshFilter>().mesh.Clear();
        //ObjImporter newMesh = new ObjImporter();
        //newMesh.ImportMesh(www.text, loadedModel.GetComponent<MeshFilter>().mesh);

        loadedModel.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
    }
}
