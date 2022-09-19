using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay; // 최대 딜레이시간
    public float curSpawnDelay; // 현재 흐르는 딜레이시간

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;    
        
        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        /* UI 점수 업데이트 */
        Player playerLogic = player.GetComponent<Player>();
        // {0:n0} 세자리마다 쉼표로 나눠주는 숫자 양식
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

    }
    
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3); // 소환될 적
        int ranPoint = Random.Range(0, 9); // 소환될 위치
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player; // 적 생성 직후 플레이어 변수를 넘겨준다

        if(ranPoint == 5 || ranPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1); // 왼쪽으로
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1); // 오른쪽으로
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed*(-1)); // 위에서 아래로
        }
    }

    public void UpdateLifeIcon(int life)
    {
        /* UI Life Init Disable */
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        /* UI Life Active */
        for (int index=0; index<life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBoomIcon(int boom)
    {
        /* UI Boom Init Disable */
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }
        /* UI Boom Active */
        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f); // 2초 뒤 실행하는 Invoke설정
    }
   void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }
    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}
