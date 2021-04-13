using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove: MonoBehaviour
{
    public float moveSpeed;
    public int[] order;
    private int ordercount = 0;
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

}
