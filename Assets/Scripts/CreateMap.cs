using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; 
using UnityEngine;
using UnityEditor;

using static Item;
using Unity.VisualScripting;

public class CreateMap : MonoBehaviour
{
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";                   // 줄 바꿈 문자열
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // 쉼표로 구분된 각 열을 분할하기 위해 사용

    [SerializeField]
    static Dictionary<int, GameObject> prefabInfo;
    public enum EItemInfo
    {
        ID,
        Name,
        ScorePoint,
        MoneyPoint,
    }

    // Start is called before the first frame update
    void Start()
    {
        // Item.csv���� ���� ������ �������� ������ ���� �� ����
        prefabInfo = new Dictionary<int, GameObject>();
        string dir = "csv/Item";
        TextAsset itemData = Resources.Load(dir) as TextAsset;

        if(itemData == null )
        {
            Debug.Log("Item.csv�� ��ΰ� �߸��Ǿ����ϴ�.");
            return;
        }

        string[] itemDataLines = Regex.Split(itemData.text, LINE_SPLIT_RE);

        for (var i = 1; i < itemDataLines.Length - 1; i++)
        {
            var values = Regex.Split(itemDataLines[i], SPLIT_RE);
            if (Convert.ToInt32(values[(int)EItemInfo.ID]) == 0)
                continue;

            GameObject newObject = new GameObject(values[(int)EItemInfo.Name]);
            
            SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();
            Sprite sprite = Resources.Load<Sprite>("Item/" + values[(int)EItemInfo.Name]);
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = 10;

            Item itemComponent = newObject.AddComponent<Item>();
            itemComponent.ID = Convert.ToInt32(values[(int)EItemInfo.ID]);
            itemComponent.Name = values[(int)EItemInfo.Name];
            itemComponent.ScorePoint = Convert.ToInt32(values[(int)EItemInfo.ScorePoint]);
            itemComponent.MoneyPoint = Convert.ToInt32(values[(int)EItemInfo.MoneyPoint]);

            string prefabPath = "Assets/Prefabs/Item/" + values[(int)EItemInfo.Name] + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(newObject, prefabPath);

            prefabInfo.Add(itemComponent.ID, newObject);
            Destroy(newObject);
        }

        // �� ������ �о Ư�� ��ġ�� ������ ��ġ
        dir = "csv/SingleMap";
        TextAsset data = Resources.Load(dir) as TextAsset;
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        var header = Regex.Split(lines[0], SPLIT_RE);

        var jellyTypes = Regex.Split(lines[1], SPLIT_RE);
        var jellyYPos = Regex.Split(lines[2], SPLIT_RE);
        var jellyAmount = Regex.Split(lines[3], SPLIT_RE);
        var obstacle = Regex.Split(lines[4], SPLIT_RE);

        float xPos = -5.0f;

        for (int i = 1; i < jellyTypes.Length; i++)
        {
            int id = Convert.ToInt32(jellyTypes[i]);
            float yPos = Convert.ToSingle(jellyYPos[i]);
            int cnt = Convert.ToInt32(jellyAmount[i]);

            // ID�� gameObject ã��
            if (prefabInfo.ContainsKey(id))
            {
                GameObject newObject = prefabInfo[id];
                if (newObject != null)
                {
                    for (int j = 0; j < cnt; j++)
                    {
                        Vector3 pos = new Vector3(xPos, yPos, 0);
                        Instantiate(newObject, pos, Quaternion.identity);
                        xPos += 0.8f;
                    }
                }
                else
                {
                    Debug.Log("SingleMap.csv 파일에 id 번호와 매칭되는 게임 오브젝트 정보가 없습니다. id 번호 : " + id);
                }
            }
            else
            {
                Debug.Log("SingleMap.csv 파일에 해당 id 번호가 없습니다. id 번호 : " + id);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}