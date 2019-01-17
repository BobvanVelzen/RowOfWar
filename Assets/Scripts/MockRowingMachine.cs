using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockRowingMachine : RowingMachine {

    //[Tooltip("The mock of the frequency")]
    //public float mFrequency = 60;
	public string player = "1";
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
    public float maxBad = 0.5f;
    [Tooltip("Maximum sin value for timeframe Bad")]
    public float maxEnough = 0.8f;

	public bool botIsEnabled = false;
	public float randomNumber = 0f;
	private bool numberAvalaible = false;
    private float trigRawTmp = 0;
    private float trigRaw = 0f;
	private bool isBotChecked = false;

	private float timer = 0f;

// TODO: Jack did this. we hate him now.
	public float triggerValue;

	void FixedUpdate () {
		timer += Time.deltaTime;

		//get the raw data of the trigger with full range 0...1
		trigRaw = Input.GetAxisRaw (TriggerName);

		// Get current Sin value
		sinus = Mathf.Sin (Time.time * sinusSpeed);
		float absSinus = Mathf.Abs (sinus);

		if (absSinus > 0.99f && triggerAllowed == false)
			triggerAllowed = true;

		// Checks if player comes from other scene (prop: Main Menu Kyle) then sets the trigger if selected
		//float triggerValue;
		if (PlayerPrefs.HasKey ("P" + player + "Trigger")) {
			triggerValue = Input.GetAxis (PlayerPrefs.GetString ("P" + player + "Trigger"));
			trigRaw = Input.GetAxisRaw (PlayerPrefs.GetString ("P" + player + "Trigger"));
		} else {
			if (!isBotChecked) {
				if (PlayerPrefs.HasKey ("Bot")) {
					botIsEnabled = true;
				}
				isBotChecked = true;
			}
			triggerValue = Input.GetAxis (TriggerName);
			//trigRaw = Input.GetAxisRaw (TriggerName);
		}

		// Checks if trigger is pressed or released

		bool triggered = false;



		////////////////////////////////////////////////////////////////////////////////////
		// Because the NPC doesnt need to do fancy stuff it will use the old code. Players
		// need to use the updated version.
		if (botIsEnabled) {

			/* OLD VERSION */
			// When 	the Slider is close to the middle it will calculate a random number.
			// this number is used to determine on what color the bot will "click"
			if (absSinus < 0.10f) {
				if (!numberAvalaible) {
					randomNumber = Random.Range (1, 10);
					numberAvalaible = true;
				}
			}

			// When the random number is highter then 5 the bot will "click" when on 
			// Orange, otherwise it will check on the green part of the slider.
			if (randomNumber > 5 && absSinus > 0.50f && absSinus < 0.90f) {
				numberAvalaible = false;
				randomNumber = 0;
				triggerValue = 1f;
			} else if(randomNumber <= 5 && randomNumber > 0 && absSinus > 0.90f){
				numberAvalaible = false;
				randomNumber = 0;
				triggerValue = 1f;
			}
		} else {

			/* NEW VERSION */
			// Since there are two events(start pulling and completely pulled)
			// which is represent by lightly pressing the trigger and
			// having it fully pressed, first we need to check for those events
			if ((trigRaw >= 1f || (trigRaw <= 0.05f && trigRaw > 0f)) && triggerAllowed && released && (trigRaw != trigRawTmp) && (timer > 1))
			{
				//Debug.Log(TriggerName + " EVENT  " + trigRaw);
				released = false;
				triggered = true;
				triggerAllowed = false;
				trigRawTmp = trigRaw;
				timer = 0f;

				if (botIsEnabled) {
					trigRaw = 0f;
					released = true;
				}

			}
			else if (trigRaw == 0f || (trigRaw < 1f && trigRaw > 0.05f))
			{
				released = true;
			}
			 
		}

		if (botIsEnabled) {
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

		} 
        

        // Changes pullstrength based on how when the trigger was pressed
        if (triggered && absSinus < maxBad)
        {
            PullStrength += missPenalty;
            //Debug.Log("Bad");
            uiTextElement.gameObject.SetActive(true);
            uiTextElement.text = "Bad";
            uiTextElement.color = Color.red;
        }
        else if (triggered && absSinus < maxEnough)
        {
            PullStrength -= forcePerPull * (absSinus) * 0.5f;
            //Debug.Log("Enough" + forcePerPull * (absSinus) * 0.5f);
            uiTextElement.gameObject.SetActive(true);
            uiTextElement.text = "Enough";
            uiTextElement.color = Color.yellow;
        }
        else if (triggered)
        {
            PullStrength -= forcePerPull * (absSinus);
            //Debug.Log("Good" + forcePerPull * (absSinus));
            uiTextElement.gameObject.SetActive(true);
            uiTextElement.text = "Good";
            uiTextElement.color = Color.green;
        }
        if (PullStrength > 0f)
            PullStrength = Mathf.Max(PullStrength - Time.deltaTime * decaySpeed, 0f);
        else if (PullStrength < 0f)
            PullStrength = Mathf.Min(PullStrength + Time.deltaTime * decaySpeed, 0f);
    }
}
