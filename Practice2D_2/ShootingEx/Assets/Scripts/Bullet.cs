using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ� ���� ��踦 ���� ���ο� �±� ����
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject); // ������Ʈ ����
        }
        
    }
}
