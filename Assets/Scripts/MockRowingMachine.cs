using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockRowingMachine : RowingMachine {

    [Tooltip("The mock of the frequency")]
    public float mFrequency = 60;
    private bool released = true;
	
	void FixedUpdate () {
        sinus = Mathf.Sin(Time.time);
        float absSinus = Mathf.Abs(sinus);

        float triggerValue = Input.GetAxis("JoystickRT");
        if (released && triggerValue > 0.8f && absSinus < 0.5f)
        {
            Debug.Log(sinus);
            released = false;
        } else if (triggerValue < 0.2f && absSinus > 0.5f)
        {
            released = true;
        }
	}
}
