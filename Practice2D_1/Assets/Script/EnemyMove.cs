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
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y); // 0.2f는 간격
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // 단위벡터 : 1
        // 빔을 쏘고 맞은 오브젝트에 대한 정보
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove *= -1; // 반대방향 전환
            CancelInvoke(); // 작동중인 Invoke함수 중단
            Invoke("Think", 5);
        }
    }


    void Think()
    {
        // nextMove가 왼쪽인지 오른쪽인지 멈춰있는지(-1, 0, 1)
        nextMove = Random.Range(-1, 2); // 최대값은 랜덤값에 포함되지 않는다

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime); // 설정한 랜덤한 시간 범위 내에 지정된 함수 호출 
    }
}
