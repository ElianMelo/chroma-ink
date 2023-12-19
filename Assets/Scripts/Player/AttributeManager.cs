using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    public static AttributeManager Instance;

    [Header("System")]
    public bool paused;

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

    [Header("Enemies")]
    public float enemiesSpawnRate;
    public float maxEnemies;
    public float enemiesDamage;

    private AttributeManager baseAttribute;

    private void Awake()
    {
        Instance = this;
        baseAttribute = new AttributeManager();
        Clone(this, baseAttribute);
    }

    public void ResetAttributeManager()
    {
        Clone(baseAttribute, this);
    }

    public void Clone(AttributeManager baseClass, AttributeManager targetClass)
    {
        targetClass.paused = baseClass.paused;
        targetClass.health = baseClass.health;
        targetClass.maxHealth = baseClass.maxHealth;
        targetClass.moveSpeed = baseClass.moveSpeed;
        targetClass.dashRecover = baseClass.dashRecover;
        targetClass.redAttackDelay = baseClass.redAttackDelay;
        targetClass.redSkillDelay = baseClass.redSkillDelay;
        targetClass.redAttackDamage = baseClass.redAttackDamage;
        targetClass.redSkillDamage = baseClass.redSkillDamage;
        targetClass.blueAttackDelay = baseClass.blueAttackDelay;
        targetClass.blueSkillDelay = baseClass.blueSkillDelay;
        targetClass.blueAttackDamage = baseClass.blueAttackDamage;
        targetClass.blueSkillDamage = baseClass.blueSkillDamage;
        targetClass.yellowAttackDelay = baseClass.yellowAttackDelay;
        targetClass.yellowSkillDuration = baseClass.yellowSkillDuration;
        targetClass.yellowSkillDelay = baseClass.yellowSkillDelay;
        targetClass.yellowAttackDamage = baseClass.yellowAttackDamage;
        targetClass.EffectDisableTimer = baseClass.EffectDisableTimer;
        targetClass.EffectDestroyTimer = baseClass.EffectDestroyTimer;
        targetClass.redEffectDamage = baseClass.redEffectDamage;
        targetClass.blueEffectForce = baseClass.blueEffectForce;
        targetClass.blueEffectDuration = baseClass.blueEffectDuration;
        targetClass.yellowEffectPercentage = baseClass.yellowEffectPercentage;
        targetClass.yellowEffectDuration = baseClass.yellowEffectDuration;
        targetClass.orangeEffectPercentage = baseClass.orangeEffectPercentage;
        targetClass.orangeEffectDuration = baseClass.orangeEffectDuration;
        targetClass.purpleEffectForce = baseClass.purpleEffectForce;
        targetClass.purpleEffectDuration = baseClass.purpleEffectDuration;
        targetClass.greenEffectPercentage = baseClass.greenEffectPercentage;
        targetClass.flowerRespawnRate = baseClass.flowerRespawnRate;
        targetClass.enemiesSpawnRate = baseClass.enemiesSpawnRate;
        targetClass.maxEnemies = baseClass.maxEnemies;
        targetClass.enemiesDamage = baseClass.enemiesDamage;
    }

    public void ChangeTypeByQuantity(AttritubeType type, float quantity)
    {
        quantity = (100 - quantity) / 100;
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
