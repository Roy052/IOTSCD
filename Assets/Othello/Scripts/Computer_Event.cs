using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Computer_Event : MonoBehaviour
{
    //난이도 조정
    public int difficult = 1;
    int player_color;
    int[,] store =new int[8,8] ;
    struct Coordinate
    {
        public int x;
        public int y;
        public int value;
    }

    //alpha-beta 알고리즘 사용하여 계산
    Coordinate dfs(int x, int y, int depth, int alpha, int beta)
    {
        Coordinate storepair;
        Coordinate returnvalue;

        //depth로 넘어갈때마다 변하는 board 배열을 대신하여 기존 board 배열을 저장하는 변수
        int[,] store1 = new int[8, 8];

        // count = nothing 갯수 , count1 = player돌 갯수 , count2 = computer 돌 갯수
        int count = 0, count1 = 0, count2 = 0;
        //기존 배열 저장
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                store1[i, j] = store[i, j];
                if (store[i, j] == 0)
                    count++;
                else if (store[i, j] == player_color)
                    count1++;
                else if (store[i, j] == 3-player_color)
                    count2++;
            }
        }

        //dfs return 조건
        //탐색 할 수 있을만큼 깊이 탐색하거나 (난이도만큼)
        //또는 흑과 백 둘 중 하나가 죽거나(돌이 하나도 없거나)
        //또는 게임이 끝났거나 (nothing의 갯수가 0) 
        if ((depth == difficult * 2) || count == 0 || count1 == 0 || count2 == 0)
        {
            int returnval = 0;
            //player가 사망했을때 value값= +inf
            if (count1 == 0)
            {
                returnvalue.value = 999999;
                returnvalue.x = x;
                returnvalue.y = y;
                return returnvalue;
            }
            //computer가 사망했을때 value값 -inf
            else if (count2 == 0)
            {
                returnvalue.value = -999999;
                returnvalue.x = x;
                returnvalue.y = y;
                return returnvalue;
            }
            //그 이외 가중치 계산
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //자신의 돌의 가중치 더함
                    if (store[i, j] == 3-player_color)
                    {
                        returnval += GameObject.Find("MainEvent").GetComponent<MainEvent>().check_value(store, i, j);
                    }
                    //상대의 돌의 가중치 뺌 (상대가 좋은 자리먹을수록 불리해짐)
                    else if (store[i, j] == player_color)
                    {
                        returnval -= GameObject.Find("MainEvent").GetComponent<MainEvent>().check_value(store, i, j);
                    }
                }
            }
            returnvalue.value = returnval;
            returnvalue.x = x;
            returnvalue.y = y;
            return returnvalue;
        }

        //find_flag는 탐색 도중 플레이어나 컴퓨터가 둘 바둑돌이 없으면 확인시켜주는 변수로
        //만약 find_flag가 false이면 탐색도중 둘 곳이 존재하지 않으므로 다음 턴에게 넘겨주는 역할
        //첫 탐색에선 무조건 둘 수가 있으므로 첫탐색과 두번째탐색부터의 코드를 나눠서 적음
        bool find_flag = false;
        //첫 탐색일때 board 의 모든 (x,y)를 찾아 find_input실시 
        //첫 탐색이므로 computer의 턴이다
        if (depth == 0)
        {
            bool aflag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameObject.Find("MainEvent").GetComponent<MainEvent>().find_input(store1, i, j, 3-player_color))
                    {
                        //변하기 전 기존 board배열 store에 저장
                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l, k] = store1[l, k];
                            }
                        }
                        // 돌을 둬서 배열 변환시킨후 다음 depth로 dfs시킴
                        GameObject.Find("MainEvent").GetComponent<MainEvent>().action(store, i, j, 3-player_color);
                        storepair = dfs(i, j, depth + 1, alpha, beta);

                        //alpha값을 갱신 (computer에게 이익이 되는 최적의 값)
                        if (storepair.value > alpha)
                        {
                            alpha = storepair.value;
                            x = storepair.x;
                            y = storepair.y;
                        }
                    }
                    //(player에게 이익이 되는 최적의 값 <= computer에게 이익이 되는 최적의값)
                    //위 식을 만족시키면 다음 dfs node들은 굳이 확인할 필요없는 값들이다.
                    //왜냐하면 computer가 아무리 최악의 수를 둬도 player보다 좋은 수를 두는 수는 굳이 계산할 필요가 없기 때문
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
        //플레이어 턴
        //플레이어는 항상 최선의 수를 둔다고 가정
        else if (depth % 2 == 1)
        {
            bool bflag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameObject.Find("MainEvent").GetComponent<MainEvent>().find_input(store1, i, j, player_color))
                    {          
                        find_flag = true;
                        //변하기 전 기존 board배열 store에 저장
                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l, k] = store1[l, k];
                            }
                        }
                        GameObject.Find("MainEvent").GetComponent<MainEvent>().action(store, i, j, player_color);
                        storepair = dfs(x, y, depth + 1, alpha, beta);
                        //beta값을 갱신 (player에게 이익이 되는 최적의 값)
                        if (storepair.value < beta)
                        {
                            beta = storepair.value;
                        }
                    }
                    //(computer에게 이익이 되는 최적의 값 <= player에게 이익이 되는 최적의값)
                    //위 식을 만족시키면 마찬가지로 dfs node들은 굳이 확인할 필요없는 값들이다.
                    //왜냐하면 computer가 아무리 최선의 수를 둬도 player의 최악의 수보다 최악을 두는 수는 굳이 계산할 필요가 없기 때문
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
            //둘 수 있는곳을 찾지 못하였을 경우
            //다음 턴에게 턴을 넘겨서 => {dfs(x, y, depth + 1, alpha, beta)}
            //beta값을 갱신시킨다
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
        // 컴퓨터의 턴
        // 첫탐색과 똑같다
        else
        {
            bool aflag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameObject.Find("MainEvent").GetComponent<MainEvent>().find_input(store1, i, j, 3-player_color))
                    {
                        find_flag = true;
                        //변하기 전 기존 board배열 store에 저장
                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l, k] = store1[l, k];
                            }
                        }

                        GameObject.Find("MainEvent").GetComponent<MainEvent>().action(store, i, j, 3-player_color);
                        storepair = dfs(x, y, depth + 1, alpha, beta);
                        if (storepair.value > alpha)
                        {
                            alpha = storepair.value;
                        }
                    }
                    //(player에게 이익이 되는 최적의 값 <= computer에게 이익이 되는 최적의값)
                    //위 식을 만족시키면 다음 dfs node들은 굳이 확인할 필요없는 값들이다.
                    //왜냐하면 computer가 아무리 최악의 수를 둬도 player보다 좋은 수를 두는 수는 굳이 계산할 필요가 없기 때문
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
            //둘 수 있는곳을 찾지 못하였을 경우
            //다음 턴에게 턴을 넘겨서 => {dfs(x, y, depth + 1, alpha, beta)}
            //alpha값을 갱신시킨다
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
        player_color = GameObject.Find("MainEvent").GetComponent<MainEvent>().player_color;

        //기존 board 배열값 받아와서 전역변수 store 에 저장
        for (int l = 0; l < 8; l++)
        {
            for (int k = 0; k < 8; k++)
            {
                store[l, k] = board[l, k];
            }
        }
        //어차피 board의 모든 (x,y) 탐색하여 둘 수 있는 곳을 찾기 때문에 초기값을 (-1,-1)로 설정
        //alpha = -inf로 초기화 beta = +inf 로 초기화
        //상대방이 최선의 수를 둔다고 가정하고 자신의 초기 승률은 -inf로 가정
        result = dfs(-1, -1, 0, -999999, 999999);

        GameObject.Find("MainEvent").GetComponent<MainEvent>().action(board, result.x, result.y, 2);


    }
}
