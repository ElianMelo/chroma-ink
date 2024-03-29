using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Skill currentSkill;
    private RedSkill redSkill;
    private BlueSkill blueSkill;
    private YellowSkill yellowSkill;
    private Pencil pencil;

    private void Start()
    {
        redSkill = GetComponentInChildren<RedSkill>();
        blueSkill = GetComponentInChildren<BlueSkill>();
        yellowSkill = GetComponentInChildren<YellowSkill>();
        pencil = FindObjectOfType<Pencil>();
        currentSkill = redSkill;
    }
    void Update()
    {
        if (AttributeManager.Instance.paused) { return; };
        if (InputSystem.Instance.Button1())
        {
            currentSkill = redSkill;
            pencil.ChangeTrailRed();
        }
        if (InputSystem.Instance.Button2())
        {
            currentSkill = blueSkill;
            pencil.ChangeTrailBlue();
        }
        if (InputSystem.Instance.Button3())
        {
            currentSkill = yellowSkill;
            pencil.ChangeTrailYellow();
        }
    }
}
