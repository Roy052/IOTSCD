using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameCount : MonoBehaviour
{
    public Text countText;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        time = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void startCount()
    {
        time = 3;
        countText.text = "3";
        Invoke("countdown", 1.0f);
    }

    void countdown()
    {
        time -= 1;
        if (time > 0)
        {
            countText.text = time.ToString();
            Invoke("countdown", 1.0f);
        }
        if (time == 0)
        {
            countText.text = "GAME START!!";
            Invoke("countdown", 1.0f);
        }
        else if (time == -1)
        {
            countText.text = "";
            GameManage.instance.game_start();
        }

    }
}
