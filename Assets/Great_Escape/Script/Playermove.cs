using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove: MonoBehaviour
{
    public float moveSpeed;
    public int[] order;
    private int ordercount = 0;
    bool collision_occur = false;

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
                
                //while(collision_occur == false)
                //    transform.Translate( (transform.position - new Vector3(1,0,0)) * Time.deltaTime);

            } 
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Button") == true)
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
}
