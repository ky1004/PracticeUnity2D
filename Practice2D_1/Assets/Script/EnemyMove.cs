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

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y); // 0.2f�� ����
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // �������� : 1
        // ���� ��� ���� ������Ʈ�� ���� ����
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove *= -1; // �ݴ���� ��ȯ
            CancelInvoke(); // �۵����� Invoke�Լ� �ߴ�
            Invoke("Think", 5);
        }
    }


    void Think()
    {
        // nextMove�� �������� ���������� �����ִ���(-1, 0, 1)
        nextMove = Random.Range(-1, 2); // �ִ밪�� �������� ���Ե��� �ʴ´�

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime); // ������ ������ �ð� ���� ���� ������ �Լ� ȣ�� 
    }
}
