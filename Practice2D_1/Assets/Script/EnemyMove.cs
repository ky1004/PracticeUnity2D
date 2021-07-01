using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    void Think()
    {
        // nextMove가 왼쪽인지 오른쪽인지 멈춰있는지(-1, 0, 1)
        nextMove = Random.Range(-1, 2); // 최대값은 랜덤값에 포함되지 않는다
        Invoke("Think", 5); // 5초 뒤에 지정된 함수 호출 
    }
}
