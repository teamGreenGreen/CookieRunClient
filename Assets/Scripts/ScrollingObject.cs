using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    // ���߿� �÷��̾� �ӵ��� ����ؼ� �ٲ�� ����
    public float speed = 1f;

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
