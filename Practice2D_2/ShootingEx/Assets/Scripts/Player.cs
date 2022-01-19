using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    Animator anim;

    void Awake()
    {
        // 초기화
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((isTouchRight && h == 1) || (isTouchLeft && h == -1))
        {
            h = 0;
        }

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
        {
            v = 0;
        }

        Vector3 curPos = transform.position; // 현재위치
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; // 다음에 이동해야할 위치 // Transform이동에는 Time.DeltaTime 사용

        transform.position = curPos + nextPos;

        // 애니메이션
        if((Input.GetButtonDown("Horizontal")) || (Input.GetButtonUp("Vertical")))
        {
            // h가 float형이니 강제 형변환해서 넘김
            anim.SetInteger("Input", (int)h);
        }
    }

    // OnTriggerEnter2D로 플래그 세우기
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 경계에 닿게 된다면
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break; 
                case "Right":
                    isTouchRight = true;
                    break;

            }
        }  
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 경계에서 떼어내면
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;

            }
        }
    }
}
