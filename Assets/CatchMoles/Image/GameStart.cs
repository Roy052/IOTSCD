using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public static GameStart instance;
    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myPos = transform.position;
        Vector2 leftbottom = new Vector2(myPos.x - 80, myPos.y - 25);
        Vector2 righttop = new Vector2(myPos.x + 80, myPos.y + 25);
        Vector3 MousePos = MouseCursorObjScript.instance.MousePos;
        //Debug.Log(myPos);
        //Debug.Log(MousePos);
        if (MousePos.x > leftbottom.x && MousePos.x < righttop.x && MousePos.y > leftbottom.y && MousePos.y < righttop.y)
        {
            btnClick();
        }
    }

    public void btnClick()
    {
        if (state == 0)
        {
            state = 1;
            GameManage.instance.game_count_start();
        }
    }
}
