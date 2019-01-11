using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XBoxControls", menuName = "Controls/XBoxControls")]
public class XBoxControls : Controls {

    //Controls type
    private const controlsType CONTROLSTYPE = controlsType.XBOX;

    //D-Pad
    private string dHorizontal = "dHorizontal";
    private string dVertical = "dVertical";

    //Left joystick
    private string lHorizontal = "lHorizontal";
    private string lVertical = "lVertical";
    private string lClick = "lClick";

    //Right Joystick
    private string rHorizontal = "rHorizontal";
    private string rVertical = "rVertical";
    private string rClick = "rClick";

    //Buttons
    private string buttonA = "buttonA";
    private string buttonB = "buttonB";
    private string buttonX = "buttonX";
    private string buttonY = "buttonY";

    //Bumpers
    private string lBumper = "lBumper";
    private string rBumper = "1rBumper";

    //Others
    private string buttonStart = "buttonStart";
    private string buttonBack = "buttonBack";
    
    //Getters
    public override controlsType GetControlsType() {
        return CONTROLSTYPE;
    }
    public override float GetDHorizontal() {
        return Input.GetAxis(base.controlsName + dHorizontal);
    }
    public override float GetDVertical() {
        return Input.GetAxis(base.controlsName + dVertical);
    }
    public override float GetLHorizontal() {
        return Input.GetAxis(base.controlsName + lHorizontal);
    }
    public override float GetLVertical() {
        return Input.GetAxis(base.controlsName + lVertical);
    }
    public override bool GetLClickDown() {
        return Input.GetButtonDown(base.controlsName + lClick);
    }
    public override float GetRHorizontal() {
        return Input.GetAxis(base.controlsName + rHorizontal);
    }
    public override float GetRVertical() {
        return Input.GetAxis(base.controlsName + rVertical);
    }
    public override bool GetRClickDown() {
        return Input.GetButtonDown(base.controlsName + rClick);
    }
    public override bool GetButtonADown() {
        return Input.GetButtonDown(base.controlsName + buttonA);
    }
    public override bool GetButtonBDown() {
        return Input.GetButtonDown(base.controlsName + buttonB);
    }
    public override bool GetButtonXDown() {
        return Input.GetButtonDown(base.controlsName + buttonX);
    }
    public override bool GetButtonYDown() {
        return Input.GetButtonDown(base.controlsName + buttonY);
    }
    public override bool GetLBumperDown() {
        return Input.GetButtonDown(base.controlsName + lBumper);
    }
    public override bool GetRBumperDown() {
        return Input.GetButtonDown(base.controlsName + rBumper);
    }
    public override bool GetButtonStartDown() {
        return Input.GetButtonDown(base.controlsName + buttonStart);
    }
    public override bool GetButtonBackDown() {
        return Input.GetButtonDown(buttonBack);
    }
}