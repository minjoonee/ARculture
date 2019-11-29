using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using LitJson;

public class galleryList : MonoBehaviour
{
    public GameObject ButtonObject;
    public Transform content;

    public ItemInfo[] items;

    int LangNum = variable.LangNum;
    public string languageField = "ko-KR";

    public class ItemInfo
    {
        public string galleryName;
        public string place;
        public string info;
        public string sprite;
        public ItemInfo(string gN, string place, string info, string sprite)
        {
            this.galleryName = gN;
            this.place = place;
            this.info = info;
            this.sprite = sprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadJson();
        Binding();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Binding()
    {
        GameObject btnItem;
        ItemObj temp;

        // 외부에서 정보를 받기 위해서는 ItemList 값을 외부에서 받아올 필요가 있음.
        // 서버와 통신하여 ItemList 값을 정해주는 과정 필요.
        // 또는 바로 데이터를 받아와서 해당 위치에 넣어줘야된다.
        /*
         * 서버에서 받아야 되는 정보 목록
         * 0. 현재 관람가능한 전시회 수 (int 값)
         *  - 1. 전시회 이름
         *  - 2. 전시회 포스터이미지
         */
        for (int i = 0; i<items.Length; i++)
        {
            btnItem = Instantiate(ButtonObject) as GameObject;  
            // 해당 오브젝트를 인스턴트화하여 외부 프리팹을 받는다.
         
            temp = btnItem.GetComponent<ItemObj>();

            temp.name.text = items[i].galleryName;
            temp.info.text = items[i].info;
            temp.Face.sprite = Resources.Load<Sprite>("3d/"+items[i].sprite);
            temp.Item.name = items[i].place;

            btnItem.transform.SetParent(content);
            // content 위치에 해당 오브젝트를 동적으로 추가한다.
        }

        Debug.Log("item Binding");
    }
    
    /*
    void ReadLang()
    {
        LangNum = -1;
        string line = "";// 한줄씩 입력받을 변수
        FileStream ReadL = new FileStream(Application.persistentDataPath + "/ARculture" + "/Language.txt", FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader sL = new StreamReader(ReadL);
        while ((line = sL.ReadLine()) != null)
        {
            LangNum = int.Parse(line);
        }
        sL.Close();
        ReadL.Close();
    }
    */

    void LoadJson()
    {
        string tst = "exhibition";
        Debug.Log("This system is in " + Application.systemLanguage);
        string LoadName = "";
        string Jsonstring = "";

        if (tst.StartsWith("ex"))
        {
            TextAsset txtAsset;
            string str = Application.systemLanguage.ToString(); // 기기의 기본 언어값을 가져온다.
            //ReadLang();
            if (LangNum < 0)
            {
                if (str.Equals("Korean"))
                {
                    languageField = "ko-KR";
                    txtAsset = (TextAsset)Resources.Load("idx_expl"); // 인식한 파일명과 같은걸로 찾아준다.
                }
                else if (str.Equals("Chinese"))
                {
                    languageField = "zh-cn";
                    txtAsset = (TextAsset)Resources.Load("idx_chn");
                }
                else if (str.Equals("English"))
                {
                    languageField = "en-US";
                    txtAsset = (TextAsset)Resources.Load("idx_eng");
                }
                else if (str.Equals("Japanese"))
                {
                    languageField = "ja-JP";
                    txtAsset = (TextAsset)Resources.Load("idx_jpn");
                }
                else
                {
                    languageField = "en-US";
                    txtAsset = (TextAsset)Resources.Load("idx_eng"); // 어떤 것도 속하지 않을 때는 영어로.
                }
            }
            else
            {
                switch (LangNum)
                {
                    case 1:
                        languageField = "ko-KR";
                        txtAsset = (TextAsset)Resources.Load("idx_expl"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;
                    case 2:
                        languageField = "zh-cn";
                        txtAsset = (TextAsset)Resources.Load("idx_chn"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;
                    case 3:
                        languageField = "en-US";
                        txtAsset = (TextAsset)Resources.Load("idx_eng"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;
                    case 4:
                        languageField = "ja-JP";
                        txtAsset = (TextAsset)Resources.Load("idx_jpn"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;
                    default:
                        languageField = "en-US";
                        txtAsset = (TextAsset)Resources.Load("idx_eng"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;

                }
            }


            Debug.Log("textAsset :  " + txtAsset.text);

            Jsonstring = txtAsset.text;
            LoadName = "index";

            Debug.Log("jsonstring 저장" + Jsonstring);

        }
        Debug.Log(JsonMapper.ToObject(Jsonstring));

        JsonData itemData = JsonMapper.ToObject(Jsonstring);
        //Debug.Log(itemData[LoadName][0]["num"].ToString()+" "+itemData[LoadName][0]["info"].ToString()+"  Json불러오기");
        //Debug.Log("개수"+itemData[LoadName].Count);

        items = new ItemInfo[itemData[LoadName].Count];
        for (int i = 0; i < itemData[LoadName].Count; i++)
        {
            items[i] = new ItemInfo(
                itemData[LoadName][i]["galleryName"].ToString(), 
                itemData[LoadName][i]["place"].ToString(), 
                itemData[LoadName][i]["Info"].ToString(), 
                itemData[LoadName][i]["sprite"].ToString());
        }


        for (int i = 0; i < items.Length; i++)
            Debug.Log(items[i].galleryName + items[i].place + items[i].info + items[i].sprite);
    }
}
