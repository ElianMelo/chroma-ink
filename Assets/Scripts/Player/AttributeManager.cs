using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    public static AttributeManager Instance;

    [Header("Player")]
    public float health;
    public float moveSpeed;

    [Header("Movement")]
    public float dashRecover;

    [Header("Red Weapon")]
    public float redAttackDelay;
    public float redSkillDelay;
    public float redAttackDamage;
    public float redSkillDamage;

    [Header("Blue Weapon")]
    public float blueAttackDelay;
    public float blueSkillDelay;
    public float blueAttackDamage;
    public float blueSkillDamage;

    [Header("Yellow Weapon")]
    public float yellowAttackDelay;
    public float yellowSkillDuration;
    public float yellowSkillDelay;
    public float yellowAttackDamage;

    private void Awake()
    {
        Instance = this;
    }
}
