using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class AttendanceManager : MonoBehaviour
{
    private int count;
    private string[] rewards;
    GameObject layout; 
    // 캔버스가 활성화 되면 켜진다.
    private void OnEnable()
    {


        // TODO : 서버에 연결하여 출석 요청 및 Count와 보상 목록 얻어오기
        count = 14;
        rewards = new string[] { "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1" };


        Transform refresh = transform.Find("RefreshRemain");
        refresh.GetComponent<Text>().text = $"{19-count+1}일 후 갱신됩니다.";
        layout = transform.Find("Layout").gameObject;

        foreach (Transform child in layout.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < rewards.Length; i++)
        {
            GameObject prefab = Resources.Load("Prefabs/AttendanceItem") as GameObject;
            GameObject item = MonoBehaviour.Instantiate(prefab) as GameObject;
            item.transform.localScale = new Vector3(1f, 1f, 1f);
            item.transform.SetParent(layout.transform, false);

            Transform itemImage = item.transform.Find("Item_Image");
            Transform itemDays = item.transform.Find("Item_Days");
            Transform itemCount = item.transform.Find("Item_Count");
            Transform check = item.transform.Find("Checked");
            Transform dayCount = itemDays.transform.Find("Day_Count");

            Sprite _sprite = Resources.Load<Sprite>($"UI/Attendance/{rewards[i].Split(":")[0]}");
            Image image = itemImage.GetComponent<Image>();
            image.sprite = _sprite;
            itemCount.GetComponent<Text>().text = $"{rewards[i].Split(":")[1]}";
            dayCount.GetComponent<Text>().text = $"{i+1}일";

            if (i+1 <= count)
            {
                Color colorItem = item.GetComponent<Image>().color;
                colorItem.a = 0.8f;
                item.GetComponent<Image>().color = colorItem;

                Color colorDays = itemDays.GetComponent<Image>().color;
                colorDays.a = 0.8f;
                itemDays.GetComponent<Image>().color = colorDays;

                Color colorImage = itemImage.GetComponent<Image>().color;
                colorImage.a = 0.8f;
                itemImage.GetComponent<Image>().color = colorImage;

                check.gameObject.SetActive(true);
            }
            else
            {
                check.gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
