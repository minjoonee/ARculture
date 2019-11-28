using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class stampList : MonoBehaviour
{
    public GameObject stampObject;
    public Transform content;
    public Sprite grayClear;
    public Sprite colorClear;

    List<string> Stamp_List = new List<string>();   // 현재 stamp.txt 파일에 저장된 목록을 담는 변수.

    // Start is called before the first frame update
    void Start()
    {
        stampCheck();
        Binding(variable.placeNum);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Binding(string placeNum)
    {
        JsonTest json = GameObject.Find("StampData").GetComponent<JsonTest>();  //언어에 맞게 json 파일 불러옴.
        GameObject btnItem;
        stampObj temp;

        for (int i = 0; i < json.item.Length; i++) {
            if (placeNum.Equals(json.item[i].place))
            {
                btnItem = Instantiate(stampObject) as GameObject;
                temp = btnItem.GetComponent<stampObj>();

                temp.author.text = json.item[i].artist;
                temp.picture.text = json.item[i].name;

                // 만약 Stamp_List 안에 json.item[i].num 값이 존재하는 경우에는 colorClear 삽입
                if (Stamp_List.Contains(json.item[i].num))
                {
                    temp.clear.sprite = colorClear;
                }
                else
                {
                    temp.clear.sprite = grayClear;
                }
                
                // 아닌 경우에는 grayClear 삽입

                btnItem.transform.SetParent(content);  // content 위치에 stampObj 삽입.
            }
        }
        // 여기서 외부 입력을 받는게 아니라 json 데이터를 받아올 수 있도록 코딩해야됨.
       
        /*
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
        */
        Debug.Log("item Binding");
    }

    void stampCheck()
    {
        Stamp_List.Clear();
        string line = "";// 한줄씩 입력받을 변수

        FileStream Readfs = new FileStream(Application.persistentDataPath + "/ARculture" + "/Stamp.txt", FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader sr = new StreamReader(Readfs);

        
        while ((line = sr.ReadLine()) != null)
        {
            Stamp_List.Add(line); // 모두 읽어서 리스트에 저장해둠.
            Debug.Log(line);
        }
        Debug.Log(Application.persistentDataPath + "/ARculture" + "/Stamp.txt");
        sr.Close();
        Readfs.Close();
    }
}
