using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text attackTMP;
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;

    public Item item;
    bool isFront; // �յ� �Ǵ��ϴ� ����

    public void Setup(Item item, bool isFront)
    {
        this.item = item;
        this.isFront = isFront;

        if (this.isFront)
        {
            // �ո��� ��� ������ �ְ�
            character.sprite = this.item.sprite;
            nameTMP.text = this.item.name;
            attackTMP.text = this.item.attack.ToString();
            healthTMP.text = this.item.health.ToString();
        }
        else
        {
            // �޸��� ���� ������ ���� �ʴ´�
            card.sprite = cardBack;
            nameTMP.text = "";
            attackTMP.text = "";
            healthTMP.text = "";
        }
    }
}
