using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MouseControls", menuName = "Controls/MouseControls")]
public class MouseControls : Controls {

    //Controls type
    private const controlsType CONTROLSTYPE = controlsType.MOUSE;

    [Header("Joysticks equivalent")]
    public string horizontalAxis = "Mouse X";
    public string verticalAxis = "Mouse Y";
    public float horizontalAxisSensitivity = 1.0f;
    public float verticalAxisSensitivity = 1.0f;
    public KeyCode click = KeyCode.Mouse0;

    [Header("Buttons equivalents")]
    public KeyCode buttonA = KeyCode.Mouse0;
    public KeyCode buttonB = KeyCode.Mouse1;
    public KeyCode buttonX = KeyCode.Mouse0;
    public KeyCode buttonY = KeyCode.Mouse1;

    [Header("Bumpers equivalents")]
    public KeyCode lBumper = KeyCode.Mouse0;
    public KeyCode rBumper = KeyCode.Mouse1;

    [Header("Other equivalents")]
    public KeyCode buttonStart = KeyCode.Mouse2;
    public KeyCode buttonBack = KeyCode.Mouse3;

    private float getHorizontalAxis() {
        return Mathf.Clamp(Input.GetAxis(horizontalAxis) * horizontalAxisSensitivity, -1.0f, 1.0f);
    }

    private float getVerticalAxis() {
        return Mathf.Clamp(Input.GetAxis(verticalAxis) * verticalAxisSensitivity, -1.0f, 1.0f);
    }

    //Getters
    public override controlsType GetControlsType() {
        return CONTROLSTYPE;
    }
    public override float GetDHorizontal() {
        return getHorizontalAxis();
    }
    public override float GetDVertical() {
        return getVerticalAxis();
    }
    public override float GetLHorizontal() {
        return getHorizontalAxis();
    }
    public override float GetLVertical() {
        return getVerticalAxis();
    }
    public override bool GetLClickDown() {
        return Input.GetKeyDown(click);
    }
    public override float GetRHorizontal() {
        return getHorizontalAxis();
    }
    public override float GetRVertical() {
        return getVerticalAxis();
    }
    public override bool GetRClickDown() {
        return Input.GetKeyDown(click);
    }
    public override bool GetButtonADown() {
        return Input.GetKeyDown(buttonA);
    }
    public override bool GetButtonBDown() {
        return Input.GetKeyDown(buttonB);
    }
    public override bool GetButtonXDown() {
        return Input.GetKeyDown(buttonX);
    }
    public override bool GetButtonYDown() {
        return Input.GetKeyDown(buttonY);
    }
    public override bool GetLBumperDown() {
        return Input.GetKeyDown(lBumper);
    }
    public override bool GetRBumperDown() {
        return Input.GetKeyDown(rBumper);
    }
    public override bool GetButtonStartDown() {
        return Input.GetKeyDown(buttonStart);
    }
    public override bool GetButtonBackDown() {
        return Input.GetKeyDown(buttonBack);
    }
}