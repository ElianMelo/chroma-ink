using System;
using System.Collections.Generic;
using UnityEngine;

public class CardBoard : MonoBehaviour
{
    public List<CardItem> cardsItems;
    public CardManager cardManager;
    private List<Card> cards;
    private List<Card> cardsFiltered;
    private Card selectedCard;

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
            int val = UnityEngine.Random.Range(0, cards.Count);
            Card selectedCardNow = cards[val];

            for (int i = 0;i < cardsFiltered.Count; i++)
            {
                if(cardsFiltered[i] == selectedCardNow)
                {
                    repeat = true;
                }
            }
            if(!repeat)
            {
                cardsFiltered.Add(selectedCardNow);
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
        selectedCard = cardsFiltered[index];
    }

    public void ConfirmCard()
    {
        if (selectedCard == null) return;
        cardManager.AddAttributeCard(selectedCard);
        this.gameObject.SetActive(false);
        AttributeManager.Instance.paused = false;
        LevelManager.Instance.EndLevelNoCards();
    }

    void Awake()
    {
        cards = cardManager.Cards;
    }
}
