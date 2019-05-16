using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Xml;
public class GameManager : TtsForm, ITrackableEventHandler
{
    public TextMesh Chktext;
    bool chk = true;
    private TrackableBehaviour track;
    string filename;
    string jpg_filename;
    string str;

    int jpg_filename_idx;
    int filename_idx=-1;
    int count;
    private bool SoundTrackCount = false; // 인식했을 때 사용하는 변수
    Touch touch;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        track = GetComponent<TrackableBehaviour>();
        if (track)
        {
            track.RegisterTrackableEventHandler(this);
        }
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // 할꺼 하셈
                Application.Quit();
            }
        }
        if (Input.touchCount>0 && SoundTrackCount == true) //인식 완료 + 터치
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && base.touchOn == false) // 사운드 트랙 처음 실행했을 때
            {
                Chktext.text = "터치 이벤트 실행" + touchOn.ToString() + "카운트 : " + base.mycount + "음악 큐~";
                base.FieldString = str;
                base.OnSpeakClick();
                base.touchOn = true;
                Debug.Log("사운드 실행 후 info : " + str);
            }
            else if(touch.phase == TouchPhase.Began && base.touchOn == true)
            {
                Chktext.text = "터치 이벤트 실행" + touchOn.ToString() + "카운트 : " + base.mycount + "음악 그만";
                base.OnStopClick();
                base.touchOn = false;
            }
        }
    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        //throw new System.NotImplementedException();
        count = 0; // 같은 태그 중복검색을 위한 초기화
        SoundTrackCount = false; // 새로운 이미지 트래킹 시 사운드 초기화
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            ScriptEnable(true);
            //Chktext.text = this.name;
            
         
            filename = this.name;
            filename_idx = filename.IndexOf("-"); // 중복된 이미지파일명에서 -만 추출해가지구 앞에 이름으로 xml파일 검색할 수 있도록 해주는 코드
            
            
            if (filename_idx > 0) // 인덱스가 0보다 적으면 -가 파일명 사이에 있다는 것!
            {
                filename = this.name.Substring(0, filename_idx);
                Debug.Log("- 인덱스 번호 : " + filename_idx);
                filename_idx = 0;
                Debug.Log("파일명 : " + filename + "실제파일명 : "+this.name);
            }
            else
                Debug.Log("xxxxxxxxxx -문자 발견되지않음");
            
            //녹음파일 component 불러와서 실행하는 부분
            //var sound = GameObject.Find(this.name).GetComponent<JB_AudioPlayer>();
            //sound.SoundPlay();

            LoadXML(filename);
        }
        else
        {
            ScriptEnable(false);
            Chktext.text = "      아래 카메라에 전시물을 비춰보세요.";
        }

    }
    void ScriptEnable(bool _enabled)
    {
        chk = _enabled;
    }

    void LoadXML(string _fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("XML/" + _fileName); // 인식한 파일명과 같은걸로 찾아준다. 문제는 파일명과 xml파일명이 달라서 일일히 하나씩 수정
        XmlDocument xmlDoc = new XmlDocument();
        //XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //xmlDoc.AppendChild(xmlDeclaration);
        xmlDoc.LoadXml(txtAsset.text);
        Debug.Log("XML로드 완료!!");
        


        XmlNodeList all_nodes = xmlDoc.SelectNodes("jb");
        XmlNodeList img_all_nodes = xmlDoc.SelectNodes("jb/img");
        foreach (XmlNode node in all_nodes)
        {
            foreach(XmlNode img_node in img_all_nodes)
            {
                IEnumerator ienum = img_node.GetEnumerator();
                while (ienum.MoveNext())
                {
                    Debug.Log("지금 이 파일 이름 : " + this.name);
                    Debug.Log("비교하려는 노드 이름 : " + img_node.SelectNodes("img")[count].InnerText);

                    jpg_filename = img_node.SelectNodes("img")[count].InnerText;
                    jpg_filename_idx = jpg_filename.IndexOf("."); // xml 파일에 .jpg 가 포함되어있는데 그거 제거시켜주려고;;


                    if (jpg_filename_idx > 0) // 인덱스가 0보다 크면 .jpg가 파일명 사이에 있다는 것!
                    {
                        jpg_filename = jpg_filename.Substring(0, jpg_filename_idx);
                        Debug.Log("- 인덱스 번호 : " + jpg_filename_idx);
                        jpg_filename_idx = 0;
                        Debug.Log("파일명 : " + jpg_filename);
                    }
                    else
                        Debug.Log("xxxxxxxxxx . 문자 발견되지않음");

                    //if (_fileName.Equals(img_node.SelectNodes("img")[count].InnerText)) // 파일 이름이랑 img태그 안에 있는 이름이랑 비교해봄
                    if (this.name.Equals(jpg_filename)) // 파일 이름이랑 img태그 안에 있는 이름이랑 비교해봄
                    {
                        //Chktext.text = "\tName : " + node.SelectSingleNode("name").InnerText + "\n" + node.SelectSingleNode("info").InnerText;
                        str = node.SelectSingleNode("info").InnerText;
                        //str = str.Replace("<br>", "\n");
                        str = str.Replace("<br>", "");
                        for(int i=0; i<str.Length; i += 27)
                        {
                            str = str.Insert(i, "\n\t");
                            Debug.Log("개행이 삽입되는 곳의 위치 : " + i);
                            i += 2;
                        }

                        Chktext.text = "\tName : " + node.SelectSingleNode("name").InnerText + "\n\n\t작가 : " + node.SelectSingleNode("artist").InnerText;

                        //화면을 터치했을때 사운드 플레이 동작 실시하기 위해서 변수 삽입
                        SoundTrackCount = true;
                        str = node.SelectSingleNode("artist").InnerText;
                        //base.FieldString = str;
                        //base.OnSpeakClick();
                        Debug.Log("사운드 실행 후 info : " + node.SelectSingleNode("artist").InnerText);
                    }
                    count++;
                }
            }

        }
    }

    
}
