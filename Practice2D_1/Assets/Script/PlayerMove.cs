using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        // Move by Control 
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);


        // y�� ���� 0�� ������ �����ÿ� ���� �� ������ rigid.velocity.y�� ����
        if(rigid.velocity.x > maxSpeed) // Right
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed*(-1)) // Left
        {
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }
    }
}
