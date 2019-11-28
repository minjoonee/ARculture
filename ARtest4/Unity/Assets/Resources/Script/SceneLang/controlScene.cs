using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controlScene : languageSet
{
    string SceneName;
    // 1: 한국어 2: 중국어 3: 영어 4: 일본어
    // Start is called before the first frame update
    void Start()
    {
        SceneName = SceneManager.GetActiveScene().name;
        base.ReadLang();

        switch (LangNum)
        {
            case 1:
                KoreanSetting(SceneName);
                break;
            case 2:
                ChineseSetting(SceneName);
                break;
            case 3:
                EnglishSetting(SceneName);
                break;
            case 4:
                JapaneseSetting(SceneName);
                break;
            default:
                if (LangNum < 0)
                {
                    string str = Application.systemLanguage.ToString();
                    if (str.Equals("Korean"))
                    {
                        KoreanSetting(SceneName);
                    }
                    else if (str.Equals("Chinese"))
                    {
                        ChineseSetting(SceneName);
                    }
                    else if (str.Equals("Japanese"))
                    {
                        JapaneseSetting(SceneName);
                    }
                    else if (str.Equals("English"))
                    {
                        EnglishSetting(SceneName);
                    }
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (SceneName.Equals("Start"))
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {
                        Application.Quit();
                    }
                }
                else if (SceneName.Equals("Main") || SceneName.Equals("poster"))
                {
                    goToMenu();
                }
                else if (SceneName.Equals("menu") || SceneName.Equals("gallery") || SceneName.Equals("howToUse") || SceneName.Equals("language"))
                {
                    goToStart();
                }
                else if (SceneName.Equals("stamp"))
                {
                    goTogallery();
                }
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
    public void goToStart()
    {
        SceneManager.LoadScene("Start");
    }
    public void goTogallery()
    {
        SceneManager.LoadScene("gallery");
    }
    public void goTohowToUse()
    {
        SceneManager.LoadScene("howToUse");
    }
    public void goTolanguage()
    {
        SceneManager.LoadScene("language");
    }
}
