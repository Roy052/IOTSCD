using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Sprite[] cardImages;
    [SerializeField]
    private Sprite backImage;
    [SerializeField]
    private Sprite clearImage;
    [SerializeField]
    private Sprite Mouse_On_Image;

    private int cardType;

    private SpriteRenderer spriteRenderer;
    private GameObject On_Mouse;
    int positionWidth, positionHeight;
    private GameManager gameManager;
    public void Setup(int cardType, int positionWidth, int positionHeight, Vector3 position)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.cardType = cardType;
        this.positionWidth = positionWidth;
        this.positionHeight = positionHeight;
    }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        //카드 이미지 추가.
       if(cardType < 11)
        spriteRenderer.sprite=cardImages[cardType];
       else
            spriteRenderer.sprite = cardImages[11];

        StartCoroutine( gameManager.CardMatch(positionWidth, positionHeight));
    }
    
    public void Flip()
    {
        spriteRenderer.sprite = backImage;
    }

    private void OnMouseEnter()
    {
        //만약 매칭 되지 않았다면 선택되는 느낌이 없도록
        if(gameManager.check[positionWidth,positionHeight] == false)
            transform.localScale = new Vector2(transform.localScale.x + 0.3f, transform.localScale.y + 0.3f);
    }

    private void OnMouseExit()
    {
        //만약 매칭 되지 않았다면 선택되는 느낌이 없도록
        if (gameManager.check[positionWidth, positionHeight] == false)
            transform.localScale = new Vector2(transform.localScale.x - 0.3f, transform.localScale.y - 0.3f);
    }

    public void beforeMatched()
    {
        transform.localScale = new Vector2(transform.localScale.x - 0.3f, transform.localScale.y - 0.3f);
    }
}
