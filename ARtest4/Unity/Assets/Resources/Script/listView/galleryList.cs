using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class galleryList : MonoBehaviour
{
    static string sellected;
    public GameObject ButtonObject;
    public Transform content;
    public List<Item> ItemList;
    // Start is called before the first frame update
    void Start()
    {
        Binding();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Binding()
    {
        GameObject btnItem;
        ItemObj temp;

        // 외부에서 정보를 받기 위해서는 ItemList 값을 외부에서 받아올 필요가 있음.
        // 서버와 통신하여 ItemList 값을 정해주는 과정 필요.
        // 또는 바로 데이터를 받아와서 해당 위치에 넣어줘야된다.
        /*
         * 서버에서 받아야 되는 정보 목록
         * 0. 현재 관람가능한 전시회 수 (int 값)
         *  - 1. 전시회 이름
         *  - 2. 전시회 포스터이미지
         */
        foreach (Item item in ItemList)
        {
            btnItem = Instantiate(ButtonObject) as GameObject;  
            // 해당 오브젝트를 인스턴트화하여 외부 프리팹을 받는다.
         
            temp = btnItem.GetComponent<ItemObj>();

            temp.name.text = item.galleryName;
            temp.info.text = item.Info;
            temp.Face.sprite = item.galleryImage;
            temp.Item.onClick = item.OnItemClick;
            temp.Item.name = item.galleryName;

            btnItem.transform.SetParent(content);
            // content 위치에 해당 오브젝트를 동적으로 추가한다.
        }

        Debug.Log("item Binding");
    }

    public void ItemClick_Result()
    {
        // 클릭되었을 때 클릭된 전시회 정보를 받아와서 다음 신에 전달한다.
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        // sendMessage 함수를 통해서 다음 씬으로 넘겨줌.
        sellected = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene("stamp");
    }
}
