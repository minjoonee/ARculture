using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class tutorial : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Tutorial_Start", PlayerPrefs.GetInt("Tutorial_Start", 0));
        if (PlayerPrefs.GetInt("Tutorial_Start") == 0)
        {
            // 앱을 최초로 실행했을 때
            Directory.CreateDirectory(Application.persistentDataPath + "/ARculture/");
            FileStream wr = new FileStream(Application.persistentDataPath + "/ARculture" + "/Language.txt", FileMode.Create, FileAccess.Write);
            StreamWriter wL = new StreamWriter(wr);
            // language.txt 파일을 생성하고, 여기에 현재 사용중인 언어 값을 입력한다.

            string str = Application.systemLanguage.ToString();
            if (str.Equals("Korean"))
            {
                variable.LangNum = 1;
            }
            else if (str.Equals("Chinese"))
            {
                variable.LangNum = 2;
            }
            else if (str.Equals("English"))
            {
                variable.LangNum = 3;
            }
            else if (str.Equals("Japanese"))
            {
                variable.LangNum = 4;
            }

            text.text = "AR 미술관3 + " + PlayerPrefs.GetInt("Tutorial_Start");
            wL.WriteLine(variable.LangNum);
            wL.Flush();

            text.text = "AR 미술관!";
            Debug.Log("Tutorial Start" + variable.LangNum);
            PlayerPrefs.SetInt("Tutorial_Start", 1);
            PlayerPrefs.Save();

            wL.Close();
            wr.Close();
            SceneManager.LoadScene("howToUse");
        }

        else if (PlayerPrefs.GetInt("Tutorial_Start") != 0)
        {
            ReadLang();
            text.text = "AR 미술관?";
            Debug.Log("GoinMull" + variable.LangNum);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void ReadLang()
    {
        string line = "";// 한줄씩 입력받을 변수
        FileStream ReadL = new FileStream(Application.persistentDataPath + "/ARculture" + "/Language.txt", FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader sL = new StreamReader(ReadL);
        while ((line = sL.ReadLine()) != null)
        {
            variable.LangNum = int.Parse(line);
        }
        sL.Close();
        ReadL.Close();
    }
    
}
