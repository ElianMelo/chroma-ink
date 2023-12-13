using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardBoard board;
    [SerializeField]
    private List<Card> cards;

    public List<Card> Cards => cards;

    public void AddAttributeCard(Card card)
    {
        AttributeManager.Instance.ChangeTypeByQuantity(card.Type, card.Percentage);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            board.gameObject.SetActive(true);
            board.SetBoard();
        }
    }
}
