﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainEvent2 : MonoBehaviour
{
    public GameObject[,] stone = new GameObject[8, 8];
    public int turn = 0;
    //board 안에 nothing,white,black 저장
    public int[,] board = new int[8, 8];
    public int[,] store = new int[8, 8];
    //8방향으로 확인하기위한 배열
    int[] updown = { -1, -1, 0, 1, 1, 1, 0, -1 }, lright = { 0, 1, 1, 1, 0, -1, -1, -1 };
    [SerializeField]
    private GameObject[] prefabArray;
    private GameObject target;

    struct Coordinate
    {
        public int x;
        public int y;
        public int value;
    }

    //(x,y)가 8x8 board안에 존재하는지 여부
    bool inside(int x, int y)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
            return true;
        else
            return false;
    }

    //플레이턴이 color 일 때 (x,y)가 board판 위에서 둘수 있는 여부 확인 함수 {게임내에서 ?위치 찾는 함수}
    //color 값은 1 또는 2
    public bool find_input(int[,] board, int x, int y, int color)
    {
        if (!inside(x, y))
            return false;
        if (board[x, y] != 0)
            return false;

        //8방향으로 확인
        for (int i = 0; i < 8; i++)
        {
            // 일자로 다른색이 계속 나오는지 확인 
            for (int j = 1; j < 9; j++)
            {
                //한칸 씩 늘려가며 확인
                int nx = j * updown[i] + x, ny = j * lright[i] + y;
                //칸을 벗어나면 종료
                if (!inside(nx, ny))
                    break;
                if (j == 1)
                {
                    //첫 확인 때 같은 색깔을 만났을때 종료(둘 수 없는 곳 return false)
                    if (board[nx, ny] != 3 - color)
                        break;
                }
                //첫 확인 때 다른 색이고 중간에 자기와 같은 색을 만나면 종료(둘 수 있는 곳 return true)
                if (board[nx, ny] == color)
                    return true;
                //첫 확인 때 다른 색이지만 중간에 비어있는 경우 종료(둘 수 없는곳 return false)
                else if (board[nx, ny] == 0)
                {
                    break;
                }
            }
        }
        return false;
    }

    //find_input 함수랑 거의 똑같음 바꿀 수 있는 돌을 queue에 넣어 한번에 바꾸는 식만 추가 됨 
    public int action(int[,] board, int x, int y, int color, int check)
    {
        int ans = 0;

        Coordinate input = new Coordinate();
        Queue<Coordinate> que = new Queue<Coordinate>();
        //8방향으로 확인
        for (int i = 0; i < 8; i++)
        {
            // 일자로 다른색이 계속 나오는지 확인 
            for (int j = 1; j < 9; j++)
            {           
                int nx = j * updown[i] + x, ny = j * lright[i] + y;
                //칸을 벗어나면 종료
                if (!inside(nx, ny))
                {
                    //바꿀 수 있는 돌 없음(큐에 있는 값들 삭제)
                    while (que.Count > 0)
                    {
                       
                        que.Dequeue();
                    }
                    break;
                }
                if (j == 1)
                {
                    //첫 확인 때 같은 색깔을 만났을때 종료
                    if (board[nx, ny] != 3 - color)
                    {
                        //바꿀 수 있는 돌 없음(큐에 있는 값들 삭제)
                        while (que.Count > 0)
                        {

                            que.Dequeue();
                        }
                        break;
                    }
                }
                if (board[nx, ny] == color)
                {
                    while (que.Count > 0)
                    {
                        //queue에 있는 값들을 board에서 플레이 턴 color로 바꿈
                        board[que.Peek().x, que.Peek().y] = color;
                        que.Dequeue();
                    }
                    break;
                }
                //첫 확인 때 다른 색이지만 중간에 비어있는 경우 종료
                else if (board[nx, ny] == 0)
                {
                    //바꿀 수 있는 돌 없음(큐에 있는 값들 삭제)
                    while (que.Count > 0)
                    {
                        que.Dequeue();
                    }
                    break;
                }
                //for문 돌리면서 확인 된 값들 queue넣기
                input.x = nx;
                input.y = ny;
                que.Enqueue(input);
            }
        }
        board[x, y] = color;
        return ans;
    }

    private void Awake()
    {
        //기본 board 세팅
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

    //GameObject 배열 x,y index 값 찾는 함수
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
        //마우스 눌렀을 때
        if (Input.GetMouseButtonDown(0))
        {
            target = null;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(pos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            //히트되었다면 여기서 실행
            if (hit.collider != null)
            {
                //히트 된 게임 오브젝트를 타겟으로 지정
                target = hit.collider.gameObject;
                //타겟 게임 오브젝트 배열 index 구하기
                x = GameObjectToindex(target);
                y = x % 8;
                x /= 8;

                //둘 수 있는 곳이라면
                if (find_input(board, x, y, turn % 2 + 1))
                {
                    //돌 바꾸기
                    action(board, x, y, turn % 2 + 1, 0);
                    turn++;
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            //기존 인스턴스 삭제
                            Destroy(stone[i, j]); 
                            if (find_input(board, i, j, turn % 2 + 1))
                            {
                                //다음 턴이 둘 수 있는지 확인
                                check_turn++;
                                //놓을수 있는 '?' 인스턴스 넣기
                                stone[i, j] = Instantiate(prefabArray[3], stone[i, j].transform.position, Quaternion.identity); 
                            }
                            else
                            {
                                //'nothing' 'black' 'white' 인스턴스 넣기
                                stone[i, j] = Instantiate(prefabArray[board[i, j]], stone[i, j].transform.position, Quaternion.identity);
                            }
                        }
                    }
                    //놓을 수 없는 턴이면 다음 사람에게 넘김
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
                                    //기존 인스턴스 삭제
                                    Destroy(stone[i, j]);
                                    //놓을수 있는 '?' 인스턴스 넣기
                                    stone[i, j] = Instantiate(prefabArray[3], stone[i, j].transform.position, Quaternion.identity);
                                }
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
