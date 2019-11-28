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
    
    public Text stampGuide;

    public Text htu1;
    public Text htu2;
    public Text htu3;
    public Text htu4;
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
            htu1.text = "그림에 가까이 가져가 보세요.\n\n 귀여운 로봇이 설명해줄거에요.";
            htu2.text = "포스터에 가까이 가져가 보세요.\n\n 누가 있는거 같아요!";
            htu3.text = "언어 설정 페이지를 이용하면.\n\n 원하는 언어로 설정할 수 있습니다.";
            htu4.text = "전시회 목록을 잘 둘러보세요.\n\n 어떤 작품을 봤는지 알 수 있어요!";
        }


        if (SceneName.Equals("menu"))
        {
            viewImage.text = "미술품 보기";
            viewPoster.text = "포스터 보기";
            viewOCR.text = "이미지로 번역하기";
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
            htu1.text = "画像の近くに持ってみましょう。\n\n かわいいロボットが説明してくれるんです。";
            htu2.text = "ポスターの近くに持ってみましょう。\n\n 誰てるんだ！";
            htu3.text = "言語設定ページを使用すると、\n\n 希望の言語に設定することができます。";
            htu4.text = "展覧会のリストをよくお楽しみください。\n\n いくつかの作品を見たのか知ることができます！";
        }

        if (SceneName.Equals("menu"))
        {
            viewImage.text = "美術品を見る";
            viewPoster.text = "ポスターを見る";
            viewOCR.text = "画像翻訳する";
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
            htu1.text = "Take closer to the picture.\n\n cute robot will explain it.";
            htu2.text = "Take closer to the poster.\n\n  I think who's there!";
            htu3.text = "Use the Language Settings page \n\n ou can set your preferred language.";
            htu4.text = "Watch your list of exhibits. \n\n You can see what you saw!";
        }

        if (SceneName.Equals("menu"))
        {
            viewImage.text = "View Artwork";
            viewPoster.text = "View Poster";
            viewOCR.text = "Image Translate";
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
            htu1.text ="靠近图片。\n\n 一个可爱的机器人会解释它";
            htu2.text = "靠近海报。\n\n 我想是谁在里面!";
            htu3.text = "使用语言设置页面。\n\n 您可以设置首选语言";
            htu4.text = "观察您的展览品清单。\n\n 您可以看到自己看到的东西!";
        }

        if (SceneName.Equals("menu"))
        {
            viewImage.text = "查看图稿";
            viewPoster.text = "查看海报";
            viewOCR.text = "翻译图片";
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
