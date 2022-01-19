using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed; // velocity���� �Ҵ��Ͽ� �ӵ� �ο�
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
}
