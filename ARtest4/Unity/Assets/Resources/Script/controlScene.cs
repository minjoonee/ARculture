using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void goToPoster()
    {
        SceneManager.LoadScene("poster");
    }
    public void goToMenu()
    {
        SceneManager.LoadScene("menu");
    }
    public void goToStart()
    {
        SceneManager.LoadScene("Start");
    }
    public void goTogallery()
    {
        SceneManager.LoadScene("gallery");
    }
    public void goTohowToUse()
    {
        SceneManager.LoadScene("howToUse");
    }
    public void goTolanguage()
    {
        SceneManager.LoadScene("language");
    }
}
