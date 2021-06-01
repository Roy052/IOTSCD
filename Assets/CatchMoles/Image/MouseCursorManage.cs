using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorManage : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseCursorObjScript.instance.setPosition(Input.mousePosition);
    }
}
