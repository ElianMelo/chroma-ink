using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    public static AttributeManager Instance;    

    [Header("Player")]
    public float health;
    public float maxHealth;
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

    [Header("Effects")]
    public float EffectDisableTimer;
    public float EffectDestroyTimer;
    public float redEffectDamage;
    public float blueEffectForce;
    public float blueEffectDuration;
    public float yellowEffectPercentage;
    public float yellowEffectDuration;
    public float orangeEffectPercentage;
    public float orangeEffectDuration;
    public float purpleEffectForce;
    public float purpleEffectDuration;
    public float greenEffectPercentage;

    [Header("Flowers")]
    public float flowerRespawnRate;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeTypeByQuantity(AttritubeType type, float quantity)
    {
        quantity /= 100;
        switch (type)
        {
            case AttritubeType.Health:
                AttributeManager.Instance.health *= quantity;
                break;
            case AttritubeType.MoveSpeed:
                AttributeManager.Instance.moveSpeed *= quantity;
                break;
            case AttritubeType.DashRecover:
                AttributeManager.Instance.dashRecover *= quantity;
                break;
            case AttritubeType.RedAttackDelay:
                AttributeManager.Instance.redAttackDelay *= quantity;
                break;
            case AttritubeType.RedSkillDelay:
                AttributeManager.Instance.redSkillDelay *= quantity;
                break;
            case AttritubeType.RedAttackDamage:
                AttributeManager.Instance.redAttackDamage *= quantity;
                break;
            case AttritubeType.RedSkillDamage:
                AttributeManager.Instance.redSkillDamage *= quantity;
                break;
            case AttritubeType.BlueAttackDelay:
                AttributeManager.Instance.blueAttackDelay *= quantity;
                break;
            case AttritubeType.BlueSkillDelay:
                AttributeManager.Instance.blueSkillDelay *= quantity;
                break;
            case AttritubeType.BlueAttackDamage:
                AttributeManager.Instance.blueAttackDamage *= quantity;
                break;
            case AttritubeType.BlueSkillDamage:
                AttributeManager.Instance.blueSkillDamage *= quantity;
                break;
            case AttritubeType.YellowAttackDelay:
                AttributeManager.Instance.yellowAttackDelay *= quantity;
                break;
            case AttritubeType.YellowSkillDuration:
                AttributeManager.Instance.yellowSkillDuration *= quantity;
                break;
            case AttritubeType.YellowSkillDelay:
                AttributeManager.Instance.yellowSkillDelay *= quantity;
                break;
            case AttritubeType.YellowAttackDamage:
                AttributeManager.Instance.yellowAttackDamage *= quantity;
                break;
        }
    }
}
