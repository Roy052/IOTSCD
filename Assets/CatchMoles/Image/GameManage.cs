using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameCount countInstance;
    public static GameManage instance;
    public static hole[] holeInstArray = new hole[9];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void game_count_start()
    {
        countInstance.startCount();
    }

    public void game_start()
    {
        Timer.instance.countStart();
        hole.playable = true;
        Score.instance.setScore(0);
        for (int i = 0; i < 9; i++)
        {
            holeInstArray[i].Invoke("BecomeMole", 0);
        }
    }

    public static void addHoleInstance(hole inst, int row, int col)
    {
        holeInstArray[row * 3 + col] = inst;
    }

    public void game_over()
    {
        for (int i = 0; i < 9; i++)
            holeInstArray[i].allStop();
        GameStart.instance.state = 0;
    }
}
