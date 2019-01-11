using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controls: ScriptableObject {

    //Player id
    public string controlsName;

    //Controls type
    public enum controlsType {
        XBOX,
        KEYBOARD,
        MOUSE
    }

    //Getters
    public abstract controlsType GetControlsType();
    public abstract float GetDHorizontal();
    public abstract float GetDVertical();
    public abstract float GetLHorizontal();
    public abstract float GetLVertical();
    public abstract bool GetLClickDown();
    public abstract float GetRHorizontal();
    public abstract float GetRVertical();
    public abstract bool GetRClickDown();
    public abstract bool GetButtonADown();
    public abstract bool GetButtonBDown();
    public abstract bool GetButtonXDown();
    public abstract bool GetButtonYDown();
    public abstract bool GetLBumperDown();
    public abstract bool GetRBumperDown();
    public abstract bool GetButtonStartDown();
    public abstract bool GetButtonBackDown();
}
