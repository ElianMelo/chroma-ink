using System.Collections;
using UnityEngine;

public class YellowSkill : Skill
{
    private bool canPerform = true;
    private MovementManager movementManager;
    public GameObject yellowVisual;

    private void Awake()
    {
        movementManager = GetComponentInParent<MovementManager>();
    }

    public IEnumerator DisableEffect()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.yellowSkillDuration);
        movementManager.speed /= (100 + AttributeManager.Instance.yellowEffectPercentage) / 100;
    }

    public IEnumerator ApplyDelay()
    {
        WeaponsCDUI.Instance.yellowSkillCd = AttributeManager.Instance.yellowSkillDelay;
        yield return new WaitForSeconds(AttributeManager.Instance.yellowSkillDelay);
        canPerform = true;
    }

    public void EnableEffect()
    {
        movementManager.speed *= (100 + AttributeManager.Instance.yellowEffectPercentage) / 100;
    }

    public override void PermformSkill(Pencil pencil)
    {
        if(canPerform)
        {
            StartCoroutine(PerformAttackCoroutine());
            pencil.PerformYellowSkill();
            WeaponsCDUI.Instance.yellowSkillCd = AttributeManager.Instance.yellowSkillDuration;
        }
    }

    public IEnumerator PerformAttackCoroutine()
    {
        canPerform = false;
        EnableEffect();
        var entity = Instantiate(yellowVisual, this.transform.position, transform.rotation);
        // Remove
        Destroy(entity, 2);
        yield return DisableEffect();
        yield return ApplyDelay();
        yield return null;
    }
}
