using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    public static HealthUI Instance;

    private Slider slider;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        slider = this.GetComponentInChildren<Slider>();
        slider.value = AttributeManager.Instance.health / AttributeManager.Instance.maxHealth;
    }
    public void UpdateHealth()
    {
        slider.value = AttributeManager.Instance.health / AttributeManager.Instance.maxHealth;
    }
}
