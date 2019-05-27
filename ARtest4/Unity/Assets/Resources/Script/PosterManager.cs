using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Xml;
using System.IO;
using LitJson;
public class PosterManager : TtsForm, ITrackableEventHandler
{
    //public GameObject robot;
    //public Animator animator;
    int char_state = 0;
    float char_scale = 0.01f;
    bool Running = false;

    public TextMesh Chktext;
    public GameObject canvas;

    bool chk = true;
    private TrackableBehaviour track;
    string firstDisplay; // "카메라로 포스터를 비춰보세요."
    int count;
    private bool SoundTrackCount = false; // 인식했을 때 사용하는 변수
    

    Touch touch;
    // Start is called before the first frame update
    
    new void Start()
    {
        //robot.transform.localScale = new Vector3(char_scale, char_scale, char_scale);
        
        base.Start();
        track = GetComponent<TrackableBehaviour>();
        if (track)
        {
            track.RegisterTrackableEventHandler(this);
        }

        //LangString();
        Debug.Log("포스터매니저 실행");
    }
    void Update()
    {
        /*
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // 일단 지움
                // Application.Quit();
            }
        }
        if (Input.touchCount > 0 && SoundTrackCount == true) //인식 완료 + 터치
        {
            touch = Input.GetTouch(0);
            robot.transform.localScale = new Vector3(char_scale, char_scale, char_scale);
            if (touch.phase == TouchPhase.Began && base.touchOn == false) // 사운드 트랙 처음 실행했을 때
            {
                Running = false;
                char_scale = 0.1f;
                base.touchOn = true;
                // 설명 이벤트가 실행되면 꾸벅 인사하고 설명하는 동작
                Robot_stat(3);
                
            }

            else if (touch.phase == TouchPhase.Began && base.touchOn == true)
            {
                Running = false;
                char_scale = 0.1f;
                base.touchOn = false;
                // 설명 이벤트를 끝내면 손을 흔들면서 인사하는 동작 반복
                Robot_stat(5);

                //Chktext.text = "터치 이벤트 실행" + touchOn.ToString() + "카운트 : " + base.mycount + "음악 그만\n";
                base.OnStopClick();
            }
        }
        else if (Running == true)
        {
            if (char_scale < 0.1f)
            {
                Robot_stat(1);
                char_scale = char_scale + 0.0003f;
                robot.transform.localScale = new Vector3(char_scale, char_scale, char_scale);
            }
            else if (char_scale >= 0.1f) 
            {
                Robot_stat(2);
            }
        }
        */

    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        //throw new System.NotImplementedException();
        count = 0; // 같은 태그 중복검색을 위한 초기화
        SoundTrackCount = false; // 새로운 이미지 트래킹 시 사운드 초기화
        Debug.Log("포스터 인식"+this.name);
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            ScriptEnable(true);
            Running = true;

            //텍스트랑 캔버스 없애줌
            Chktext.text = "";
            canvas.SetActive(false);
        }
        else
        {
            char_scale = 0.01f;
            //Robot_stat(1);

            ScriptEnable(false);
        }

    }

    void ScriptEnable(bool _enabled)
    {
        chk = _enabled;
    }
    /*
    void Robot_stat(int state)
    {
        switch (state)
        {
            case 0:
                animator.SetInteger("state", state);
                break;
            case 1:
                animator.SetInteger("state", state);
                break;
            case 2:
                animator.SetInteger("state", state);
                break;
            case 3:
                animator.SetInteger("state", state);
                animator.SetTrigger("expl");
                animator.SetBool("stop", false);
                break;
            case 5:
                animator.SetInteger("state", state);
                animator.SetBool("stop", true);
                break;
        }
    }
    */
    void LangString()
    {
        string str = Application.systemLanguage.ToString();
        if (str.Equals("Korean"))
        {
            firstDisplay = "카메라로 포스터를 비춰보세요.";
        }
        else if (str.Equals("Chinese"))
        {
            firstDisplay = "用相机照亮海报。";
        }
        else if (str.Equals("English"))
        {
            firstDisplay = "Use your camera to illuminate posters.";
        }
        else if (str.Equals("Japanese"))
        {
            firstDisplay = "カメラを使ってポスターを照らしましょう。";
        }
    }
}
