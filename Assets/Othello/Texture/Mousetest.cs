using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousetest : MonoBehaviour
{
    // //은 주석 입니다.
    // Vector2는 X, Y 값을 가집니다.
    // Vector3는 X, Y, Z 값을 가집니다.

    public float moveSpeed = 3.0f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        //Debug.Log(transform.position);
    }
}