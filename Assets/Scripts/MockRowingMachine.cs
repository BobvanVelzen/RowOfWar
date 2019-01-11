using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockRowingMachine : RowingMachine {

    //[Tooltip("The mock of the frequency")]
    //public float mFrequency = 60;
    public TMPro.TextMeshProUGUI uiTextElement;
    private bool released = true;
    private bool triggerAllowed = true;
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

        if (absSinus > 0.99f && triggerAllowed == false)
            triggerAllowed = true;

        // Checks if trigger is pressed or released
        float triggerValue = Input.GetAxis(TriggerName);
        bool triggered = false;
        if (triggerAllowed && released && triggerValue > 0.8f)
        {
            released = false;
            triggered = true;
            triggerAllowed = false;
        }
        else if (triggerValue < 0.2f)
        {
            released = true;
        }

        // Changes pullstrength based on how when the trigger was pressed
        if (triggered && absSinus < maxGood)
        {
            PullStrength += forcePerPull * (1f - absSinus);
            Debug.Log("Good" + forcePerPull * (1f - absSinus));
            uiTextElement.gameObject.SetActive(true);
            uiTextElement.text = "Good";
            uiTextElement.color = Color.green;
        }
        else if (triggered && absSinus < maxEnough)
        {
            PullStrength += forcePerPull * (1f - absSinus) * 0.5f;
            Debug.Log("Enough" + forcePerPull * (1f - absSinus) * 0.5f);
            uiTextElement.gameObject.SetActive(true);
            uiTextElement.text = "Good";
            uiTextElement.color = Color.green;
        }
        else if (triggered)
        {
            PullStrength -= missPenalty;
            Debug.Log("Bad");
            uiTextElement.gameObject.SetActive(true);
            uiTextElement.text = "Good";
            uiTextElement.color = Color.green;
        }
        PullStrength = Mathf.Max(PullStrength - Time.deltaTime * decaySpeed, 0f);
    }
}
