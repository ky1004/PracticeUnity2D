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
            // 점프키를 누른상태 && 점프중이 아닌 상태 였을 때 발동 (무한점프 제어)
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetBool("IsJumping", true);
        }

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

        // Animation
        if(Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            // 단위값이 0일때 : 멈췄을 때
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
        // 경사로를 넘지 못하는 경우엔 Vector2.right * h의 뒤에 * 2를 작성한다
        rigid.AddForce(Vector2.right * h * 2, ForceMode2D.Impulse);


        // y축 값에 0을 넣으면 점프시에 멈출 수 있으니 rigid.velocity.y를 넣음
        if(rigid.velocity.x > maxSpeed) // Right
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed*(-1)) // Left
        {
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }

        // Landing Platform
        if(rigid.velocity.y < 0) // y값이 음수일 경우에만
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            // 단위벡터 : 1
            // 빔을 쏘고 맞은 오브젝트에 대한 정보
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                // RayCastHit변수의 콜라이더로 검색 확인 기능
                if (rayHit.distance < 0.5f)
                {
                    // 확인을 위한 코드 Debug.Log(rayHit.collider.name);
                    ani.SetBool("IsJumping", false);
                }
            }
        }
        
    }
}
