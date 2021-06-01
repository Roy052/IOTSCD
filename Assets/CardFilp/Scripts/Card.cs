using UnityEngine;

public enum CardType{
    zero = 0, one, two, three, four, five, six, seven, eight, nine, ten, eleven,
    Back = 100,
    Clear = 1000
}
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

    private CardType cardType;

    private SpriteRenderer spriteRenderer;
    private GameObject On_Mouse;
    public void Setup(CardType cardType)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CardType = cardType;
    }

    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
       spriteRenderer.sprite=cardImages[1];
    }

    private void OnMouseEnter()
    {
        transform.localScale = new Vector2(transform.localScale.x + 0.3f, transform.localScale.y + 0.3f);
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector2(transform.localScale.x - 0.3f, transform.localScale.y - 0.3f);
    }
    public CardType CardType
    {
        set
        {
            cardType = value;

            if((int)cardType < (int)CardType.Back)
            {
                spriteRenderer.sprite = cardImages[(int)cardType];
            }
            else if((int)cardType < (int)CardType.Clear)
            {
                spriteRenderer.sprite = backImage;
            }
            else if((int)cardType == (int)CardType.Clear)
            {
                spriteRenderer.sprite = clearImage;
            }
        }
        get => cardType;
    }
}
