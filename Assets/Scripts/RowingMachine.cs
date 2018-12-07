using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RowingMachine : MonoBehaviour {

    [Tooltip("the rowing machine's ID")]
    public string id;
    [Tooltip("The maximum allowed deviation from the frequency")]
    public float allowedOffset = 10;
    [Tooltip("The ideal frequency of the user")]
    public float idealFrequency = 60;
    protected float frequency;
    public float Frequency
    {
        get { return frequency; }
    }
    protected float sinus;
    public float Sinus
    {
        get { return sinus; }
    }
    public float Offset
    {
        get { return frequency - idealFrequency; }
    }
}
