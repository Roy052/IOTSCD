using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove: MonoBehaviour
{
    public float moveSpeed;
    public int[] order;
    private int ordercount = 0;
    bool collision_occur = false;
    public Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        order = new int[10];
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * moveSpeed);
    }

    public void Movement()
    {
        for (int i = 0; i < ordercount; i++)
        {
            Debug.Log(order[i]);
            if (order[i] == 0)
            {
                
                playerRigidBody.AddForce(new Vector2(-moveSpeed*30, 0));
                if(collision_occur == true)
                    playerRigidBody.AddForce(new Vector2(0, moveSpeed * 30));
            } 
            if(order[i] == 1)
                playerRigidBody.AddForce(new Vector2(0, moveSpeed * 30));
            if(order[i] == 2)
                playerRigidBody.AddForce(new Vector2(0, -moveSpeed * 30));

        }
    }
    
    //IEnumerator WaitForMove

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") == true)
        {
            collision_occur = true;
        }
    }

    public void UpButtonClicked()
    {
        Debug.Log("UpButtonClicked");
        order[ordercount++] = 0;
    }

    public void LeftButtonClicked()
    {
        Debug.Log("LeftButtonClicked");
        order[ordercount++] = 1;
    }
    public void RightButtonClicked()
    {
        Debug.Log("RightButtonClicked");
        order[ordercount++] = 2;
    }

    public void movewithArduino(int num)
    {
        if(num == 0) transform.Translate(new Vector2(0, (float) -0.75));
        else if (num == 1) transform.Translate(new Vector2((float) -0.75, 0));
        else if (num == 2) transform.Translate(new Vector2(0, (float) 0.75));
        else if (num == 3) transform.Translate(new Vector2((float) 0.75, 0));
    }
}
