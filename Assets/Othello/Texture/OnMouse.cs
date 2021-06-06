using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouse : MonoBehaviour
{
    [SerializeField]
    private GameObject trans_white,trans_black;
    private GameObject obj,mouse_obj;

    private GameObject testRedDot;
    public static int turn;
    Vector3 myPos, leftbottom, righttop;
    bool oncheck=false;
    // Start is called before the first frame update
    void Start()
    {
        myPos = transform.position;
        //leftbottom = Camera.main.WorldToScreenPoint(myPos + new Vector3(-1.25f, -0.405f, 0));
        //righttop = Camera.main.WorldToScreenPoint(myPos + new Vector3(1.25f, 2.095f, 0));
        leftbottom = myPos + new Vector3(-0.3f, -0.3f, 0);
        righttop = myPos + new Vector3(0.3f, 0.3f, 0);
        testRedDot = GameObject.Find("Mouse");
    }

    // Update is called once per frame
    void Update()
    {
       //  Vector2 MousePos = MouseCursorObjScript.instance.pos;
        Vector3 MousePos = testRedDot.transform.position;
        //Debug.Log(MousePos);
        if (MousePos.x > leftbottom.x && MousePos.x < righttop.x && MousePos.y > leftbottom.y && MousePos.y < righttop.y)
        {
            if (!oncheck)
            {
                oncheck = true;
                OnMouseEnter();
            }
        }
        else
        {
            OnMouseExit();
            oncheck = false;
        }
    }

    private void OnMouseEnter()
    {
        if (MainEvent.now % 2 == 0)
        {
            mouse_obj = Instantiate(trans_white, this.gameObject.transform.position, Quaternion.identity);
        }
    }
    private void OnDestroy()
    {
        Destroy(mouse_obj);
    }

    private void OnMouseExit()
    {
        Destroy(mouse_obj);
    }
}
