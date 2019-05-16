using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_position : MonoBehaviour
{
    public TextMesh Chktext;
    Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /* 자식 확인해서 backgroundplane 추출
        Debug.Log("child count = " + this.transform.childCount);
        if (this.transform.childCount > 3)
        {
            FindObjectPlane();
        }
        */
    }
    void FindObjectPlane()
    {
        Debug.Log("x = " + transform.Find("BackgroundPlane").localScale.x);
        Debug.Log("y = " + transform.Find("BackgroundPlane").localScale.y);
        Debug.Log("z = " + transform.Find("BackgroundPlane").localScale.z);
        vector.x = transform.Find("BackgroundPlane").localScale.x * -1;  // backgroundPlane의 scale x가 block position의 x
        vector.y = transform.Find("BackgroundPlane").localScale.z;// backgroundPlane의 scale z가 block position의 y
        vector.z = 4.99f;

        //Chktext.transform.localPosition = vector;
        //Chktext.text = "aaaaaaaaaaaaaaaaaaa  " + vector.x.ToString()+"vs"+ transform.Find("BackgroundPlane").localScale.x.ToString();
    }
}
