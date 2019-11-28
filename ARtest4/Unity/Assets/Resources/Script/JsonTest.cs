using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class JsonTest : TtsForm
{
    // Start is called before the first frame update

    public ItemInfo[] item;
    public int LangNum;
    public class ItemInfo
    {
        public string num;
        public string artist;
        public string name;
        public string info;
        public string place;
        public ItemInfo(string n, string ar, string name, string info, string place)
        {
            this.num = n;
            this.artist = ar;
            this.name = name;
            this.info = info;
            this.place = place;
        }
    }

    void LoadJson(string _fileName)
    {
        Debug.Log("This system is in " + Application.systemLanguage);
        string LoadName = "";
        string Jsonstring = "";
        if (_fileName.StartsWith("jb"))
        {
            TextAsset txtAsset;
            string str = Application.systemLanguage.ToString(); // 기기의 기본 언어값을 가져온다.
            ReadLang();
            if (LangNum < 0)
            {
                if (str.Equals("Korean"))
                {
                    base.languageField = "ko-KR";
                    txtAsset = (TextAsset)Resources.Load("jb-expl"); // 인식한 파일명과 같은걸로 찾아준다.
                }
                else if (str.Equals("Chinese"))
                {
                    base.languageField = "zh-cn";
                    txtAsset = (TextAsset)Resources.Load("jb-chn");
                }
                else if (str.Equals("English"))
                {
                    base.languageField = "en-US";
                    txtAsset = (TextAsset)Resources.Load("jb-eng");
                }
                else if (str.Equals("Japanese"))
                {
                    base.languageField = "ja-JP";
                    txtAsset = (TextAsset)Resources.Load("jb-jpn");
                }
                else
                {
                    base.languageField = "en-US";
                    txtAsset = (TextAsset)Resources.Load("jb-eng"); // 어떤 것도 속하지 않을 때는 영어로.
                }
            }
            else
            {
                switch (LangNum)
                {
                    case 1:
                        base.languageField = "ko-KR";
                        txtAsset = (TextAsset)Resources.Load("jb-expl"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;
                    case 2:
                        base.languageField = "zh-cn";
                        txtAsset = (TextAsset)Resources.Load("jb-chn"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;
                    case 3:
                        base.languageField = "en-US";
                        txtAsset = (TextAsset)Resources.Load("jb-eng"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;
                    case 4:
                        base.languageField = "ja-JP";
                        txtAsset = (TextAsset)Resources.Load("jb-jpn"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;
                    default:
                        base.languageField = "en-US";
                        txtAsset = (TextAsset)Resources.Load("jb-eng"); // 인식한 파일명과 같은걸로 찾아준다.
                        break;

                }
            }
            
            //xmlDoc.LoadXml(txtAsset.text);
            //Jsonstring = File.ReadAllText(Application.dataPath + "/Resources/jb-expl.json");
            Debug.Log("경로 지정 : "+txtAsset.text);
            //Jsonstring = File.ReadAllText(txtAsset.text);
            Jsonstring = txtAsset.text;
            LoadName = "jb";
            Debug.Log("jsonstring 저장" + Jsonstring);
        }
        
        
        JsonData itemData = JsonMapper.ToObject(Jsonstring);
        //Debug.Log(itemData[LoadName][0]["num"].ToString()+" "+itemData[LoadName][0]["info"].ToString()+"  Json불러오기");
        //Debug.Log("개수"+itemData[LoadName].Count);

        item = new ItemInfo[itemData[LoadName].Count];
        for(int i=0; i<itemData[LoadName].Count; i++) {
            item[i] = new ItemInfo(itemData[LoadName][i]["num"].ToString(), itemData[LoadName][i]["artist"].ToString(), itemData[LoadName][i]["name"].ToString(), itemData[LoadName][i]["info"].ToString(), itemData[LoadName][i]["place"].ToString());
        }


        for (int i = 0; i < item.Length; i++)
            Debug.Log(item[i].num + item[i].artist + item[i].name + item[i].info + item[i].place);
    }
    void Start()
    {
        LoadJson("jb01");
    }
    // Update is called once per frame
    void Update()
    {
    }
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

}