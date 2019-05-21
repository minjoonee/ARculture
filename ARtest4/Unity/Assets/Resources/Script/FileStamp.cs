
using UnityEngine;
using System.Collections;
using System.IO;
public class FileStamp : MonoBehaviour // 처음 실행했을 때 폴더 생성하는 역할이 되어줄 스크립트
{
    void Start() 
    {
        if (!Directory.Exists(Application.persistentDataPath + "/ARculture"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/ARculture");
        }

        FileStream fs = new FileStream(Application.persistentDataPath + "/ARculture" + "/TestFileName.txt", FileMode.Append, FileAccess.Write);

        StreamWriter sw = new StreamWriter(fs);
      
        //경로 : 내 PC\Samsung Galaxy S7 edge\Phone\Android\data\com.AR.test\files
        /* 
         * 안드로이드에서도 이 경로를 불러와야 되는데 어떻게?
         * 읽는건
         * string str = string.Empty;
         * StreamReader sr = new StreamReader(file);
         * str = sr.ReadLine();
         */
        sw.WriteLine("write test\n");
        sw.WriteLine("add write test"); // 한번에 추가되고 이스케이프 문자 활용 가능
        sw.Flush();
        sw.Close();
        fs.Close();
    }

    void Update()
    {

    }
}