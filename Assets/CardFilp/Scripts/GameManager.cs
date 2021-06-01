using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private SetStage stage;
    public int[,] board;  //보드판
    public bool[,] check; //1. setup때 카드 넣기, 2. 이 카드가 이미 열려있는가?
    public int[] list;  //카드 종류 
    private int cardCount = 0; //현재 Card를 몇 개 선택했는가?
    private int[] tempCard;

    void Start()
    {
        tempCard = new int[2];
        // for(int i = 0; i < Cards.Length; i++)
        //{
        //Debug.Log(i + "th, " + Cards[i].name);
        //}
    }

    public void boardSetup(int width, int height)
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
    }

    //카드 매칭
    public IEnumerator CardMatch(int positionWidth, int positionHeight)
    {
        if(check[positionWidth, positionHeight] == true) yield return null; //이미 맞춘거 누르면 skip
        //처음 누른 카드인 경우,
        else if(cardCount == 0)
        {
            tempCard[0] = positionWidth;
            tempCard[1] = positionHeight;
            cardCount++;
        }
        //두번째 누른 카드인 경우,
        else
        {
            //두 카드가 일치할 경우,
            if(board[positionWidth,positionHeight] == board[tempCard[0], tempCard[1]])
            {
                check[positionWidth, positionHeight] = true;
                check[tempCard[0], tempCard[1]] = true;
                GameObject.Find("Card" + positionWidth + positionHeight).GetComponent<Card>().beforeMatched();
                Debug.Log("Matched");
            }
            //두 카드가 일치하지 않을 경우,
            else
            {
                yield return new WaitForSeconds(0.5f);
                GameObject.Find("Card" + positionWidth + positionHeight).GetComponent<Card>().Flip();
                GameObject.Find("Card" + tempCard[0] + tempCard[1]).GetComponent<Card>().Flip();
                
            }
            cardCount = 0;
        }
    }

    void Update()
    {
        
    }
}
