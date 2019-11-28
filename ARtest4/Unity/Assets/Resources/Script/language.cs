using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class languageSet : MonoBehaviour
{
    public Text appName;

    public Text setLanguage;
    public Text setKorean;
    public Text setEnglish;
    public Text setJapanese;
    public Text setChinese;

    public Text ARstart;
    public Text galleryIndex;
    public Text howToUse;

    public Text viewPoster;
    public Text viewImage;
    public Text viewOCR;
    

    string git = "https://github.com/minjoonee/ARculture";

    public int LangNum;

    public void KoreanSetting(string SceneName)
    {
        if (SceneName.Equals("Start"))
        { 
            appName.text = "AR 미술관";

            ARstart.text = "AR 카메라";
            setLanguage.text = "언어 설정";
            galleryIndex.text = "전시관 목록";
            howToUse.text = "이용 방법";
        }

        if (SceneName.Equals("gallery"))
        {
            galleryIndex.text = "전시관 목록";
        }

        if (SceneName.Equals("language"))
        {
            setLanguage.text = "언어 설정";
            setKorean.text = "한국어";
            setEnglish.text = "영어";
            setJapanese.text = "일본어";
            setChinese.text = "중국어";

        }

        if (SceneName.Equals("howToUse"))
        {
        }


        if (SceneName.Equals("menu"))
        {
            viewImage.text = "미술품 보기";
            viewPoster.text = "포스터 보기";
            viewOCR.text = "이미지로 번역하기";
        }

        if (SceneName.Equals("stamp"))
        {
        }
    }

    public void JapaneseSetting(string SceneName)
    {
        if (SceneName.Equals("Start"))
        {
            appName.text = "AR 美術館";
            ARstart.text = "ARカメラ";
            galleryIndex.text = "展示会リスト";
            setLanguage.text = "言語設定";
            howToUse.text = "使用方法";
        }

        if (SceneName.Equals("gallery"))
        {
            galleryIndex.text = "展示会リスト";
        }

        if (SceneName.Equals("language"))
        {
            setLanguage.text = "言語設定";
            setKorean.text = "韓国語";
            setEnglish.text = "英語";
            setJapanese.text = "日本語";
            setChinese.text = "中国語";
        }

        if (SceneName.Equals("howToUse"))
        {
        }

        if (SceneName.Equals("menu"))
        {
            viewImage.text = "美術品を見る";
            viewPoster.text = "ポスターを見る";
            viewOCR.text = "画像翻訳する";
        }

        if (SceneName.Equals("stamp"))
        {
        }
    }
    
    public void EnglishSetting(string SceneName)
    {
        if (SceneName.Equals("Start"))
        {
            appName.text = "AR culture";
            ARstart.text = "AR Camera";
            galleryIndex.text = "Exhibition List";
            setLanguage.text = "Language Setting";
            howToUse.text = "How To Use";
        }

        if (SceneName.Equals("gallery"))
        {
            galleryIndex.text = "Exhibition List";
        }

        if (SceneName.Equals("language"))
        {
            setLanguage.text = "Language Setting";
            setKorean.text = "Korean";
            setEnglish.text = "English";
            setJapanese.text = "Japanese";
            setChinese.text = "Chinese";
        }

        if (SceneName.Equals("howToUse"))
        {
        }

        if (SceneName.Equals("menu"))
        {
            viewImage.text = "View Artwork";
            viewPoster.text = "View Poster";
            viewOCR.text = "Image Translate";
        }
        
        if (SceneName.Equals("stamp"))
        {
        }

    }

    public void ChineseSetting(string SceneName)
    {
        if (SceneName.Equals("Start"))
        {
            appName.text = "AR 美术馆";
            ARstart.text = "AR 相机";
            galleryIndex.text = "展览清单";
            setLanguage.text = "语言设定";
            howToUse.text = "怎么用";
        }

        if (SceneName.Equals("gallery"))
        {
            galleryIndex.text = "展览清单";
        }

        if (SceneName.Equals("language"))
        {
            setLanguage.text = "语言设定";
            setKorean.text = "韩语";
            setEnglish.text = "英文";
            setJapanese.text = "日文";
            setChinese.text = "中文";
        }

        if (SceneName.Equals("howToUse"))
        {
        }

        if (SceneName.Equals("menu"))
        {
            viewImage.text = "查看图稿";
            viewPoster.text = "查看海报";
            viewOCR.text = "翻译图片";
        }

        if (SceneName.Equals("stamp"))
        {
        }
    }

    public void ReadLang()
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
}
