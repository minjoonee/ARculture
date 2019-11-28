using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasSize : MonoBehaviour
{
    public RectTransform top;
    public RectTransform bottom;
    public RectTransform popUp;


    // Start is called before the first frame update
    void Start()
    {
        if (Screen.width == 1440 && Screen.height == 2560)
        {
            top.sizeDelta = new Vector2(0, 696.37f);
            bottom.sizeDelta = new Vector2(0, 696.37f);
            popUp.sizeDelta = new Vector2(1300, 1400);
        }
        else if(Screen.width == 1440 && Screen.height == 2960)
        {
            top.sizeDelta = new Vector2(0, 800);
            bottom.sizeDelta = new Vector2(0, 800);
            popUp.sizeDelta = new Vector2(1200, 1440);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
