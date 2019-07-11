using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    public new Camera camera;       //보여지는 카메라.
    public Text text;
    private int resWidth;
    private int resHeight;
    string path;
    // Use this for initialization
    void Start()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;
        if (!Directory.Exists(Application.persistentDataPath + "/AR_ScreenShot"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/AR_ScreenShot");
        }
        path = Application.persistentDataPath + "/AR_ScreenShot/";


        //PC 에서는 path = Application.dataPath + "/AR_ScreenShot/";
        Debug.Log(path);
        text.text = path;
    }

    public void ClickScreenShot()
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(path);
        }
        string name;
        name = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(name, bytes);
    }
}