using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainEvent : MonoBehaviour
{
    public White_score white_score;
    public Black_score black_score;
    public GameObject win_logo;
    public GameObject lose_logo;
    public GameObject draw_logo;
    public GameObject[,] stone = new GameObject[8,8];
    public GameObject[,] origin_stone = new GameObject[8, 8];
    //3-player_color== computer_color
    public static int now=4;
    public int turn = 4,player_color=1,skip=0;
    //board 안에 nothing,white,black 저장
    public int[,] board = new int[8, 8];
    private int[,] origin = new int[8, 8];
    //8방향으로 확인하기위한 배열
    int[] updown = { -1,-1,0,1,1,1,0,-1 }, lright = { 0,1,1,1,0,-1,-1,-1 };
    bool game_end_status=true;
    float timer=0.0f;
    int waitingTime=2;
    [SerializeField]
    private GameObject[] prefabArray;
    private GameObject target;


    //각 칸마다의 가중치
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
        public int value;//nothing,white,black
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

    //valueary 가중치 값 반환 (60 턴 넘어가면 돌 갯수로 확인)
    public int check_value(int[,] board, int x, int y)
    {
        if (turn-skip >= 56)
        {
            return 1;
        }

        return valueary[x,y];
    }

    //find_input 함수랑 거의 똑같음 바꿀 수 있는 돌을 queue에 넣어 한번에 바꾸는 식만 추가 됨 
    public int action(int[,] board, int x, int y, int color,int check)
    {
        int ans = 0;
        
        Coordinate input =new Coordinate();
        Queue<Coordinate> que= new Queue<Coordinate>();
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
                    while (que.Count>0)
                    {
                        que.Dequeue();
                    }
                    break;
                }
                if (j == 1)
                {
                    //첫 확인 때 같은 색깔을 만났을때 종료
                    if (board[nx,ny] != 3 - color)
                    {
                        //바꿀 수 있는 돌 없음(큐에 있는 값들 삭제)
                        while (que.Count>0)
                        {
                            que.Dequeue();
                        }
                        break;
                    }
                }
                if (board[nx,ny] == color)
                {
                    while (que.Count>0)
                    {
                        //queue에 있는 값들을 board에서 플레이 턴 color로 바꿈 
                        board[que.Peek().x,que.Peek().y] = color;
                        if (check == 1)
                        {
                            stone[que.Peek().x, que.Peek().y].GetComponent<blackanimate>().flip(que.Peek().x, que.Peek().y);
                        }
                        que.Dequeue();
                    }
                    break;
                }

                //첫 확인 때 다른 색이지만 중간에 비어있는 경우 종료
                else if (board[nx,ny] == 0)
                {
                    //바꿀 수 있는 돌 없음(큐에 있는 값들 삭제)
                    while (que.Count>0)
                    {
                        que.Dequeue();
                    }
                    break;
                }
                //for문 돌리면서 확인 된 값들 queue넣기
                input.x = nx;
                input.y = ny;
                que.Enqueue(input) ;
            }
        }
        //직접 둔 위치도 바꾸기
        board[x,y] = color;
        return ans;
    }

    private void Awake()
    {
        //기본 board 세팅
        board[3, 3] = 1; board[4, 4] = 1; board[3, 4] = 2; board[4,3] = 2;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 position = new Vector3(-5.45f + j * 1.12f, -3.9f + i * 1.12f, 0);
                if (i >= 3 && i <= 4 && j >= 3 && j <= 4)
                {
                    stone[i, j] = Instantiate(prefabArray[(j + i) % 2+1], position, Quaternion.identity);
                }
                //나중에 수정 (후공 선택할 시 이 코드 쓰면 안됨)
                else if (find_input(board, i, j, player_color))
                {
                    stone[i, j] = Instantiate(prefabArray[3], position, Quaternion.identity);
                }
                else
                {
                    stone[i,j] = Instantiate(prefabArray[0], position, Quaternion.identity);
                }
            }
        }
    }
    //GameObject 배열 x,y index 값 찾는 함수
    int GameObjectToindex(GameObject targetObj)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (stone[i, j] == targetObj)
                    return i * 8 + j;
            }
        }
        return -1;
    }

    public void create_Object(int x, int y)
    {
        stone[x, y] = Instantiate(prefabArray[board[x,y]], stone[x, y].transform.position, Quaternion.identity);
    }

    void Update()
    {

        int x, y, check_turn = 0,game_end=0,w=0,b=0;
        //플레이어 턴
        for(int i=0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == 1)
                {
                    w++;
                }
                if (board[i, j] == 2)
                {
                    b++;
                }
                white_score.update_white(w);
                black_score.update_black(b);
                if (find_input(board, i, j, turn% 2+1 ))
                {
                    game_end++;
                }
            }
        }
        if (game_end == 0)
        {
            if (game_end_status)
            {
                game_end_status = false;
                if(w>b)
                Instantiate(win_logo, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
                else if (w<b)
                {
                    Instantiate(lose_logo, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(draw_logo, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                }
            }
        }
        else if (turn % 2+1 == player_color)
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    origin[i, j] = board[i, j];
                    origin_stone[i, j] = stone[i, j];
                }
            }


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
                    if (find_input(board, x, y, player_color))
                    {
                        //돌 바꾸기
                        action(board, x, y, player_color,1);
                        //가중치 계산 위해 turn 증가시켜줌
                        turn++;
                        now = turn;
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (find_input(board, i, j, 3 - player_color))
                                {
                                    //다음 턴이 둘 수 있는지 확인
                                    check_turn++;
                                    //놓을수 있는 '?' 인스턴스 넣기
                                    Destroy(stone[i, j]);
                                    stone[i, j] = Instantiate(prefabArray[3], stone[i, j].transform.position, Quaternion.identity);
                                }
                                else if (find_input(origin, i, j,player_color))
                                {
                                    //놓을수 있는 '?' 인스턴스 넣기
                                    Destroy(stone[i, j]);
                                    stone[i, j] = Instantiate(prefabArray[board[i,j]], stone[i, j].transform.position, Quaternion.identity);
                                }
                            }
                        }

                        //놓을 수 없는 턴이면 다음 사람에게 넘김
                        if (check_turn == 0)
                        {
                            turn++;
                            skip++;
                            now = turn;
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (find_input(board, i, j, player_color))
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
                    
                    //Debug.Log(x * 8 + y);
                    //Debug.Log(board[x, y]);
                }
            }

            
        }

        //컴퓨터 턴
        //위 코드랑 똑같음
        else
        {
            Debug.Log(game_end);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    origin[i, j] = board[i, j];
                    origin_stone[i, j] = stone[i, j];
                    if (board[i, j] == 1 || board[i, j] == 2)
                        stone[i, j].GetComponent<Animator>().SetBool("Getback", false);
                    if (find_input(board, i, j, 3-player_color))
                    {
                        game_end++;
                    }
                }
            }


            //텀이 없이 두게되면 컴퓨터가 2턴 연속으로 둘 때 플레이어가 이해할 수 없으므로 1초의 텀을 둠
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                timer = 0;

                //alpha-beta 알고리즘 사용 후 action함수 사용 (compute함수안에 다 있음)
                GameObject.Find("ComputerEvent").GetComponent<Computer_Event>().compute(board);
                turn++;
                now = turn;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (find_input(board, i, j, player_color))
                        {
                            //다음 턴이 둘 수 있는지 확인
                            check_turn++;
                            //놓을수 있는 '?' 인스턴스 넣기
                            Destroy(stone[i, j]);
                            stone[i, j] = Instantiate(prefabArray[3], stone[i, j].transform.position, Quaternion.identity);
                        }
                        else if (find_input(origin, i, j, 3-player_color))
                        {
                            //놓을수 있는 '?' 인스턴스 넣기
                            Destroy(stone[i, j]);
                            stone[i, j] = Instantiate(prefabArray[board[i, j]], stone[i, j].transform.position, Quaternion.identity);
                        }
                    }
                }
            

                //상대가 둘 곳 없을 때 턴 넘기기
                if (check_turn == 0)
                {
                    turn++;
                    skip++;
                    now = turn;
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {

                            if (find_input(board, i, j, 3-player_color))
                            {
                                check_turn++;
                                Destroy(stone[i, j]); //기존 인스턴스 삭제
                                stone[i, j] = Instantiate(prefabArray[3], stone[i, j].transform.position, Quaternion.identity); //놓을수 있는 ?? 표현하기
                            }
                        }
                    }
                }
            }
        }
    }
}
