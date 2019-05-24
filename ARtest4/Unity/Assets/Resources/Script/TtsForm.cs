using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

// FreeTts 클래스 적용
using FreeTts;

public class TtsForm : MonoBehaviour
{

	private FreeTtsManager _tts;
	private List<string> _texts = new List<string>();

	[DllImport("__Internal")]
	private static extern void Languages ();
    public string FieldString = " ";
    public float rateField = 1.0f;
    public float pitchField = 1.0f;
    public string languageField = "ko-KR";
    public bool touchOn = false; // 재생 도중 터치 한번 더 할 시에 중지할 수 있도록 해주는 변수, 재생시 true
    public int mycount = 0;

	public void Start ()
	{
        // 여기 인풋필드로 구성된걸 텍스트로 바꾼다음에
        // xml 에서 가져온 값을 바로 넣어주면 될 듯함.
        //_inputField.text = "안녕하세요, 텍스트 변환 합성 음성 출력 테스트입니다.";

        //_rateInputField.text = "1.0";
		//_pitchInputField.text = "1.0";
		//_languageInputField.text = "ko-KR";

        // 안드로이드 플러그인 tts 를 호출할 수 있도록 한다...
		if (!Application.isEditor && Application.platform == RuntimePlatform.Android)
		{
			var nativeDialog = new AndroidJavaClass ("com.wapa5pow.freettsplugin.TtsManagerPlugin");
			var unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			context.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				nativeDialog.CallStatic (
					"initialize",
					context
				);
			}));
		}

        // 추가 언어값 설정으로 보임. 
		//_languageList.SetActive(false);
		//_chooseLanguageButton.enabled = false;
        /*
		if (Application.isEditor)
		{
			AddLanguageButtons("ja-JP,en-US");
		} else
		{
			Languages();
		}
        */
	}
    

    /*
    // 출력할 언어 선택 하는 부분인데 이부분 번역 한다음에 써야되니까 일단 냅둠
    public void AddLanguageButtons(string languages)
	{
		foreach (Transform c in _content.transform)
		{
			Destroy(c.gameObject);
		}

		foreach (var language in languages.Split(','))
		{
			var button = Instantiate(Resources.Load<GameObject>("LanguageButton")).GetComponent <Button>();
			var l = language;
			var t = _content.transform;
			button.onClick.AddListener(() => SetLanguage(l));
			button.transform.SetParent(t);
			button.transform.localScale = Vector3.one;
			button.GetComponentInChildren<Text>().text = l;
		}

		_chooseLanguageButton.enabled = true;
	}
    */

    // 스피킹 시작 버튼에 넣어줬음.
	public void OnSpeakClick()
	{
		Debug.Log("OnSpeakClick");
        
        touchOn = true;
        mycount++;
        //var inputTexts = _inputField.text;

        var inputTexts = FieldString;
        _texts.Clear();
		foreach (var text in inputTexts.Split('.'))
		{
			_texts.Add(text);
		}
		SpeakTextsIfExists();
	}

    // 스피킹 중단에 멈추고싶을때 사용할 버튼
	public void OnStopClick()
	{
		Debug.Log("OnStopClick");
        touchOn = false;
		_texts.Clear();

		if (Application.isEditor)
		{
			return;
		}

		if (_tts != null)
		{
			_tts.StopSpeech();
		}
	}

    /*
    // 언어 선택
	public void OnChooseLanguageClick()
	{
		Debug.Log("OnChooseLanguageClick");

		_languageList.SetActive(true);
	}

    //  언어설정
	public void SetLanguage(string language)
	{
		Debug.Log("SetLanguage: " + language);
		_languageInputField.text = language;
		_languageList.SetActive(false);
	}
    */

    // tts 매니저 호출
    // 이 때 텍스트, 언어, 속도, 높이 값이 들어감.
	private void SpeakTextsIfExists()
	{
		if (_texts.Count < 1)
		{
			return;
		}
        /*
		var language = _languageInputField.text;
		var rate = float.Parse(_rateInputField.text);
		var pitch = float.Parse(_pitchInputField.text);
        */
    var language = languageField;
        var rate = rateField;
        var pitch = pitchField;

		var text = _texts.First();
		_texts.RemoveAt(0);
		_tts = FreeTtsManager.Create(text, language, rate, pitch);
		_tts.OnSpeakComplete += OnSpeakComplete;
	}

    // 말하기 완료되면
	private void OnSpeakComplete(FreeTtsResult result) {
		Debug.Log("OnFinishSpeaking");
        //touchOn = false;
        //parsing result
        switch (result) {
            // 완료
			case FreeTtsResult.Finish:
				Debug.Log ("Finish!!!");
				SpeakTextsIfExists();
				break;
            
            // 취소
			case FreeTtsResult.Cancel:
				Debug.Log ("Finish!!!");
				break;
		}
	}
    /*
    // 언어 설정 꺼지면
	public void CloseLanguageList()
	{
		_languageList.SetActive(false);
	}
    */
}
