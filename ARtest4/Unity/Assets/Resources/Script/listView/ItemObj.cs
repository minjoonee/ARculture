using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ItemObj : MonoBehaviour
{
    public Button Item;
    public Image Face;
    public Text name;
    public Text info;

    public void ItemClick_Result()
    {
        // 클릭되었을 때 클릭된 전시회 정보를 받아와서 다음 신에 전달한다.
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        // sendMessage 함수를 통해서 다음 씬으로 넘겨줌.
        variable.placeNum = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene("stamp");
    }

}
