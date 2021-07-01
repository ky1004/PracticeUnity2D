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
        // nextMove�� �������� ���������� �����ִ���(-1, 0, 1)
        nextMove = Random.Range(-1, 2); // �ִ밪�� �������� ���Ե��� �ʴ´�
        Invoke("Think", 5); // 5�� �ڿ� ������ �Լ� ȣ�� 
    }
}
