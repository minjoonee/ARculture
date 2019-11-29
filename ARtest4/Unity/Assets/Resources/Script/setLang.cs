using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class setLang : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setKorean()
    {
        setLanguage(1);
    }


    public void setChinese()
    {
        setLanguage(2);
    }

    public void setEnglish()
    {
        setLanguage(3);
    }

    public void setJapanese()
    {
        setLanguage(4);
    }

    public void setLanguage(int LangNum)
    {
        FileStream writerL = new FileStream(Application.persistentDataPath + "/ARculture" + "/Language.txt", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter wL = new StreamWriter(writerL);
        variable.LangNum = LangNum;
        wL.WriteLine(LangNum);
        wL.Flush();
        wL.Close();
        writerL.Close();

        SceneManager.LoadScene("Start");
    }
}
