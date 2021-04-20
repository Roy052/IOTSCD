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

    // Start is called before the first frame update
    void Start()
    {
        order = new int[10];
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
        for (int i = 0; i < ordercount; i++)
        {
            Debug.Log(order[i]);
            if (order[i] == 0)
            {
                
                playerRigidBody.AddForce(new Vector2(clockwiseMove[direction, 0], clockwiseMove[direction, 1]) * moveSpeed * 100);
                yield return new WaitForSeconds(3);
                playerRigidBody.velocity = Vector2.zero;
            }
            if (order[i] == 1)
            {
                if (direction == 0) direction = 3;
                else direction = direction - 1;
                yield return new WaitForSeconds(1);
            }
            if (order[i] == 2)
            {
                direction = (direction + 1) % 4;
                yield return new WaitForSeconds(1);
            }

        }
        yield return new WaitForSeconds(1);
        gamesystem.gameRestart();

    }

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

    public void ResetButtonClicked()
    {
        ordercount = 0;
    }

    public void movewithArduino(int num)
    {
        if(num == 0) transform.Translate(new Vector2(0, (float) -0.75));
        else if (num == 1) transform.Translate(new Vector2((float) -0.75, 0));
        else if (num == 2) transform.Translate(new Vector2(0, (float) 0.75));
        else if (num == 3) transform.Translate(new Vector2((float) 0.75, 0));
    }
}
