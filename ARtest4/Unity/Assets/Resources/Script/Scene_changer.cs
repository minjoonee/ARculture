using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_changer : MonoBehaviour
{
    public Text MainText;
    public Text PosterText;
    public int LangNum; // 1: 한국어 2: 중국어 3: 영어 4: 일본어
    private void Start()
    {
        ReadLang();
    }
    void ReadLang()
    {
        LangNum = -1;
        string line = "";// 한줄씩 입력받을 변수
        FileStream ReadL = new FileStream(Application.persistentDataPath + "/ARculture" + "/Language.txt", FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader sL = new StreamReader(ReadL);
        while ((line = sL.ReadLine()) != null)
        {
            LangNum = int.Parse(line);
        }
        sL.Close();
        ReadL.Close();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
               // Application.Quit();
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                jo.Call("goBack");
            }
        }
        if (LangNum < 0)
        {
            string str = Application.systemLanguage.ToString();
            if (str.Equals("Korean"))
            {
                MainText.text = "미술품 보기";
                PosterText.text = "포스터 보기";
            }
            else if (str.Equals("Chinese"))
            {
                MainText.text = "查看图稿";
                PosterText.text = "查看海报";
            }
            else if (str.Equals("English"))
            {
                MainText.text = "View artwork";
                PosterText.text = "View poster";
            }
            else if (str.Equals("Japanese"))
            {
                MainText.text = "美術品を見る";
                PosterText.text = "ポスターを見る";
            }
        }
        else
        {
            switch (LangNum)
            {
                case 1:
                    MainText.text = "미술품 보기";
                    PosterText.text = "포스터 보기";
                    break;
                case 2:
                    MainText.text = "查看图稿";
                    PosterText.text = "查看海报";
                    break;
                case 3:
                    MainText.text = "View artwork";
                    PosterText.text = "View poster";
                    break;
                case 4:
                    MainText.text = "美術品を見る";
                    PosterText.text = "ポスターを見る";
                    break;
            }
        }
    }


    public void goToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void goToPoster()
    {
        SceneManager.LoadScene("poster");
    }
    public void goToMenu()
    {
        SceneManager.LoadScene("menu");
    }

}
