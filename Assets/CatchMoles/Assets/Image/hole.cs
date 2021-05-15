using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hole : MonoBehaviour
{
    public mole obj;
    public int row;
    public int col;
    public static int[,] moleMap = new int[3,3];

    public Sprite Sprite_Hole;
    public Sprite Sprite_Catchable;
    public Sprite Sprite_Catched;
    public Sprite Sprite_Disappear;
    private SpriteRenderer spriteRenderer;

    Vector3 pos;

    [Header("초당 생성 확률")]
    [SerializeField] [Range(0.0f, 100.0f)] float prob = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        pos = this.gameObject.transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite_Hole;

        Invoke("BecomeMole", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BecomeMole()
    {
        int rand = (int)Random.Range(0, 100);
        
        if (rand < prob && moleMap[row, col] == 0)
        {
            moleMap[row,col] = 1;
            Debug.Log(row + ", " + col);
            spriteRenderer.sprite = Sprite_Catchable;
            //this.transform.localPosition = new Vector3(pos.x - 4, pos.y, pos.z);

            /*
            mole instance = Instantiate(obj, new Vector3(pos.x - 0.03f, pos.y + 0.3f, pos.z - 1), Quaternion.identity);
            instance.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            instance.row = this.row;
            instance.col = this.col;
            */
        }
        Invoke("BecomeMole", 1.0f);
    }

    private void OnMouseDown()
    {
        if (moleMap[row, col] == 1)
        {
            Debug.Log("꺅");
            moleMap[row, col] = 2;
            //this.transform.localPosition = new Vector3(pos.x + 4, pos.y, pos.z);
            spriteRenderer.sprite = Sprite_Catched;
            Invoke("AfterCatched", 0.1f);
        }
    }

    private void AfterCatched()
    {
        spriteRenderer.sprite = Sprite_Disappear;
        Invoke("GoIdle", 0.1f);
    }
    private void GoIdle()
    {
        spriteRenderer.sprite = Sprite_Hole;
        moleMap[row, col] = 0;
    }
}
