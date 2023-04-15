using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine;

public class ObjFromStream : MonoBehaviour {

    string url = "https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj";

    public GameObject loadedObj, spawnPoint;

    public GameObject CreateObject () {
        //make www
        var www = new WWW(url);
        while (!www.isDone)
            System.Threading.Thread.Sleep(1);
        
        //create stream and load
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
        loadedObj = new OBJLoader().Load(textStream);

        var scaleOfObject = new Vector3(0.025f, 0.025f, 0.025f);
        loadedObj.transform.localScale = scaleOfObject;

        Instantiate(loadedObj, spawnPoint.transform);

        return loadedObj;
	}
}
