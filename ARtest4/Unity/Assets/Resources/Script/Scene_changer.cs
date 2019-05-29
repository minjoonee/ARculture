using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_changer : MonoBehaviour
{
    public Text MainText;
    public Text PosterText;

    private void Start()
    {
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
