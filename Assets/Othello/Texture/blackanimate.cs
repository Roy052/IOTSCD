using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackanimate : MonoBehaviour
{
    private Animator anim;
    private GameObject obj;
    private int x, y;
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void flip(int i, int j)
    {
        anim.SetBool("OnFlip", true);
        x = i;
        y = j;
    }

    public void create()
    {
        GameObject.Find("MainEvent").GetComponent<MainEvent>().create_Object(x,y);
    }
}
