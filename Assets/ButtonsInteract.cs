using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsInteract : MonoBehaviour
{
    [SerializeField] private Button redSkillButton;
    [SerializeField] private Button blueSkillButton;
    [SerializeField] private Button yellowSkillButton;
    [SerializeField] private Button dashSkillButton;
    [SerializeField] private Button primaryAttackButton;
    [SerializeField] private Button secondaryAttackButton;
    [SerializeField] private InputSystem inputSystem;

    void Start()
    {
        redSkillButton.onClick.AddListener(() =>
        {
            inputSystem.MobilePressButton(InputSystem.Buttons.BUTTON1);
        });
        blueSkillButton.onClick.AddListener(() =>
        {
            inputSystem.MobilePressButton(InputSystem.Buttons.BUTTON2);
        });
        yellowSkillButton.onClick.AddListener(() =>
        {
            inputSystem.MobilePressButton(InputSystem.Buttons.BUTTON3);
        });
        dashSkillButton.onClick.AddListener(() =>
        {
            inputSystem.MobilePressButton(InputSystem.Buttons.SPACE);
        });
        primaryAttackButton.onClick.AddListener(() =>
        {
            inputSystem.MobilePressButton(InputSystem.Buttons.MOUSE_LEFT);
        });
        secondaryAttackButton.onClick.AddListener(() =>
        {
            inputSystem.MobilePressButton(InputSystem.Buttons.MOUSE_RIGHT);
        });
    }
}
