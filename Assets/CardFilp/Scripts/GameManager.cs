using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int[,] board;  //보드판
    bool[,] check; //1. setup때 카드 넣기, 2. 이 카드가 이미 열려있는가?
    int[] list;  //카드 종류 
    GameObject[] Cards;

    void Start()
    {
        StartCoroutine(boardSetup());
        Cards = GameObject.FindGameObjectsWithTag("Card");
       // for(int i = 0; i < Cards.Length; i++)
       //{
            //Debug.Log(i + "th, " + Cards[i].name);
       //}
    }

    IEnumerator boardSetup()
    {
        int i, j, k;

        board = new int[4, 4];
        check = new bool[4, 4];
        list = new int[8];

        for (i = 0; i < 8; i++) list[i] = i; //단순히 int 넣음

        //보드판 제작. check를 1번 용도로 사용.
        for (i = 0; i < 8; i++)
        {
            while (true)
            {
                j = Random.Range(0, 4);
                k = Random.Range(0, 4);
                if (check[j, k] == false)
                {
                    board[j, k] = list[i];
                    check[j, k] = true;
                    break;
                }
            }

            while (true)
            {
                j = Random.Range(0, 4);
                k = Random.Range(0, 4);
                if (check[j, k] == false)
                {
                    board[j, k] = list[i];
                    check[j, k] = true;
                    break;
                }
            }
        }

        //check를 2번 용도로 쓰기 위해 초기화
        for (i = 0; i < 4; i++)
            for (j = 0; j < 4; j++)
                check[i, j] = false;
        for(i = 0; i < 4; i++)
        {
            Debug.Log(i + "번째 행 = " + board[i, 0] + ", " + board[i, 1] + ", " + board[i, 2] + ", " + board[i, 3]);
        }
        yield return null;
    }

    void Update()
    {
        
    }
}
