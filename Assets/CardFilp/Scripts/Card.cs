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

    private CardType cardType;

    private SpriteRenderer spriteRenderer;

    public void Setup(CardType cardType)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CardType = cardType;
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
