using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public static GameStart instance;
    public int state = 0;

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

    public void btnClick()
    {
        if (state == 0)
        {
            state = 1;
            GameManage.instance.game_count_start();
        }
    }
}
