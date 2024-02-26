using Assets.Scripts.DTO;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
    private async void OnEnable()
    {
        // 런타임 중 출석체크 및 게임 오브젝트 생성
        await RecvAndCreateComponent();
        // 클라이언트 UI 정보 업데이트
        await UserInfoData.Instance.RequestUserInfoPostAsync();
    }
    private async Task RecvAndCreateComponent()
    {
        // 서버에 연결하여 출석 요청 및 Count와 보상 목록 얻어오기
        AttendanceRes res = await HttpManager.Instance.Post<AttendanceRes>("attendance/request", null);
        count = res.AttendanceCount;
        rewards = res.Rewards;

        Transform refresh = transform.Find("RefreshRemain");
        refresh.GetComponent<Text>().text = $"{res.RemainDays}일 후 갱신됩니다.";
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
            dayCount.GetComponent<Text>().text = $"{i + 1}일";

            if (i + 1 <= count)
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
}
