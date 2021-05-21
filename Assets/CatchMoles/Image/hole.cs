using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hole : MonoBehaviour
{
    public mole obj;
    public int row;
    public int col;
    public static int[,] moleMap = new int[3,3];
    public Score scoreInstance;
    public static bool playable = false;

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
    }

    private void Awake()
    {
        GameManage.instance.addHoleInstance(this, row, col);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BecomeMole()
    {
        if (hole.playable)
        {
            int rand = (int)Random.Range(0, 100);

            if (rand < prob && moleMap[row, col] == 0)
            {
                moleMap[row, col] = 1;
                spriteRenderer.sprite = Sprite_Catchable;
            }
            Invoke("BecomeMole", 1.0f);
        }
    }

    private void OnMouseDown()
    {
        if (hole.playable)
        {
            if (moleMap[row, col] == 1)
            {
                moleMap[row, col] = 2;
                scoreInstance.addScore(100);

                spriteRenderer.sprite = Sprite_Catched;
                Invoke("AfterCatched", 0.1f);
            }
        }
    }

    private void AfterCatched()
    {
        if (hole.playable)
        {
            spriteRenderer.sprite = Sprite_Disappear;
            Invoke("GoIdle", 0.1f);
        }
    }
    private void GoIdle()
    {
        spriteRenderer.sprite = Sprite_Hole;
        moleMap[row, col] = 0;
    }

    public void allStop()
    {
        hole.playable = false;
        GoIdle();
    }
}
