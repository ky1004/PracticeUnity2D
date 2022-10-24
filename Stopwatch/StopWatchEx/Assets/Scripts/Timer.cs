using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    bool timerActive = false; // 타이머 활성화 상태

    float currentTime; // 현재 시간
    public int startMinutes; // 초기값 설정 변수
    public Text currentTimeText;

    void Start()
    {
        // sec
        currentTime = startMinutes * 60; 
    }

    void Update()
    {
        // 활성화 상태일 때
        if (timerActive == true)
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime <= 0)
            {
                timerActive = false;
                Start();
                Debug.Log("타이머 완료");
            }
        }
        // 초를 분 시간으로 쉽게 변환해줌
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

    // 매초 업데이트
    public void StartTimer()
    {
        timerActive = true;
    }
    public void StopTimer()
    {
        timerActive = false;
    }

}
