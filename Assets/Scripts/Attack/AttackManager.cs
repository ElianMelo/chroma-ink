using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private WeaponManager weaponManager;
    private SkillManager skillManager;
    private Pencil pencil;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        skillManager = GetComponent<SkillManager>();
        pencil = FindObjectOfType<Pencil>();
    }

    void Update()
    {
        if (AttributeManager.Instance.paused) { return; };
        if (InputSystem.Instance.MouseLeft())
        {
            weaponManager.currentWeapon.PermformAttack(pencil);
        }
        if (InputSystem.Instance.MouseRight())
        {
            skillManager.currentSkill.PermformSkill(pencil);
        }
    }
}

