using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Computer_Event : MonoBehaviour
{
    public int difficult = 1;
    int[,] store =new int[8,8] ;
    struct Coordinate
    {
        public int x;
        public int y;
        public int value;
    }
    Coordinate dfs(int x, int y, int depth, int alpha, int beta)
    {
        Coordinate storepair;
        Coordinate returnvalue;
        int[,] store1 = new int[8, 8];
        int count = 0, count1 = 0, count2 = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                store1[i, j] = store[i, j];
                if (store[i, j] == 0)
                    count++;
                else if (store[i, j] == 1)
                    count1++;
                else if (store[i, j] == 2)
                    count2++;
            }
        }

        if ((depth == difficult * 2) || count == 0 || count1 == 0 || count2 == 0)
        {
            int returnval = 0;
            if (count1 == 0)
            {
                returnvalue.value = 999999;
                returnvalue.x = x;
                returnvalue.y = y;
                return returnvalue;
            }
            else if (count2 == 0)
            {
                returnvalue.value = -999999;
                returnvalue.x = x;
                returnvalue.y = y;
                return returnvalue;
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (store[i, j] == 2)
                    {
                        returnval += GameObject.Find("MainEvent").GetComponent<MainEvent>().check_value(store, i, j, 2);       //가장자리일수록 높은점수
                    }
                    else if (store[i, j] == 1)
                    {
                        returnval -= GameObject.Find("MainEvent").GetComponent<MainEvent>().check_value(store, i, j, 2);
                    }
                }
            }
            returnvalue.value = returnval;
            returnvalue.x = x;
            returnvalue.y = y;
            return returnvalue;
        }

        bool find_flag = false;
        if (depth == 0)
        {
            bool aflag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameObject.Find("MainEvent").GetComponent<MainEvent>().find_input(store1, i, j, 2))
                    {

                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l, k] = store1[l, k];
                            }
                        }

                        GameObject.Find("MainEvent").GetComponent<MainEvent>().action(store, i, j, 2, 0);
                        storepair = dfs(i, j, depth + 1, alpha, beta);
                        if (storepair.value > alpha)
                        {
                            alpha = storepair.value;
                            x = storepair.x;
                            y = storepair.y;
                        }
                    }
                    if (beta <= alpha)
                    {
                        aflag = true;
                        break;
                    }
                }
                if (aflag)
                {
                    break;
                }
            }
            returnvalue.value = alpha;
            returnvalue.x = x;
            returnvalue.y = y;
            return returnvalue;
        }
        
        else if (depth % 2 == 1)
        {//플레이어 턴
            bool bflag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameObject.Find("MainEvent").GetComponent<MainEvent>().find_input(store1, i, j, 1))
                    {           //색깔 나중에 수정(플레이어꺼)
                        find_flag = true;
                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l, k] = store1[l, k];
                            }
                        }
                        GameObject.Find("MainEvent").GetComponent<MainEvent>().action(store, i, j, 1, 0);
                        storepair = dfs(x, y, depth + 1, alpha, beta);
                        if (storepair.value < beta)
                        {
                            beta = storepair.value;
                        }
                    }
                    if (beta <= alpha)
                    {
                        bflag = true;
                        break;
                    }
                }
                if (bflag)
                {
                    break;
                }
            }
            if (!find_flag)
            {
                storepair = dfs(x, y, depth + 1, alpha, beta);
                if (storepair.value < beta)
                {
                    beta = storepair.value;
                }
            }

            returnvalue.value = beta;
            returnvalue.x = x;
            returnvalue.y = y;
            return returnvalue;
        }
        else
        {// 컴퓨터 턴
            bool aflag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameObject.Find("MainEvent").GetComponent<MainEvent>().find_input(store1, i, j, 2))
                    {
                        find_flag = true;
                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l, k] = store1[l, k];
                            }
                        }

                        GameObject.Find("MainEvent").GetComponent<MainEvent>().action(store, i, j, 2, 0);
                        storepair = dfs(x, y, depth + 1, alpha, beta);
                        if (storepair.value > alpha)
                        {
                            alpha = storepair.value;
                        }
                    }
                    if (beta <= alpha)
                    {
                        aflag = true;
                        break;
                    }
                }
                if (aflag)
                {
                    break;
                }
            }
            if (!find_flag)
            {
                storepair = dfs(x, y, depth + 1, alpha, beta);
                if (storepair.value > alpha)
                {
                    alpha = storepair.value;
                }
            }
            returnvalue.value = alpha;
            returnvalue.x = x;
            returnvalue.y = y;
            return returnvalue;
        }
    }
    private void Awake()
    {
        
    }
    public void compute(int[,] board)
    {
        Coordinate result;

        for (int l = 0; l < 8; l++)
        {
            for (int k = 0; k < 8; k++)
            {
                store[l, k] = board[l, k];
            }
        }
        result = dfs(-1, -1, 0, -99999, 99999);

        GameObject.Find("MainEvent").GetComponent<MainEvent>().action(board, result.x, result.y, 2, 0);


    }
}
