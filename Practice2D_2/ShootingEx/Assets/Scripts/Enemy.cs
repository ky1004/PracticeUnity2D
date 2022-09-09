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

    /* player의 로직을 enemy에 재활용 */
    public float maxShotDelay; // 실제 딜레이(값 높으면 높을수록 총알 딜레이 간격 증가)
    public float curShotDelay; // 한발 쏜 후의 딜레이
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
        // rigid.velocity = Vector2.down * speed; // velocity값을 할당하여 속도 부여
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

            // 플레이어의 총알 피격 시 총알 삭제
            Destroy(collision.gameObject);
        }
    }

    void Fire()
    {
        // 장전이 안됐을 때
        if (curShotDelay < maxShotDelay)
        {
            return;
        }
        if(enemyname == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position; // 목표물 방향 = 목표물 위치 - 자신의 위치
            rigid.AddForce(dirVec, ForceMode2D.Impulse);
        }
        else if(enemyname == "L")
        {
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f); // 목표물 방향 = 목표물 위치 - 자신의 위치
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f); // 목표물 방향 = 목표물 위치 - 자신의 위치
            // normalized : 벡터가 단위 값(1)로 변환된 변수
            // 방향은 그대로 유지, 크기는 1로
            rigidR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);
        }

        curShotDelay = 0; // 총알 쏜 다음에는 딜레이 변수 0으로 초기화
    }

    void Reload()
    {
        // 딜레이 변수에 Time.deltaTime을 계속 더하여 시간 계산
        curShotDelay += Time.deltaTime;
    }
}
