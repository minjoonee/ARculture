using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_changer : MonoBehaviour
{
    public void goToMain()
    {
        SceneManager.LoadScene("Main");
        
    }
    public void goToPoster()
    {
        SceneManager.LoadScene("poster");
    }
}
