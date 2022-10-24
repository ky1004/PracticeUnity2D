using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    bool timerActive = false; // Ÿ�̸� Ȱ��ȭ ����

    float currentTime; // ���� �ð�
    public int startMinutes; // �ʱⰪ ���� ����
    public Text currentTimeText;

    void Start()
    {
        // sec
        currentTime = startMinutes * 60; 
    }

    void Update()
    {
        // Ȱ��ȭ ������ ��
        if (timerActive == true)
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime <= 0)
            {
                timerActive = false;
                Start();
                Debug.Log("Ÿ�̸� �Ϸ�");
            }
        }
        // �ʸ� �� �ð����� ���� ��ȯ����
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        
        if(time.Minutes < 10)
        {
            if (time.Seconds < 10)
            {
                currentTimeText.text = "0" + time.Minutes.ToString() + ":0" + time.Seconds.ToString();
            }
            else           {
                currentTimeText.text = "0" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
            }
        }
        else
        {
            if (time.Seconds < 10)
            {
                currentTimeText.text = "0" + time.Minutes.ToString() + ":0" + time.Seconds.ToString();
            }
            else
            {
                currentTimeText.text = "0" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
            }
        }

        // currentTimeText.text = currentTime.ToString();
    }

    // ���� ������Ʈ
    public void StartTimer()
    {
        timerActive = true;
    }
    public void StopTimer()
    {
        timerActive = false;
    }

}
