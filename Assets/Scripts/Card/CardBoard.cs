using System.Collections.Generic;
using UnityEngine;

public class CardBoard : MonoBehaviour
{
    public List<CardItem> cardsItems;
    public CardManager cardManager;
    private List<Card> cards;
    private List<Card> cardsFiltered;

    private void Start()
    {
        SetBoard();
    }

    public void SetBoard()
    {
        cardsFiltered = new List<Card>();
        for (int i = 0; i < 3; i++)
        {
            int val = Random.Range(0, cards.Count);
            Card selectedCard = cards[val];

            cardsFiltered.Add(selectedCard);
            string cardText = selectedCard.Text + " " + selectedCard.Percentage + "%";
            cardsItems[i].SetCardValues(cardText, selectedCard.Image, i);
        }
    }

    public void SelectCard(int index)
    {
        cardManager.AddAttributeCard(cardsFiltered[index]);
        this.gameObject.SetActive(false);
    }

    void Awake()
    {
        cards = cardManager.Cards;
    }
}
