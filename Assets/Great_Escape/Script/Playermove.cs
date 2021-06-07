using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove: MonoBehaviour
{
    public float moveSpeed;

    //이동 명령
    public int[] order;
    private int ordercount = 0;

    public Rigidbody2D playerRigidBody;
    GameSystem gamesystem;

    //방향 전환
    int[,] clockwiseMove  = new int[4,2] { {-1, 0},{0 , 1}, { 1, 0 }, { 0, -1 } };
    int direction = 0;

    //Raycasting
    [SerializeField] private LayerMask layermask;

    // Start is called before the first frame update
    void Start()
    {
        order = new int[20];
        gamesystem = GameObject.FindGameObjectWithTag("GameSystem").GetComponent<GameSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * moveSpeed);
    }

    public IEnumerator Movement()
    {
        //게임이 부드러워 보이기 위해 약간의 지연시간
        float bufferedTime = (float) 0.3;

        //order에 따라 움직임 수행
        for (int i = 0; i < ordercount; i++)
        {
            Debug.Log(order[i]);
            if (order[i] == 0)
            {
                Vector2 movingDirection = new Vector2(clockwiseMove[direction, 0], clockwiseMove[direction, 1]);
                RaycastHit2D hit;
                hit = Physics2D.Raycast(this.transform.position, movingDirection, layermask);

                playerRigidBody.AddForce(movingDirection * moveSpeed * 100);
                Debug.Log(hit.distance + ", " + hit.transform.name);
                float waitTime = hit.distance / (500 * Time.fixedDeltaTime);
                yield return new WaitForSeconds(waitTime - 0.001f);
                playerRigidBody.velocity = Vector2.zero;
            }
            if (order[i] == 1)
            {
                if (direction == 0) direction = 3;
                else direction = direction - 1;
                yield return new WaitForSeconds(bufferedTime);
            }
            if (order[i] == 2)
            {
                direction = (direction + 1) % 4;
                yield return new WaitForSeconds(bufferedTime);
            }

        }
        yield return new WaitForSeconds(bufferedTime);
        gamesystem.stageRestart();

    }

    //Up button 눌림
    public void UpButtonClicked()
    {
        
        order[ordercount++] = 0;

        string temp = "[";
        for (int i = 0; i < ordercount; i++)
        {
            temp += order[i];
            temp += ", ";
        }
        temp += "]";
        Debug.Log(temp);
    }

    //Left button 눌림
    public void LeftButtonClicked()
    {
        order[ordercount++] = 1;

        string temp = "[";
        for (int i = 0; i < ordercount; i++)
        {
            temp += order[i];
            temp += ", ";
        }
        temp += "]";
        Debug.Log(temp);
    }

    //Right button 눌림
    public void RightButtonClicked()
    {
        order[ordercount++] = 2;

        string temp = "[";
        for (int i = 0; i < ordercount; i++)
        {
            temp += order[i];
            temp += ", ";
        }
        temp += "]";
        Debug.Log(temp);
    }

    //Reset button 눌림
    public void ResetButtonClicked()
    {
        ordercount = 0;
        direction = 0;
    }

    //아두이노 이용한 움직임
    public void movewithArduino(int num)
    {
        if(num == 0) transform.Translate(new Vector2(0, (float) -0.75));
        else if (num == 1) transform.Translate(new Vector2((float) -0.75, 0));
        else if (num == 2) transform.Translate(new Vector2(0, (float) 0.75));
        else if (num == 3) transform.Translate(new Vector2((float) 0.75, 0));
    }
}
