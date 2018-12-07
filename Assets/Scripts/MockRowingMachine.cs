using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockRowingMachine : RowingMachine {

    //[Tooltip("The mock of the frequency")]
    //public float mFrequency = 60;
    private bool released = true;
    public string TriggerName = "JoystickRT";
    [Tooltip("Speed of the sinus")]
    public float sinusSpeed = 1f;
    [Tooltip("Decreasement of pullStrength over time")]
    public float decaySpeed = 1f;
    [Tooltip("Incresement of pullStrength when missed")]
    public float forcePerPull = 1f;
    [Tooltip("Decreasement of pullStrength when missed")]
    public float missPenalty = 1f;
    [Tooltip("Maximum sin value for timeframe Good")]
    public float maxGood = 0.5f;
    [Tooltip("Maximum sin value for timeframe Enough")]
    public float maxEnough = 0.8f;
	
	void FixedUpdate () {
        // Get current Sin value
        sinus = Mathf.Sin(Time.time * sinusSpeed);
        float absSinus = Mathf.Abs(sinus);

        // Checks if trigger is pressed or released
        float triggerValue = Input.GetAxis(TriggerName);
        bool triggered = false;
        if (released && triggerValue > 0.8f)
        {
            released = false;
            triggered = true;
        }
        else if (triggerValue < 0.2f)
        {
            released = true;
        }

        // Changes pullstrength based on how when the trigger was pressed
        if (triggered && absSinus < maxGood)
        {
            PullStrength += forcePerPull;
            Debug.Log("Good");
        }
        else if (triggered && absSinus < maxEnough)
        {
            PullStrength += forcePerPull / 2;
            Debug.Log("Enough");
        }
        else if (triggered)
        {
            PullStrength -= missPenalty;
            Debug.Log("Bad");
        }
        PullStrength = Mathf.Max(PullStrength - Time.deltaTime * decaySpeed, 0f);
    }
}
