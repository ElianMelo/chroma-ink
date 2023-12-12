using System.Collections;
using UnityEngine;

public class YellowSkill : Skill
{
    public float disableDelay = 0.2f;

    private MovementManager movementManager;

    private void Awake()
    {
        movementManager = GetComponentInParent<MovementManager>();
    }

    public IEnumerator DisableEffect()
    {
        yield return new WaitForSeconds(disableDelay);
        movementManager.speed /= 2f;
    }

    public void EnableEffect()
    {
        movementManager.speed *= 2f;
    }

    public override void PermformSkill(Pencil pencil)
    {
        StartCoroutine(PerformAttackCoroutine());
        pencil.PerformYellowSkill();
    }

    public IEnumerator PerformAttackCoroutine()
    {
        EnableEffect();
        yield return DisableEffect();
        yield return null;
    }
}
