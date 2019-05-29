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
    public float y;
    int stat;

    Touch touch;
    // Start is called before the first frame update
    
    new void Start()
    {
        y = 0;
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
        
        if (Input.touchCount > 0 && chk == true) //인식 완료 + 터치
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) 
            {
                
                if(count == 0)
                {
                    count = 1;
                    stat = Robot_stat(1);
                }
            }
        }
        
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.poster") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            stat = Robot_stat(2);
        }
        if (stat == 2)
        {
            y = y + 0.01f;
            robot.transform.Rotate(0, y, 0);
        }
        
    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        //throw new System.NotImplementedException();
        Debug.Log("포스터 인식"+this.name);
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            ScriptEnable(true);
            
        }
        else
        {
            y = 0;
            count = 0; // 같은 태그 중복검색을 위한 초기화
            stat = Robot_stat(0);
            robot.transform.localPosition = new Vector3(0.277f, -0.057f, 0);
            transform.rotation = Quaternion.Euler(0,0,0);
            ScriptEnable(false);
        }

    }

    void ScriptEnable(bool _enabled)
    {
        chk = _enabled;
    } 
    
    int Robot_stat(int state)
    {
        switch (state)
        {
            case 0:
                animator.SetInteger("state", state);
                break;
            case 1:
                animator.SetInteger("state", state);
                stat = state;
                break ;
            case 2:
                animator.SetInteger("state", state);
                stat = state;
                break;
        }
        return state;
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
