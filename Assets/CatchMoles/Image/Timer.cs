using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public Text timeText;
    private float time;
    float militime;
    static int countState = 0;

    private void Awake()
    {
        time = 30f;
        instance = this;
    }

    private void Update()
    {
        if (countState == 1)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                militime = Mathf.Floor((time - Mathf.Floor(time)) * 100);
                timeText.text = Mathf.Floor(time).ToString() + ":";
                if (militime < 10)
                    timeText.text += "0" + militime.ToString();
                else
                    timeText.text += militime.ToString();
            }
            else
            {
                time = 0.0f;
                timeText.text = "TIME OVER";
                countState = 0;
                GameManage.instance.game_over();
            }
        }
    }

    public void countStart()
    {
        time = 30f;
        countState = 1;
    }
}
