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
        var repeat = false;
        while (cardsFiltered.Count < 3)
        {
            int val = Random.Range(0, cards.Count);
            Card selectedCard = cards[val];

            for (int i = 0;i < cardsFiltered.Count; i++)
            {
                if(cardsFiltered[i] == selectedCard)
                {
                    repeat = true;
                }
            }
            if(!repeat)
            {
                cardsFiltered.Add(selectedCard);
            }
            repeat = false;
        }

        for (int j = 0; j < cardsFiltered.Count; j++)
        {
            string cardText = cardsFiltered[j].Text + " " + cardsFiltered[j].Percentage + "%";
            cardsItems[j].SetCardValues(cardText, cardsFiltered[j].Image, j);
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
