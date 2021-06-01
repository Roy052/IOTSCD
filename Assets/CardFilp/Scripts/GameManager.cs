using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SetStage stage;
    int width, height;
    int[,] board;  //보드판
    bool[,] check; //1. setup때 카드 넣기, 2. 이 카드가 이미 열려있는가?
    int[] list;  //카드 종류 
    GameObject[] Cards;

    void Start()
    {
        width = GameObject.Find("Stg").GetComponent<SetStage>().Width;

        height = GameObject.Find("Stg").GetComponent<SetStage>().Height;
        StartCoroutine(boardSetup());
        // Cards = GameObject.FindGameObjectsWithTag("Card");
        // for(int i = 0; i < Cards.Length; i++)
        //{
        //Debug.Log(i + "th, " + Cards[i].name);
        //}
    }

    IEnumerator boardSetup()
    {
        int i, j, k;

        board = new int[width, height];
        check = new bool[width, height];
        list = new int[width*height/2];

        for (i = 0; i < width*height/2; i++) list[i] = i; //단순히 int 넣음

        //보드판 제작. check를 1번 용도로 사용.
        for (i = 0; i < width*height/2; i++)
        {
            while (true)
            {
                j = Random.Range(0, width);
                k = Random.Range(0, height);
                if (check[j, k] == false)
                {
                    board[j, k] = list[i];
                    check[j, k] = true;
                    break;
                }
            }

            while (true)
            {
                j = Random.Range(0, width);
                k = Random.Range(0, height);
                if (check[j, k] == false)
                {
                    board[j, k] = list[i];
                    check[j, k] = true;
                    break;
                }
            }
        }

        //check를 2번 용도로 쓰기 위해 초기화
        for (i = 0; i < width; i++)
            for (j = 0; j < height; j++)
                check[i, j] = false;
        yield return null;
    }

    void Update()
    {
        
    }
}
