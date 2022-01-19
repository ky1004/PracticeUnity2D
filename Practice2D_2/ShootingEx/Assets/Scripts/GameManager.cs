using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay; // 최대 딜레이시간
    public float curSpawnDelay; // 현재 흐르는 딜레이시간

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
        int ranEnemy = Random.Range(0, 3); // 소환될 적
        int ranPoint = Random.Range(0, 5); // 소환될 위치
        Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
    }
}
