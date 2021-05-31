using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouse : MonoBehaviour
{
    [SerializeField]
    private GameObject trans_white,trans_black;

    private GameObject obj,mouse_obj;
    public static int turn;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseEnter()
    {
        if(MainEvent.now%2==0)
            mouse_obj=Instantiate(trans_white, this.gameObject.transform.position, Quaternion.identity);
    }

    private void OnMouseDown()
    {
        Destroy(mouse_obj);
    }

    private void OnMouseExit()
    {
        Destroy(mouse_obj);
    }
}
