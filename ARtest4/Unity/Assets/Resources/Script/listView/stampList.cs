using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stampList : MonoBehaviour
{
    public GameObject stampObject;
    public Transform content;
    public List<stamp> stampLists;
    // Start is called before the first frame update
    void Start()
    {
        Binding();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Binding()
    {
        // 여기서 외부 입력을 받는게 아니라 json 데이터를 받아올 수 있도록 코딩해야됨.
        GameObject btnItem;
        stampObj temp;

        foreach (stamp St in stampLists)    //stamp 목록을 가져와서.
        {
            btnItem = Instantiate(stampObject) as GameObject;
            temp = btnItem.GetComponent<stampObj>();
            // tmp = 현재 스크립트에 입력받은 각 목록 정보들.

            temp.author.text = St.authorName;
            temp.picture.text = St.pictureName;
            temp.clear.sprite = St.clearImage;

            btnItem.transform.SetParent(content);  // content 위치에 stampObj 삽입.
        }

        Debug.Log("item Binding");
    }
}
