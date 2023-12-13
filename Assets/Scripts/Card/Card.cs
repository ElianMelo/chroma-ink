using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Card
{
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private float percentage;
    [SerializeField]
    private string text;
    [SerializeField]
    private AttritubeType type;

    public AttritubeType Type => type;
    public float Percentage => percentage;
    public string Text => text;
    public Sprite Image => image;
}
