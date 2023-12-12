using System.Collections;
using UnityEngine;

public class YellowSkill : Skill
{
    private bool canPerform = true;
    private MovementManager movementManager;

    private void Awake()
    {
        movementManager = GetComponentInParent<MovementManager>();
    }

    public IEnumerator DisableEffect()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.yellowSkillDuration);
        movementManager.speed /= 2f;
    }

    public IEnumerator ApplyDelay()
    {
        WeaponsCDUI.Instance.yellowSkillCd = AttributeManager.Instance.yellowSkillDelay;
        yield return new WaitForSeconds(AttributeManager.Instance.yellowSkillDelay);
        canPerform = true;
    }

    public void EnableEffect()
    {
        movementManager.speed *= 2f;
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
        yield return DisableEffect();
        yield return ApplyDelay();
        yield return null;
    }
}
