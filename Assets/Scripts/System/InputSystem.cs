using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance;

    public Joystick joystick;

    private bool isKeyboard = false;

    private bool mouseLeft = false;
    private bool mouseRight = false;
    private bool button1 = false;
    private bool button2 = false;
    private bool button3 = false;
    private bool space = false;
    private bool escape = false;

    public enum Buttons
    {
        MOUSE_LEFT,
        MOUSE_RIGHT,
        BUTTON1,
        BUTTON2,
        BUTTON3,
        SPACE,
        ESCAPE
    }

    private void Awake()
    {
        Instance = this;        
    }

    public bool IsKeyboard()
    {
        return isKeyboard;
    }

    public void MobilePressButton(Buttons button)
    {
        StartCoroutine(MobilePressButtonCoroutine(button));
    }

    private IEnumerator MobilePressButtonCoroutine(Buttons button)
    {
        ButtonToBool(button, true);
        yield return new WaitForSeconds(0.01f);
        ButtonToBool(button, false);
    }

    private void ButtonToBool(Buttons button, bool value)
    {
        switch (button) { 
            case Buttons.MOUSE_LEFT:
                mouseLeft = value;
                break;
            case Buttons.MOUSE_RIGHT:
                mouseRight = value;
                break;
            case Buttons.BUTTON1:
                button1 = value;
                break;
            case Buttons.BUTTON2:
                button2 = value;
                break;
            case Buttons.BUTTON3:
                button3 = value;
                break;
            case Buttons.SPACE:
                space = value;
                break;
            case Buttons.ESCAPE:
                escape = value;
                break;
        }
    }

    public bool MouseLeft()
    {
        if (isKeyboard)
        {
            return Input.GetKeyDown(KeyCode.Mouse0);
        }
        else
        {
            return mouseLeft;
        }
    }

    public bool MouseRight()
    {
        if (isKeyboard)
        {
            return Input.GetKeyDown(KeyCode.Mouse1);
        }
        else
        {
            return mouseRight;
        }
    }

    public bool Button1()
    {
        if (isKeyboard)
        {
            return Input.GetKeyDown(KeyCode.Alpha1);
        }
        else
        {
            return button1;
        }
    }

    public bool Button2()
    {
        if (isKeyboard)
        {
            return Input.GetKeyDown(KeyCode.Alpha2);
        }
        else
        {
            return button2;
        }
    }

    public bool Button3()
    {
        if (isKeyboard)
        {
            return Input.GetKeyDown(KeyCode.Alpha3);
        }
        else
        {
            return button3;
        }
    }

    public float Horizontal()
    {
        if (isKeyboard)
        {
            return Input.GetAxis("Horizontal");
        }
        else
        {
            return joystick.Horizontal;
        }
    }

    public float Vertical()
    {
        if (isKeyboard)
        {
            return Input.GetAxis("Vertical");
        }
        else
        {
            return joystick.Vertical;
        }
    }

    public bool Space()
    {
        if (isKeyboard)
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
        else
        {
            return space;
        }
    }

    public bool Escape()
    {
        if (isKeyboard)
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
        else
        {
            return escape;
        }
    }

    public Vector3 MousePosWorldPoint(Camera camera, Transform referenceTransform)
    {
        if (isKeyboard)
        {
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            return new Vector3(referenceTransform.position.x + joystick.Horizontal,
                referenceTransform.position.y + joystick.Vertical, 0f);
        }
    }
}
