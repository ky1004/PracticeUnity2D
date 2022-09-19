using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* �ӵ��� �Ŀ� ���� */
    public float speed;
    public int power;
    public int maxPower;

    /* �߻� ������ ������ ���� ���� */
    public float maxShotDelay; // ���� ������(�� ������ �������� �Ѿ� ������ ���� ����)
    public float curShotDelay; // �ѹ� �� ���� ������

    /* ����� ���� ���� */
    public int life;
    public int score;

    /* ��ź ���� */
    public int boom;
    public int maxBoom;
    public bool isBoomTime;

    /* �Ѿ� ������Ʈ ���� */
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;

    /* ��� ������ ���� ���� */
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    public GameManager manager;
    public bool isHit;
    Animator anim;

    void Awake()
    {
        // �ʱ�ȭ
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
        Boom();
    }

    // Move��� �Լ��� ĸ��ȭ
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

        Vector3 curPos = transform.position; // ������ġ
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; // ������ �̵��ؾ��� ��ġ // Transform�̵����� Time.DeltaTime ���

        transform.position = curPos + nextPos;

        // �ִϸ��̼�
        if ((Input.GetButtonDown("Horizontal")) || (Input.GetButtonUp("Vertical")))
        {
            // h�� float���̴� ���� ����ȯ�ؼ� �ѱ�
            anim.SetInteger("Input", (int)h);
        }
    }

    void Fire()
    {
        // Fire1 ��ư�� ������ �ʾҴٸ�, �����Ѵ�
        if (!Input.GetButton("Fire1"))
        {
            return;
        }
        // ������ �ȵ��� ��
        if (curShotDelay < maxShotDelay)
        {
            return;
        }

        switch (power)
        {
            case 1:
                /* Power One */
                // Instantiate : �Ű����� ������Ʈ�� �����ϴ� �Լ�
                // ������Ʈ, ��ġ, ����
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                /* Power Two */
                // Vector3.right, left �������͸� ���� ��ġ ����
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

        

        curShotDelay = 0; // �Ѿ� �� �������� ������ ���� 0���� �ʱ�ȭ
    }

    void Reload()
    {
        // ������ ������ Time.deltaTime�� ��� ���Ͽ� �ð� ���
        curShotDelay += Time.deltaTime;
    }

    void Boom()
    {
        // ������ ���콺 Ŭ�� ��
        if (!Input.GetButton("Fire2"))
            return;
        if (isBoomTime)
            return;
        if (boom == 0)
            return;

        boom--;
        isBoomTime = true;
        manager.UpdateBoomIcon(boom);

        // ����Ʈ ��
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);

        // �� ���� (�����̱� ������ FindGameObjectsWithTag�� ��� -s���� �� Ȯ��)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int index = 0; index < enemies.Length; index++)
        {
            Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }

        // �� źȯ ����
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int index = 0; index < bullets.Length; index++)
        {
            Destroy(bullets[index]);
        }
    }

    // OnTriggerEnter2D�� �÷��� �����
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ��迡 ��� �ȴٸ� (�ܰ���)
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
        // ���� �浹 �� �� źȯ�� ��� �ȴٸ�
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
        // ��迡�� �����
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
