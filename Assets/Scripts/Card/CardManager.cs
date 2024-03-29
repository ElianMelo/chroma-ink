using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public CardBoard board;
    [SerializeField]
    private List<Card> cards;

    public List<Card> Cards => cards;
    private void Awake()
    {
        Instance = this;
    }
    public void AddAttributeCard(Card card)
    {
        AttributeManager.Instance.ChangeTypeByQuantity(card.Type, card.Percentage);
    }

    public void CallCards()
    {
        AttributeManager.Instance.SetPaused(true);
        InterfaceSystem.Instance.DisableInterface();
        board.gameObject.SetActive(true);
        board.SetBoard();
    }
}
