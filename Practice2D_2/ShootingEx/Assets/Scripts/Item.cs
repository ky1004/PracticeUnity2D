using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    Rigidbody2D rigid;

    void Awake()
    {
        // �ӵ� �߰�
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * 2;
    }

    void Update()
    {
        
    }
}
