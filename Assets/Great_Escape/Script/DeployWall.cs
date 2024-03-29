﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployWall : MonoBehaviour
{
    public GameObject Wall_Vertical;
    public GameObject Wall_Horizontal;
    public GameObject[] Verticalpoints;
    public GameObject[] Horizontalpoints;
    private bool[] vGenNum;
    private bool[] hGenNum;
    private GameSystem gameSystem;
    public bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        Verticalpoints = GameObject.FindGameObjectsWithTag("VerticalPoint");
        Horizontalpoints = GameObject.FindGameObjectsWithTag("HorizontalPoint");
        gameSystem = GameObject.FindGameObjectWithTag("GameSystem").GetComponent<GameSystem>();
        vGenNum = new bool[12];
        hGenNum = new bool[10];
        for (int i = 0; i < Verticalpoints.Length; i++)
            vGenNum[i] = true;
        for (int i = 0; i < Horizontalpoints.Length; i++)
            hGenNum[i] = true;
        
    }
    private void spawnWall()
    {
        
        for (int i = 0; i < Verticalpoints.Length; i++)
        {
            if (vGenNum[i] == true)
            {
                GameObject temp = Instantiate(Wall_Vertical) as GameObject;
                temp.name = "V_Wall" + i;
                temp.transform.position = new Vector2(Verticalpoints[i].transform.position.x, Verticalpoints[i].transform.position.y);
            }
        }
        for (int i = 0; i < Horizontalpoints.Length; i++)
        {
            if (hGenNum[i] == true)
            {
                GameObject temp = Instantiate(Wall_Horizontal) as GameObject;
                temp.name = "H_Wall" + i;
                temp.transform.position = new Vector2(Horizontalpoints[i].transform.position.x, Horizontalpoints[i].transform.position.y);
            }
        }
    }

    private void randomGenerate()
    {
        int count = 0, number;

        string a = "" + GameSystem.mapCount + "=";
        for (int i = 0; i < 12; i++)
        {
            a += gameSystem.maps[GameSystem.mapCount, 0, i];
            a += ", ";
        }
        
        Debug.Log(a);
       
        for(int i = 0; i < Verticalpoints.Length; i++)
            if(gameSystem.maps[GameSystem.mapCount, 0, i] == 0)
            {
                vGenNum[i] = false;
                count++;
            }

        while (count <= Verticalpoints.Length / 2 - 2)
        {
            number = Random.Range(0, Verticalpoints.Length);
            //1. 중복되지 않으며 2. 맵에 반드시 있어야 하거나
            if (vGenNum[number] != false && gameSystem.maps[GameSystem.mapCount, 0, number] != 1)
            {
                vGenNum[number] = false;
                count++;
            }
        }

        count = 0;
        for (int i = 0; i < Horizontalpoints.Length; i++)
            if (gameSystem.maps[GameSystem.mapCount, 1, i] == 0)
            {
                hGenNum[i] = false;
                count++;
            }
        while (count <= Horizontalpoints.Length / 2 - 1)
        {
            number = Random.Range(0, Horizontalpoints.Length);
            if(hGenNum[number] != false && gameSystem.maps[GameSystem.mapCount, 1, number] != 1)
            {
                hGenNum[number] = false;
                count++;
            }
        }

        GameSystem.mapCount++;
    }
    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            randomGenerate();
            spawnWall();
            started = false;
        }
    }
}
