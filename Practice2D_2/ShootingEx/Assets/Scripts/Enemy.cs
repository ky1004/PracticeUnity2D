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
        rigid.velocity = Vector2.down * speed; // velocity값을 할당하여 속도 부여
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        // 평소 스프라이트는 0, 피격은 1
        spriteRenderer.sprite = sprites[1];
        // 시간차 함수 Invoke
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

            // 플레이어의 총알 피격 시 총알 삭제
            Destroy(collision.gameObject);
        }
    }
}
