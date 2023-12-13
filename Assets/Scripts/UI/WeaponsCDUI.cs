using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponsCDUI : MonoBehaviour
{
    public static WeaponsCDUI Instance;

    public TextMeshProUGUI redSkill;
    public TextMeshProUGUI blueSkill;
    public TextMeshProUGUI yellowSkill;

    public float redSkillCd = 0f;
    public float blueSkillCd = 0f;
    public float yellowSkillCd = 0f;

    void Update()
    {
        redSkillCd -= Time.deltaTime; 
        blueSkillCd -= Time.deltaTime; 
        yellowSkillCd -= Time.deltaTime;

        redSkill.text = (redSkillCd <= 0) ? "" : redSkillCd.ToString("0.0");
        blueSkill.text = (blueSkillCd <= 0) ? "" : blueSkillCd.ToString("0.0");
        yellowSkill.text = (yellowSkillCd <= 0) ? "" : yellowSkillCd.ToString("0.0");
    }

    private void Awake()
    {
        Instance = this;
    }
}
