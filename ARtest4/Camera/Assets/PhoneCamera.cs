using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;
    public RawImage background;
    public AspectRatioFitter fit;

    public Text text;
    public Text resultText;
    private int resWidth;
    private int resHeight;
    string path;
    public byte[] imageByte; //스크린샷을 Byte로 저장.Texture2D use 
    //public Text textbox;
    public GameObject panel;
    public GameObject button;
    // Use this for initialization

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        button.SetActive(true);
        resultText.text = "Loading...";

        //textbox.gameObject.SetActive(false); // ocr결과 나타나줄 부분 비활성화
        resWidth = Screen.width;
        resHeight = Screen.height;
        if (!Directory.Exists(Application.persistentDataPath + "/AR_ScreenShot"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/AR_ScreenShot");
        }
        path = Application.persistentDataPath + "/AR_ScreenShot/";
        //Debug.Log(path);

        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            //Debug.Log("카메라가 인식되지 않음");
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
            //Debug.Log("후면 카메라 찾을 수 없음");
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
        panel.SetActive(false);
        string name;
        name = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        //panel.SetActive(true); // 테스트용 잠깐
        //text.text = path;
        //UnityEngine.ScreenCapture.CaptureScreenshot(name); PC에서는 잘 됨
        StartCoroutine("CaptureIt");
        
        //StartCoroutine("PostNetworkingWithWWW");
        //textbox.gameObject.SetActive(false); // ocr결과 나타나줄 부분 비활성화
    }

    public void ClickPanelButton()
    {
        panel.SetActive(false);
    }

    //구버전
    /*
    IEnumerator PostNetworkingWithWWW()
    {
        /////// 전송 부분 //////
        WWWForm form = new WWWForm();
        form.AddField("test", "Jo"); // string , byte 배열이면 form.AddBinaryData("이름","바이트배열");
        form.AddBinaryData("img", imageByte, "screenshot.png", "image/png");
        WWW www = new WWW("13.125.173.0/python/test.php", form);
        yield return www;
        Debug.Log(www.text);
    }
    */
    //신버전
    /*
    IEnumerator PostNetworkingWithWWW()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("test", "jo"));
        form.Add(new MultipartFormDataSection("test2", "ppap"));
        form.Add(new MultipartFormFileSection("plz", imageByte, "test.png", "Image/png"));
        Debug.Log("이미지 전송");
        UnityWebRequest webRequest = UnityWebRequest.Post("13.125.173.0/python/test.php", form);
        yield return webRequest.SendWebRequest();
        string result = webRequest.downloadHandler.text;
        Debug.Log(result);
    }
    */


    IEnumerator CaptureIt()
    {
        yield return new WaitForEndOfFrame();
        string name = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
        //말 그대로입니다. 현제 화면을 픽셀 단위로 읽음
        resultText.text = "Loading...";
        panel.SetActive(true);
        tex.Apply();
        imageByte = tex.EncodeToPNG();
        //읽어 드린 픽셀을 Byte[] 에 PNG 형식으로 인코딩
        DestroyImmediate(tex);
        //Byte[]에 넣었으니 Texture 2D 객체는 파괴(메모리관리) 
        File.WriteAllBytes(name, imageByte); // 안드로이드 저장 시 이렇게 써야됨
        Debug.Log("이미지 저장 완료");

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("test", "jo"));
        form.Add(new MultipartFormDataSection("test2", "ppap"));
        form.Add(new MultipartFormFileSection("plz", imageByte, "test.png", "Image/png")); // 이미지 이름을 test.png로 전송함
        Debug.Log("이미지 전송");
        UnityWebRequest webRequest = UnityWebRequest.Post("http://54.180.99.23:8000/ocrapp/", form);
        yield return webRequest.SendWebRequest();
        string result = webRequest.downloadHandler.text;
        Debug.Log("결과"+result);
        resultText.text = result;
        //textbox.text = " ";
        //textbox.text = result;
        //textbox.gameObject.SetActive(false);
    }
}
