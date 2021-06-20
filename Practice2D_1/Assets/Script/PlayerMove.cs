using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;
    SpriteRenderer spRenderer;
    Animator ani;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            // �����ִ� ���� rigid.velocity.normalized*(ũ��)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }

        // ���� ��ȯ(Direction Sprite)
        if (Input.GetButtonDown("Horizontal"))
        {
            spRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        // Animation
        if(Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            // �������� 0�϶� : ������ ��
            ani.SetBool("IsWalking", false);
        }
        else
        {
            ani.SetBool("IsWalking", true);
        }
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
