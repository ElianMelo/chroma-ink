using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public Vector3 randomizeIntensity = new Vector3(-5f, 5f, 0);
    public Color red;
    public Color blue;
    public Color yellow;
    public Color green;
    // private TextMeshProUGUI textComponent;
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
            Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
            Random.Range(-randomizeIntensity.z, randomizeIntensity.z)
            );
    }

    public void SetColor(TextColors color)
    {
        Color realColor;
        switch(color)
        {
            case TextColors.RED:
                realColor = red;   
                break;
            case TextColors.BLUE:
                realColor = blue;
                break;
            case TextColors.YELLOW:
                realColor = yellow;
                break;
            case TextColors.GREEN:
                realColor = green;
                break;
            default: realColor = Color.white; break;
        }
        this.GetComponent<TextMeshProUGUI>().color = realColor;
    }

    public void IncrementSize()
    {
        this.GetComponent<TextMeshProUGUI>().fontSize += 2;
    }

    public void ChangeText(string val)
    {
        this.GetComponent<TextMeshProUGUI>().text = val;
    }
}
