using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyname;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    /* player�� ������ enemy�� ��Ȱ�� */
    public float maxShotDelay; // ���� ������(�� ������ �������� �Ѿ� ������ ���� ����)
    public float curShotDelay; // �ѹ� �� ���� ������
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    void Update()
    {
        Fire();
        Reload();
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // rigid = GetComponent<Rigidbody2D>();
        // rigid.velocity = Vector2.down * speed; // velocity���� �Ҵ��Ͽ� �ӵ� �ο�
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        // ��� ��������Ʈ�� 0, �ǰ��� 1
        spriteRenderer.sprite = sprites[1];
        // �ð��� �Լ� Invoke
        Invoke("ReturnSprite",0.1f);

        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            // �÷��̾��� �Ѿ� �ǰ� �� �Ѿ� ����
            Destroy(collision.gameObject);
        }
    }

    void Fire()
    {
        // ������ �ȵ��� ��
        if (curShotDelay < maxShotDelay)
        {
            return;
        }
        if(enemyname == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position; // ��ǥ�� ���� = ��ǥ�� ��ġ - �ڽ��� ��ġ
            rigid.AddForce(dirVec, ForceMode2D.Impulse);
        }
        else if(enemyname == "L")
        {
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f); // ��ǥ�� ���� = ��ǥ�� ��ġ - �ڽ��� ��ġ
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f); // ��ǥ�� ���� = ��ǥ�� ��ġ - �ڽ��� ��ġ
            // normalized : ���Ͱ� ���� ��(1)�� ��ȯ�� ����
            // ������ �״�� ����, ũ��� 1��
            rigidR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);
        }

        curShotDelay = 0; // �Ѿ� �� �������� ������ ���� 0���� �ʱ�ȭ
    }

    void Reload()
    {
        // ������ ������ Time.deltaTime�� ��� ���Ͽ� �ð� ���
        curShotDelay += Time.deltaTime;
    }
}
