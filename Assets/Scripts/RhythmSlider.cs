using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmSlider : MonoBehaviour {

    [Tooltip("Connected rowing machine")]
    private RowingMachine playerRowingMachine;
    public RowingMachine PlayerRowingMachine
    {
        get { return playerRowingMachine; }
        set
        {
            playerRowingMachine = value;
            //float minFreq = value.idealFrequency - value.allowedOffset;
            //float maxFreq = value.idealFrequency + value.allowedOffset;
            //SetFrequencyBounds(minFreq, maxFreq);
        }
    }
    private bool setBounds = false;

    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
		slider.value = 1f;
    }

    private void Update()
    {
        if (playerRowingMachine != null)
            SetFrequency(playerRowingMachine.Sinus);
    }

    /// <summary>
    /// Sets the minimum and maximum frequency values of the slider
    /// </summary>
    /// <param name="min">Minimum frequency</param>
    /// <param name="max">Maximum frequency</param>
    public void SetFrequencyBounds(float min, float max)
    {
        slider.minValue = min;
        slider.maxValue = max;
        setBounds = true;
    }

    /// <summary>
    /// Sets the value of the slider to the current frequency of the player
    /// </summary>
    /// <param name="frequency">Current frequency</param>
	private void SetFrequency (float frequency) {
        if (!setBounds)
            return;

        slider.value = frequency;
	}
}
