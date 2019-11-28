using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.IO;
using LitJson;

public class PosterManager : MonoBehaviour, ITrackableEventHandler
{
    private IEnumerator coroutine;
    public GameObject robot;
    public Animator animator;

    public float robot_xPos = 0.237f;

    bool chk = true;
    private TrackableBehaviour track;
    string firstDisplay; // "카메라로 포스터를 비춰보세요."
    int count;
    public bool jump;

    Touch touch;
    // Start is called before the first frame update
    
    new void Start()
    {
        coroutine = CheckAnimator();
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
                 Robot_stat(1);
            }
        }
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.poster") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
        {
            Robot_stat(2);
        }
        
        
    }

    IEnumerator CheckAnimator()
    {
        while(!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.poster2"))
        {
            yield return null;
        }
        while(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.poster2"))
        {
            //애니메이션 재생 중 실행되는 부분
            robot.transform.Rotate(0, 1, 0);
            Debug.Log("빙글빙글!");
            yield return null;
        }
        yield return null;
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
            Robot_stat(0);
            robot.transform.localPosition = new Vector3(robot_xPos, -0.057f, 0);
            robot.transform.localRotation = Quaternion.Euler(0,270,0);
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
                StopCoroutine(coroutine);
                break;
            case 1:
                animator.SetInteger("state", state);
                StartCoroutine(coroutine);
                break ;
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
