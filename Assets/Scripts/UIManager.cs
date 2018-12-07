using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public RhythmSlider[] rhythmSliders;
    public Slider ropeSlider;
    public TextMeshProUGUI scoreboard;
    
    /// <summary>
    /// Create a new slider for each player
    /// </summary>
    /// <param name="players">Current players</param>
	public void CreateSliders(Player[] players)
    {
        if (rhythmSliders.Length == 0)
            rhythmSliders = new RhythmSlider[players.Length];
        for (int i = 0; i < rhythmSliders.Length; i++)
        {
            // TODO: Instantiate rhythmbar for player instead of checking if not null
            if (rhythmSliders[i] != null)
            {
                rhythmSliders[i].PlayerRowingMachine = players[i].RowingMachine;
            }
        }

        // TODO: Get these values from somewhere else!
        ropeSlider.minValue = -100;
        ropeSlider.maxValue = 100;
    }

    /// <summary>
    /// Updates the current position of the rope
    /// </summary>
    /// <param name="value">New rope position</param>
    public void SetRopeSliderValue(float value)
    {
        //TODO: Get value from somewhere else
        ropeSlider.value = value;
    }

    /// <summary>
    /// Sets new scoreboard values from parameters
    /// </summary>
    /// <param name="leftPoints">Points for team 1</param>
    /// <param name="rightPoints">Points for team 2</param>
    public void SetScore(int leftPoints, int rightPoints)
    {
        scoreboard.text = string.Format("{0} - {1}", leftPoints, rightPoints);
    }
}
