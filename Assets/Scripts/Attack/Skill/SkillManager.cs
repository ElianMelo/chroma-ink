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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSkill = redSkill;
            pencil.ChangeTrailRed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSkill = blueSkill;
            pencil.ChangeTrailBlue();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSkill = yellowSkill;
            pencil.ChangeTrailYellow();
        }
    }
}
