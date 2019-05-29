using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.IO;
using LitJson;

public class PosterManager : MonoBehaviour, ITrackableEventHandler
{
    public GameObject robot;
    public Animator animator;

    bool chk = true;
    private TrackableBehaviour track;
    string firstDisplay; // "카메라로 포스터를 비춰보세요."
    int count;

    Touch touch;
    // Start is called before the first frame update
    
    new void Start()
    {
        
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
        int y = 0;
        
        if (Input.touchCount > 0 && chk == true) //인식 완료 + 터치
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) 
            {
                
                if(count == 0)
                {
                    count = 1;
                    Robot_stat(1);
                }
                else if(count == 1)
                {
                    robot.transform.Rotate(0, y, 0);
                    Robot_stat(2);
                    y++;
                    if (y == 360) { y = 0; }
                }
            }
            
        }
        
        

    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        //throw new System.NotImplementedException();
        count = 0; // 같은 태그 중복검색을 위한 초기화
        Debug.Log("포스터 인식"+this.name);
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            ScriptEnable(true);
            
        }
        else
        {
            Robot_stat(0);
            transform.rotation = Quaternion.Euler(0,0,0);
            ScriptEnable(false);
        }

    }

    void ScriptEnable(bool _enabled)
    {
        chk = _enabled;
    }
    
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
        }
    }
    /*
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
    */
}
