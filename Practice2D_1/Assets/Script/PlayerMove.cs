using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
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
        // Jump
        if (Input.GetButtonDown("Jump") && !ani.GetBool("IsJumping"))
        {
            // ����Ű�� �������� && �������� �ƴ� ���� ���� �� �ߵ� (�������� ����)
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetBool("IsJumping", true);
        }

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
        // ���θ� ���� ���ϴ� ��쿣 Vector2.right * h�� �ڿ� * 2�� �ۼ��Ѵ�
        rigid.AddForce(Vector2.right * h * 2, ForceMode2D.Impulse);


        // y�� ���� 0�� ������ �����ÿ� ���� �� ������ rigid.velocity.y�� ����
        if(rigid.velocity.x > maxSpeed) // Right
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed*(-1)) // Left
        {
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }

        // Landing Platform
        if(rigid.velocity.y < 0) // y���� ������ ��쿡��
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            // �������� : 1
            // ���� ��� ���� ������Ʈ�� ���� ����
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                // RayCastHit������ �ݶ��̴��� �˻� Ȯ�� ���
                if (rayHit.distance < 0.5f)
                {
                    // Ȯ���� ���� �ڵ� Debug.Log(rayHit.collider.name);
                    ani.SetBool("IsJumping", false);
                }
            }
        }
        
    }
}
