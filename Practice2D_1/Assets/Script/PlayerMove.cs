using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;
    SpriteRenderer spRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            // 가고있는 방향 rigid.velocity.normalized*(크기)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }

        // 방향 전환(Direction Sprite)
        if (Input.GetButtonDown("Horizontal"))
        {
            spRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    void FixedUpdate()
    {
        // Move by Control 
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);


        // y축 값에 0을 넣으면 점프시에 멈출 수 있으니 rigid.velocity.y를 넣음
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
