using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hole : MonoBehaviour
{
    public int row;
    public int col;
    public Score scoreInstance;  // 게임스코어 접근용
    public getScore getScoreObj;  // 두더지 잡히면 뜨는 점수표시
    public Canvas UIparent;  // 점수표시 부모

    public Sprite Sprite_Hole;
    public Sprite Sprite_Catchable;
    public Sprite Sprite_Catched;
    public Sprite Sprite_Disappear;
    private SpriteRenderer spriteRenderer;

    public static int[,] moleMap = new int[3, 3];
    public static bool playable = false;
    Vector3 myPos;
    double[] collRec = new double[4];  // xmin xmax ymin ymax

    [Header("초당 생성 확률")]
    [SerializeField] [Range(0.0f, 100.0f)] float prob = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 스프라이트 변경용 변수 설정
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite_Hole;
        myPos = Camera.main.WorldToScreenPoint(transform.position);
        /*myPos = this.gameObject.transform.localPosition;
        collRec[0] = myPos.x - 2.5;
        collRec[1] = myPos.x + 2.5;
        collRec[2] = myPos.y - 1.2;
        collRec[3] = myPos.y + 3.8;*/
    }

    private void Awake()
    {
        // GameManager에 오브젝트 등록
        GameManage.addHoleInstance(this, row, col);
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 MousePos = Input.mousePosition;
        if (MousePos.x > collRec[0] && MousePos.x < collRec[1] && MousePos.y > collRec[2] && MousePos.y < collRec[3])
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }
        else
            transform.localScale = new Vector3(1, 1, 1);*/
    }

    void BecomeMole()
    {

        // 게임플레이 중이면 1초마다 prob 확률로 두더지로 변함
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
        // 게임 도중 두더지로 변한 상태에서 클릭당하면 발생
        if (hole.playable)
        {
            if (moleMap[row, col] == 1)
            {
                moleMap[row, col] = 2;
                getScore scoreObj = Instantiate(getScoreObj);
                scoreObj.init(100);
                scoreObj.transform.localPosition = this.gameObject.transform.localPosition + new Vector3(0, 1, 0);
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
