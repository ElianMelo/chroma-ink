using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = this.GetComponentInChildren<Slider>();
        slider.value = 1;
    }
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }
}
