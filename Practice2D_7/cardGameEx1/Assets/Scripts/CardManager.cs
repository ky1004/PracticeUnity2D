using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // 매니저는 하나만 존재하기 때문에 싱글톤으로 만들어줌
    
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;

    List<Item> itemBuffer;

    /* Buffer에 있는 아이템을 하나씩 뽑아올 수 있다
     카드를 뽑을 때 마다 맨 앞에 있는 인덱스를 뽑아서 RemoveAt(0)번 후 return
     큐가 하는 일과 똑같다
     그리고 리스트로 관리하게 함
     */
    public Item PopItem()
    {
        // 카드가 없으면 다시 setup해서 100개를 다시 채워넣는다
        if (itemBuffer.Count == 0)
            SetupItemBuffer();

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    void SetupItemBuffer()
    {
        itemBuffer = new List<Item>();
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for (int j = 0; j <item.percent; j++)
            {
                itemBuffer.Add(item);
            }
        }
        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    void Start()
    {
        SetupItemBuffer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            print(PopItem().name);
    }
}
