using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToButton : MonoBehaviour
{
    GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if(collision.name == "Mouse")
        {
            for(int i = 0; i < buttons.Length; i++)
            {
                if(buttons[i].name == this.name)
                {
                    buttons[i].GetComponent<DirectionButton>().onClickButton();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == this.name)
            {
                buttons[i].GetComponent<DirectionButton>().onClickButton();
            }
        }
    }
}
