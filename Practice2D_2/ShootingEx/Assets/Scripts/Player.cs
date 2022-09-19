using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* 속도와 파워 변수 */
    public float speed;
    public int power;
    public int maxPower;

    /* 발사 딜레이 로직을 위한 변수 */
    public float maxShotDelay; // 실제 딜레이(값 높으면 높을수록 총알 딜레이 간격 증가)
    public float curShotDelay; // 한발 쏜 후의 딜레이

    /* 목숨과 점수 변수 */
    public int life;
    public int score;

    /* 폭탄 변수 */
    public int boom;
    public int maxBoom;
    public bool isBoomTime;

    /* 총알 오브젝트 변수 */
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;

    /* 경계 구분을 위한 변수 */
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    public GameManager manager;
    public bool isHit;
    Animator anim;

    void Awake()
    {
        // 초기화
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
        Boom();
    }

    // Move라는 함수로 캡슐화
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
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
        if ((Input.GetButtonDown("Horizontal")) || (Input.GetButtonUp("Vertical")))
        {
            // h가 float형이니 강제 형변환해서 넘김
            anim.SetInteger("Input", (int)h);
        }
    }

    void Fire()
    {
        // Fire1 버튼을 누르지 않았다면, 리턴한다
        if (!Input.GetButton("Fire1"))
        {
            return;
        }
        // 장전이 안됐을 때
        if (curShotDelay < maxShotDelay)
        {
            return;
        }

        switch (power)
        {
            case 1:
                /* Power One */
                // Instantiate : 매개변수 오브젝트를 생성하는 함수
                // 오브젝트, 위치, 방향
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                /* Power Two */
                // Vector3.right, left 단위벡터를 더해 위치 조절
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }

        

        curShotDelay = 0; // 총알 쏜 다음에는 딜레이 변수 0으로 초기화
    }

    void Reload()
    {
        // 딜레이 변수에 Time.deltaTime을 계속 더하여 시간 계산
        curShotDelay += Time.deltaTime;
    }

    void Boom()
    {
        // 오른쪽 마우스 클릭 시
        if (!Input.GetButton("Fire2"))
            return;
        if (isBoomTime)
            return;
        if (boom == 0)
            return;

        boom--;
        isBoomTime = true;
        manager.UpdateBoomIcon(boom);

        // 이펙트 온
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);

        // 몹 제거 (다중이기 때문에 FindGameObjectsWithTag를 사용 -s구분 잘 확인)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int index = 0; index < enemies.Length; index++)
        {
            Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }

        // 몹 탄환 제거
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int index = 0; index < bullets.Length; index++)
        {
            Destroy(bullets[index]);
        }
    }

    // OnTriggerEnter2D로 플래그 세우기
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 경계에 닿게 된다면 (외각선)
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
        // 적과 충돌 및 적 탄환에 닿게 된다면
        else if(collision.gameObject.tag == "Enemy"|| collision.gameObject.tag == "EnemyBullet")
        {
            if (isHit)
                return;

            isHit = true;
            life--;
            manager.UpdateLifeIcon(life);

            if(life == 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (power == maxPower)
                        score += 500;
                    else
                        power++;
                    break;
                case "Boom":
                    if (boom == maxBoom)
                        score += 1000;
                    else
                    {
                        boom++;
                        manager.UpdateBoomIcon(boom);
                    }
                    break;
            }
            Destroy(collision.gameObject);
        }
    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
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
