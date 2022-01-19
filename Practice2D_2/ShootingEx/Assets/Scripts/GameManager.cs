using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay; // �ִ� �����̽ð�
    public float curSpawnDelay; // ���� �帣�� �����̽ð�

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
        int ranPoint = Random.Range(0, 5); // ��ȯ�� ��ġ
        Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
    }
}
