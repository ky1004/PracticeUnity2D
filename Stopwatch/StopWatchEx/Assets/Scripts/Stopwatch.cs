using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] float timeStart;
    [SerializeField] Text timeText, startPauseText;

    bool timeActive = false;

    void Start()
    {
        timeText.text = timeStart.ToString("F2");
    }

    void Update()
    {
        StartTime();
    }

    void StartTime()
    {
        if (timeActive)
        {
            timeStart += Time.deltaTime;
            timeText.text = timeStart.ToString("F2");
        }
    }

    public void StartPauseBtn()
    {
        // ��ư Ŭ�� �� ���� ���� ���
        timeActive = !timeActive;
        startPauseText.text = timeActive ? "�Ͻ�����" : "����";
    }

    public void ResetBtn()
    {
        timeStart = 0f;
        timeText.text = timeStart.ToString("F2");
    }
}
