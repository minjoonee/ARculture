using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace FreeTts
{
    // TTS 매니저, 여기서 안드로이드 플러그인을 이용해서
    // Android TTS 함수를 불러올 수 있음.
	public class FreeTtsManager: MonoBehaviour
	{
		public Action<FreeTtsResult> OnSpeakComplete = delegate {};

		[DllImport("__Internal")]
		private static extern void Speech (string text, string language, float rate, float pitch);
		[DllImport("__Internal")]
		private static extern void Stop ();

        // 새로운 텍스트 스피칭 시작
		public static FreeTtsManager Create(string text, string language, float rate, float pitch)
		{
            // 게임 오브젝트로 tts를 추가해준다.
			var tts = new GameObject("FreeTtsManager").AddComponent<FreeTtsManager>();
			if (!Application.isEditor)
			{
				switch (Application.platform)
				{
                        // IOS 일때는 xcode 이용할 수 있도록 한다.
					case RuntimePlatform.IPhonePlayer:
						Speech(text, language, rate, pitch);
						break;

                        // 안드로이드 플랫폼일 때
					case RuntimePlatform.Android:
						var nativeDialog = new AndroidJavaClass ("com.wapa5pow.freettsplugin.TtsManagerPlugin");
						var unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
							nativeDialog.CallStatic (
								"speech",
								text,
								language,
								rate,
								pitch
							);
						}));
						break;
				}
			} else
			{
				tts.CallCallbackLater();
			}
            // 합성된 음성을 출력할 수 있다...
			return tts;
		}

        // 음성 출력 도중에 스탑 버튼 누르면 호출됨.
		public void StopSpeech()
		{
			if (!Application.isEditor)
			{
				switch (Application.platform)
				{
					case RuntimePlatform.IPhonePlayer:
						Stop();
						break;
                        // 안드로이드 일때
					case RuntimePlatform.Android:
						var nativeDialog = new AndroidJavaClass ("com.wapa5pow.freettsplugin.TtsManagerPlugin");
						var unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
							nativeDialog.CallStatic (
								"stop"
							);
						}));
						break;
				}
			} else
			{
				OnCallBack("cancel");
			}
		}

        // 아래 콜백 함수들

		public void CallCallbackLater()
		{
			StartCoroutine(OnEditorCallBack("finish"));
		}

		IEnumerator OnEditorCallBack(string result)
		{
			yield return new WaitForSeconds(1);
			OnCallBack(result);
		}


		public void OnCallBack(string result)
		{
			Debug.Log("OnCallBack: " + result);
			if (result == "finish")
			{
				OnSpeakComplete(FreeTtsResult.Finish);
				Destroy(gameObject);
			} else if (result == "cancel")
			{
				OnSpeakComplete(FreeTtsResult.Cancel);
				Destroy(gameObject);
			}
		}
	}

}