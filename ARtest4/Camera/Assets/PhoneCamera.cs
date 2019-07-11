using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;
    public RawImage background;
    public AspectRatioFitter fit;

    public Text text;
    private int resWidth;
    private int resHeight;
    string path;
    // Use this for initialization

    // Start is called before the first frame update
    void Start()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;
        if (!Directory.Exists(Application.persistentDataPath + "/AR_ScreenShot"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/AR_ScreenShot");
        }
        path = Application.persistentDataPath + "/AR_ScreenShot/";
        Debug.Log(path);

        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            Debug.Log("카메라가 인식되지 않음");
            camAvailable = false;
            return;
        }
        for (int i=0; i<devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(backCam == null)
        {
            Debug.Log("후면 카메라 찾을 수 없음");
            return;
        }
        backCam.Play();
        background.texture = backCam;
        camAvailable = true; 


    }

    // Update is called once per frame
    void Update()
    {
        if (!camAvailable) return;
        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;
        float scaleY = backCam.videoVerticallyMirrored ? -1f: 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    public void ClickScreenShot()
    {
        string name;
        name = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        text.text = path;
        //UnityEngine.ScreenCapture.CaptureScreenshot(name); PC에서는 잘 됨
        StartCoroutine("CaptureIt");
        text.text = "저장완료";
    }
    IEnumerator CaptureIt()
    {
        yield return new WaitForEndOfFrame();
        byte[] imageByte; //스크린샷을 Byte로 저장.Texture2D use 
        string name = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        //2d texture객체를 만드는대.. 스크린의 넓이, 높이를 선택하고 텍스쳐 포멧은 스크린샷을 찍기 위해서는 이렇게 해야 한다더군요. 

        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
        //말 그대로입니다. 현제 화면을 픽셀 단위로 읽어 드립니다. 
        tex.Apply();
        //읽어 들인것을 적용. 
        imageByte = tex.EncodeToPNG();
        //읽어 드린 픽셀을 Byte[] 에 PNG 형식으로 인코딩 해서 넣게 됩니다. 
        DestroyImmediate(tex);
        //Byte[]에 넣었으니 Texture 2D 객체는 바로 파괴시켜줍니다.(메모리관리) 
        File.WriteAllBytes(name, imageByte);
        //원하는 경로.. 저같은 경우는 저렇게 했습니다.
    }
}
