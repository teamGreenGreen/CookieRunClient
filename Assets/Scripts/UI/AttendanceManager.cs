using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class AttendanceManager : MonoBehaviour
{
    private int count;
    private string[] rewards;
    GameObject layout; 
    // 캔버스가 활성화 되면 켜진다.
    private void OnEnable()
    {
        Debug.Log("enable");
        // TODO : 서버에 연결하여 출석 요청 및 Count와 보상 목록 얻어오기
        count = 14;
        rewards = new string[] { "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1", "diamond:1" };

        layout = transform.Find("Layout").gameObject;
        for (int i = 0; i < rewards.Length; i++)
        {
            GameObject prefab = Resources.Load("Prefabs/AttendanceItem") as GameObject;
            GameObject item = MonoBehaviour.Instantiate(prefab) as GameObject;
            item.transform.parent = layout.transform;
            item.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
