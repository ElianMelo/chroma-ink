using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon currentWeapon;
    private RedWeapon redWeapon;
    private BlueWeapon blueWeapon;
    private YellowWeapon yellowWeapon;
    private Pencil pencil;

    private void Awake()
    {
        redWeapon = GetComponentInChildren<RedWeapon>();
        blueWeapon = GetComponentInChildren<BlueWeapon>();
        yellowWeapon = GetComponentInChildren<YellowWeapon>();
        pencil = FindObjectOfType<Pencil>();
        currentWeapon = redWeapon;
    }

    void Update()
    {
        if (AttributeManager.Instance.paused) { return; };
        if (InputSystem.Instance.Button1())
        {
            currentWeapon = redWeapon;
            pencil.ChangeTrailRed();
        }
        if (InputSystem.Instance.Button2())
        {
            currentWeapon = blueWeapon;
            pencil.ChangeTrailBlue();
        }
        if (InputSystem.Instance.Button3())
        {
            currentWeapon = yellowWeapon;
            pencil.ChangeTrailYellow();
        }
    }
}
