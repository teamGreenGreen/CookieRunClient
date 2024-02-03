using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameObject : MonoBehaviour
{
    [SerializeField]
    protected int id = 0;
    [SerializeField]
    protected string objectName = "";
    [SerializeField]
    protected string objectType = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return objectName; }
        set { objectName = value; }
    }

    public string Type
    {
        get { return objectType; }
        set { objectType = value; }
    }
}
