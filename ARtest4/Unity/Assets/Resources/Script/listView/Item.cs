using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public string galleryName;
    public string place;
    [TextArea]
    public string Info;
    public Sprite galleryImage;
    public Button.ButtonClickedEvent OnItemClick;
}