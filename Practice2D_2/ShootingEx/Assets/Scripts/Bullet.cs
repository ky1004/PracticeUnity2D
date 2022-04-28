using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알 제거 경계를 위한 새로운 태그 조건
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject); // 오브젝트 제거
        }
        
    }
}
