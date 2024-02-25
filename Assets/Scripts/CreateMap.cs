using System;
using System.Collections.Generic;
using System.Text.RegularExpressions; 
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.Build.Content;

public class CreateMap : MonoBehaviour
{
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";                   // 줄 바꿈 문자열
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // 쉼표로 구분된 각 열을 분할하기 위해 사용

    [SerializeField]
    static Dictionary<int, GameObject> objects;
    public enum EObjectInfo
    {
        ID,
        Name,
        Type,
        ScorePoint,
        MoneyPoint,
    }

    // Start is called before the first frame update
    void Start()
    {
        //UpdateObjectInfo();
        //// UpdateMapCSV();

        //string dir = "csv/SingleMap";
        //TextAsset data = Resources.Load(dir) as TextAsset;
        //var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        //for(int i = 1; i < lines.Length - 1; i++)
        //{
        //    var curInfo = Regex.Split(lines[i], SPLIT_RE);
        //    int id = Convert.ToInt32(curInfo[0]);
        //    float x = Convert.ToSingle(curInfo[1]);
        //    float y = Convert.ToSingle(curInfo[2]);

        //    if (objects.ContainsKey(id))
        //    {
        //        GameObject newObject = objects[id];
        //        if (newObject != null)
        //        {
        //            Vector3 pos = new Vector3(x, y, 0);
        //            Instantiate(newObject, pos, Quaternion.identity);
        //        }
        //        else
        //        {
        //            Debug.Log("SingleMap.csv 파일에 id 번호와 매칭되는 게임 오브젝트 정보가 없습니다. id 번호 : " + id);
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("SingleMap.csv 파일에 해당 id 번호가 없습니다. id 번호 : " + id);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    // csv 파일 내용을 바탕으로 프리팹 생성
    void CreatePrefabs()
    {
        string dir = "csv/Objects";
        TextAsset itemData = Resources.Load(dir) as TextAsset;

        if (itemData == null)
        {
            Debug.Log("Objects.csv의 경로가 잘못되었습니다.");
            return;
        }

        string[] itemDataLines = Regex.Split(itemData.text, LINE_SPLIT_RE);
        int maxIdx = itemDataLines.Length;

        for (var i = 1; i < maxIdx - 1; i++)
        {
            var values = Regex.Split(itemDataLines[i], SPLIT_RE);

            GameObject newObject = new GameObject(values[(int)EObjectInfo.Name]);

            SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();
            Sprite sprite = Resources.Load<Sprite>("Objects/" + values[(int)EObjectInfo.Name]);
            if (sprite == null)
            {
                Debug.Log("Objects/ 에 해당 이름을 가진 sprite가 존재하지 않습니다. sprite 이름 : " + values[(int)EObjectInfo.Name]);
            }

            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = 800;

            if (values[(int)EObjectInfo.Type] == "Item")
            {
                Item itemComponent = newObject.AddComponent<Item>();
                itemComponent.ID = Convert.ToInt32(values[(int)EObjectInfo.ID]);
                itemComponent.Name = values[(int)EObjectInfo.Name];
                itemComponent.Type = values[(int)EObjectInfo.Type];
                itemComponent.ScorePoint = Convert.ToInt32(values[(int)EObjectInfo.ScorePoint]);
                itemComponent.MoneyPoint = Convert.ToInt32(values[(int)EObjectInfo.MoneyPoint]);
            }
            else if (values[(int)EObjectInfo.Type] == "Land")
            {
                InGameObject inGameObjComponent = newObject.AddComponent<InGameObject>();
                inGameObjComponent.ID = Convert.ToInt32(values[(int)EObjectInfo.ID]);
                inGameObjComponent.Name = values[(int)EObjectInfo.Name];
                inGameObjComponent.Type = values[(int)EObjectInfo.Type];
                newObject.AddComponent<Parallax>();
                newObject.AddComponent<Ground>();
                newObject.AddComponent<BoxCollider2D>();
            }

            string prefabPath = "Assets/Resources/Obj/" + values[(int)EObjectInfo.Name] + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(newObject, prefabPath);

            Destroy(newObject);
        }
    }

    // Resources/Obj에서 프리팹 가져와 맵에 id와 gameobject 저장
    void UpdateObjectInfo()
    {
        CreatePrefabs();

        objects = new Dictionary<int, GameObject>();
        string prefabDir = "Obj";

        GameObject[] objectArray = Resources.LoadAll<GameObject>(prefabDir);

        foreach (GameObject obj in objectArray)
        {
            InGameObject inGameObject = obj.GetComponent<InGameObject>();
            if (inGameObject != null)
            {
                int id = inGameObject.ID;
                objects.Add(id, obj);
            }
        }
    }

    void UpdateMapCSV()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject[] allGameObjects = currentScene.GetRootGameObjects();

        string dir = "Assets/Resources/csv/SingleMap.csv";
        StreamWriter writer = new StreamWriter(dir, false);
        string format = "ID,X,Y";
        writer.WriteLine(format);

        foreach (GameObject rootObj in allGameObjects)
        {
            if (rootObj.name == "Map")
            {
                foreach (Transform childTransform in rootObj.transform)
                {
                    GameObject childObj = childTransform.gameObject;
                    if (childObj != null)
                    {
                        InGameObject gameObject = childObj.GetComponent<InGameObject>();

                        if (gameObject != null)
                        {
                            string position = gameObject.ID + "," + childTransform.position.x + "," + childTransform.position.y;
                            writer.WriteLine(position);
                        }
                    }
                }
            }
        }

        writer.Close();
    }
}