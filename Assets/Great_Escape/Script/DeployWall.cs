using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        Verticalpoints = GameObject.FindGameObjectsWithTag("VerticalPoint");
        Horizontalpoints = GameObject.FindGameObjectsWithTag("HorizontalPoint");
        vGenNum = new bool[9];
        hGenNum = new bool[8];
        for (int i = 0; i < Verticalpoints.Length; i++)
            vGenNum[i] = true;
        for (int i = 0; i < Horizontalpoints.Length; i++)
            hGenNum[i] = true;
        randomGenerate();

        spawnWall();
    }
    private void spawnWall()
    {
        
        for (int i = 0; i < Verticalpoints.Length; i++)
        {
            if (vGenNum[i] == true)
            {
                GameObject temp = Instantiate(Wall_Vertical) as GameObject;
                temp.transform.position = new Vector2(Verticalpoints[i].transform.position.x, Verticalpoints[i].transform.position.y);
            }
        }
        for (int i = 0; i < Horizontalpoints.Length; i++)
        {
            if (hGenNum[i] == true)
            {
                GameObject temp = Instantiate(Wall_Horizontal) as GameObject;
                temp.transform.position = new Vector2(Horizontalpoints[i].transform.position.x, Horizontalpoints[i].transform.position.y);
            }
        }
    }

    private void randomGenerate()
    {
        int count = 0, number;

        while(count <= Verticalpoints.Length / 2 + 1)
        {
            number = Random.Range(0, Verticalpoints.Length);
            vGenNum[number] = false;
            count++;
        }

        count = 0;
        while (count <= Horizontalpoints.Length / 2 + 1)
        {
            number = Random.Range(0, Horizontalpoints.Length);
            hGenNum[number] = false;
            count++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
