using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    private CardBoard board;
    private int index;

    private void Start()
    {
        board = GetComponentInParent<CardBoard>();
    }

    public void SetCardValues(string text, Sprite image, int index)
    {
        this.text.text = text;
        this.image.sprite = image;
        this.index = index;
    }

    public void SelectCard()
    {
        board.SelectCard(index);
    }
}
