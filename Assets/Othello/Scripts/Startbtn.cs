using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startbtn : MonoBehaviour
{
    public Startbtn button;
    // Start is called before the first frame update
    void Start()
    {
        button = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Jump")||Input.GetButton("Jump"))
            btnClick();
    }

    public void btnClick()
    {

       SceneManager.LoadScene("Single_Play");

    }
}
