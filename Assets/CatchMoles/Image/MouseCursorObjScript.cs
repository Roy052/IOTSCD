using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorObjScript : MonoBehaviour
{
    public static MouseCursorObjScript instance;
    public Sprite Hammer;
    public Sprite Hammer_rotate;
    private SpriteRenderer spriteRenderer;
    Vector2 pos = new Vector2(0, 0);
    Vector2 MousePos = new Vector2(0, 0);

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
        Cursor.visible = false;
        if (Input.GetMouseButton(0))
        {
            spriteRenderer.sprite = Hammer;
            pos = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x + 30, MousePos.y - 30));
        }
        else
        {
            spriteRenderer.sprite = Hammer_rotate;
            pos = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x + 10, MousePos.y - 10));
        }

        transform.localPosition = new Vector3(pos.x, pos.y, -2);
        Debug.Log(transform.localPosition);

    }

    public void setPosition(Vector2 newPos)
    {
        MousePos = newPos;
    }
}
