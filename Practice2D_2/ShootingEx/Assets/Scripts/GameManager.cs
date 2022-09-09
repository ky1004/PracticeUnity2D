using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay; // �ִ� �����̽ð�
    public float curSpawnDelay; // ���� �帣�� �����̽ð�

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject[] gameOverSet;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;    
        
        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }
    
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3); // ��ȯ�� ��
        int ranPoint = Random.Range(0, 9); // ��ȯ�� ��ġ
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player; // �� ���� ���� �÷��̾� ������ �Ѱ��ش�

        if(ranPoint == 5 || ranPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1); // ��������
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1); // ����������
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed*(-1)); // ������ �Ʒ���
        }
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f); // 2�� �� �����ϴ� Invoke����
    }
   void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);
    }
}
