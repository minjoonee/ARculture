


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class TTSservice : MonoBehaviour
{
    private float TimeLeft = 2.0f;
    private float nextTime = 0.0f;

    
    public AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        Debug.Log("음성테스트 시~작!");
        StartCoroutine("DownloadAudio");
    }

    IEnumerator DownloadAudio()
    {
        //string googleUrl = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=1024&client=tw-ob&q=+" + "Hello%20how%20are%20you" + "&tl=En-gb";
        string googleUrl = "http://api.voicerss.org/?key=675f12414bc04f5ea4c6850dd994c415&hl=en-us&src=Hello";
        WWW www = new WWW(googleUrl);
        yield return www;
        audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + TimeLeft;
            StartCoroutine("DownloadAudio");
        }

        
    }
}
