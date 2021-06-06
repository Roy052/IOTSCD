using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorObjScript : MonoBehaviour
{
    public static MouseCursorObjScript instance;
    public Sprite Hammer;
    public Sprite Hammer_rotate;
    private SpriteRenderer spriteRenderer;

    public Vector3 pos = new Vector3(0, 0, -2);
    public Vector2 MousePos = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Hammer_rotate;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
    }

    public void setPosition(Vector3 newPos)
    {
        pos += newPos;
        Cursor.visible = false;
        if (Input.GetMouseButton(0))  // 이거 나중에 클릭버튼 생기면 수정
        {
            spriteRenderer.sprite = Hammer;
            //pos = Camera.main.ScreenToWorldPoint(new Vector2(MousePos.x + 30, MousePos.y - 30));
            transform.position = new Vector3(pos.x, pos.y, -2);
        }
        else
        {
            spriteRenderer.sprite = Hammer_rotate;
            //pos =  transform.localPosition + Camera.main.ScreenToWorldPoint(new Vector2(MousePos.x + 10, MousePos.y - 10));
            transform.position = new Vector3(pos.x, pos.y, -2);
        }
        Vector3 posBorder = Camera.main.WorldToViewportPoint(transform.position);
        if (posBorder.x < 0f) posBorder.x = 0f;
        if (posBorder.x > 1f) posBorder.x = 1f;
        if (posBorder.y < 0f) posBorder.y = 0f;
        if (posBorder.y > 1f) posBorder.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(posBorder);

        //transform.position = new Vector3(pos.x, pos.y, -2);
    }
}
