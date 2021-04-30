using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainEvent2 : MonoBehaviour
{
    public GameObject[,] stone = new GameObject[8, 8];
    public int turn = 0;
    public int[,] board = new int[8, 8];
    public int[,] store = new int[8, 8];
    int[] updown = { -1, -1, 0, 1, 1, 1, 0, -1 }, lright = { 0, 1, 1, 1, 0, -1, -1, -1 };
    [SerializeField]
    private GameObject[] prefabArray;
    private GameObject target;

    int[,] valueary = new int[8, 8]{
{100,1,4,3,3,4,1,100 },
{ 1,-3,-1,-1,-1,-1,-3,1 },
{ 4,-1,0,0,0,0,-1,4 },
{ 3,-1,0,0,0,0,-1,3 },
{ 3,-1,0,0,0,0,-1,3 },
{ 4,-1,0,0,0,0,-1,4 },
{ 1,-3,-1,-1,-1,-1,-3,1 },
{ 100,1,4,3,3,4,1,100 }
};

    struct Coordinate
    {
        public int x;
        public int y;
        public int value;
    }

    bool inside(int x, int y)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
            return true;
        else
            return false;
    }

    public bool find_input(int[,] board, int x, int y, int color)
    {
        if (!inside(x, y))
            return false;
        if (board[x, y] != 0)
            return false;

        for (int i = 0; i < 8; i++)
        {               //8방향으로 확인
            for (int j = 1; j < 8; j++)
            {           // 일자로 다른색이 계속 나오는지 확인 
                int nx = j * updown[i] + x, ny = j * lright[i] + y;
                if (!inside(nx, ny))
                    break;
                if (j == 1)
                {
                    if (board[nx, ny] != 3 - color)
                        break;
                }
                if (board[nx, ny] == color)
                    return true;
                else if (board[nx, ny] == 0)
                {
                    break;
                }
            }
        }
        return false;
    }

    public int check_value(int[,] board, int x, int y, int color)
    {
        if (turn >= 60)
        {
            return 1;
        }

        return valueary[x, y];
    }

    public int action(int[,] board, int x, int y, int color, int check)
    {
        int ans = 0;

        Coordinate input = new Coordinate();
        Queue<Coordinate> que = new Queue<Coordinate>();
        for (int i = 0; i < 8; i++)
        {               //8방향으로 확인
            for (int j = 1; j < 10; j++)
            {           // 일자로 다른색이 계속 나오는지 확인 
                int nx = j * updown[i] + x, ny = j * lright[i] + y;

                if (!inside(nx, ny))
                {
                    while (que.Count > 0)
                    {
                        que.Dequeue();
                    }
                    break;
                }
                if (j == 1)
                {
                    if (board[nx, ny] != 3 - color)
                    {
                        while (que.Count > 0)
                        {
                            que.Dequeue();
                        }
                        break;
                    }
                }
                if (board[nx, ny] == color)
                {
                    if (check == 1)
                        ans += check_value(board, x, y, color);
                    while (que.Count > 0)
                    {
                        if (check == 0)
                        {
                            board[que.Peek().x, que.Peek().y] = color;
                        }
                        if (check == 1)
                            ans += check_value(board, que.Peek().x, que.Peek().y, color); //가장자리일수록 높은값
                        que.Dequeue();
                    }
                    break;
                }
                else if (board[nx, ny] == 0)
                {
                    while (que.Count > 0)
                    {
                        que.Dequeue();
                    }
                    break;
                }
                input.x = nx;
                input.y = ny;
                que.Enqueue(input);
            }
        }
        board[x, y] = color;
        return ans;
    }

    /*Coordinate dfs(int x, int y, int depth, int alpha, int beta)
    {
        Coordinate storepair;
        Coordinate returnvalue;
        int[,] store1=new int[8,8];
        int count = 0, count1 = 0, count2 = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                store1[i,j] = store[i,j];
                if (store[i,j] == 0)
                    count++;
                else if (store[i,j] == 1)
                    count1++;
                else if (store[i,j] == 2)
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
                    if (store[i,j] == 2)
                    {
                        returnval += check_value(store, i, j, 2);       //가장자리일수록 높은점수
                    }
                    else if (store[i,j] == 1)
                    {
                        returnval -= check_value(store, i, j, 1);
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
                    if (find_input(store1, i, j, 2))
                    {

                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l,k] = store1[l,k];
                            }
                        }

                        action(store, i, j, 2, 0);
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
                    if (find_input(store1, i, j, 1))
                    {           //색깔 나중에 수정(플레이어꺼)
                        find_flag = true;
                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l,k] = store1[l,k];
                            }
                        }
                        action(store, i, j, 1, 0);
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
                    if (find_input(store1, i, j, 2))
                    {
                        find_flag = true;
                        for (int l = 0; l < 8; l++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                store[l,k] = store1[l,k];
                            }
                        }

                        action(store, i, j, 2, 0);
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
    }*/

    private void Awake()
    {
        board[3, 3] = 1; board[4, 4] = 1; board[3, 4] = 2; board[4, 3] = 2;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 position = new Vector3(-3.0f + j * 0.87f, -3.0f + i * 0.87f, 0);
                if (i >= 3 && i <= 4 && j >= 3 && j <= 4)
                {
                    stone[i, j] = Instantiate(prefabArray[(j + i) % 2 + 1], position, Quaternion.identity);
                }
                else if (find_input(board, i, j, turn % 2 + 1))
                {
                    stone[i, j] = Instantiate(prefabArray[3], position, Quaternion.identity);
                }
                else
                {
                    stone[i, j] = Instantiate(prefabArray[0], position, Quaternion.identity);
                }
                stone[i, j].name = (i * 8 + j).ToString();
            }
        }
    }
    int GameObjectToindex(GameObject targetObj)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (stone[i, j] == targetObj)
                    return i * 8 + j;
            }
        }
        return -1;
    }


    void Update()
    {
        int x, y, check_turn = 0;
        if (Input.GetMouseButtonDown(0))
        {
            target = null;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(pos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            { //히트되었다면 여기서 실행

                target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정
                x = GameObjectToindex(target);
                y = x % 8;
                x /= 8;
                if (find_input(board, x, y, turn % 2 + 1))
                {
                    action(board, x, y, turn % 2 + 1, 0);
                    turn++;
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Destroy(stone[i, j]); //기존 인스턴스 삭제
                            if (find_input(board, i, j, turn % 2 + 1))
                            {
                                check_turn++;
                                stone[i, j] = Instantiate(prefabArray[3], stone[i, j].transform.position, Quaternion.identity); //놓을수 있는 ?? 표현하기
                            }
                            else
                            {
                                stone[i, j] = Instantiate(prefabArray[board[i, j]], stone[i, j].transform.position, Quaternion.identity); //새로 인스턴스 넣기
                            }

                            stone[i, j].name = (i * 8 + j).ToString();
                        }
                    }

                    if (check_turn == 0)
                    {
                        turn++;
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {

                                if (find_input(board, i, j, turn % 2 + 1))
                                {
                                    check_turn++;
                                    Destroy(stone[i, j]); //기존 인스턴스 삭제
                                    stone[i, j] = Instantiate(prefabArray[3], stone[i, j].transform.position, Quaternion.identity); //놓을수 있는 ?? 표현하기
                                }
                                stone[i, j].name = (i * 8 + j).ToString();
                            }
                        }
                    }
                }

                Debug.Log(x * 8 + y);
                Debug.Log(board[x, y]);
            }
        }
    }
}
