using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_PlayerCheck : MonoBehaviour
{

	public GameObject panel;

	public Texture noPlayersAddedImage;
	public Texture onePlayersAddedImage;
	public Texture twoPlayersAddedImage;

	public int ammountOfPlayers = 0;
	public bool ammoutOfPlayersChanged = false;

	private bool startTimer;
	private float timeLeft = 20;

	public TMPro.TextMeshProUGUI uiTextElement;

	public string TriggerNameFirstAdded = "Mouse Y"; //JoystickLT //JoystickRT
	public bool P1Added = false;
	public string TriggerNameSecondAdded = "Mouse X";
	public bool P2Added = false;


    // Start is called before the first frame update
    void Start()
    {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetString ("Bot", "On");
		//tv hoeger dan 0.8f
        
    }

    // Update is called once per frame
    void Update()
    {
		float triggerValueP1 = Input.GetAxis(TriggerNameFirstAdded);
		float triggerValueP2 = Input.GetAxis(TriggerNameSecondAdded);

		//No one is player 1 yet
		if (!P1Added && !P2Added) {
			if (triggerValueP1 > 0.8f) {
				ammountOfPlayers = ammountOfPlayers + 1;
				P1Added = true;
				ammoutOfPlayersChanged = true;
				PlayerPrefs.SetString ("P1Trigger", TriggerNameFirstAdded);
			} else if (triggerValueP2 > 0.8f) {
				ammoutOfPlayersChanged = true;
				ammountOfPlayers = ammountOfPlayers + 1;
				P2Added = true;
				PlayerPrefs.SetString ("P1Trigger", TriggerNameSecondAdded);
			}

		} else {
			if (triggerValueP1 > 0.8f) {
				if (P1Added == false) {
					ammountOfPlayers = ammountOfPlayers + 1;
					P1Added = true;
					ammoutOfPlayersChanged = true;
					PlayerPrefs.SetString ("P2Trigger", TriggerNameFirstAdded);
				}
			}
			if (triggerValueP2 > 0.8f) {
				if (P2Added == false) {
					ammoutOfPlayersChanged = true;
					ammountOfPlayers = ammountOfPlayers + 1;
					P2Added = true;
					PlayerPrefs.SetString ("P2Trigger", TriggerNameSecondAdded);
				}
			}

		}




		if (ammoutOfPlayersChanged) 
		{
			if (ammountOfPlayers == 0) {
				panel.GetComponent<RawImage> ().texture = noPlayersAddedImage;
				ammoutOfPlayersChanged = false;
			} else if (ammountOfPlayers == 1) {
				startTimer = true;
				panel.GetComponent<RawImage> ().texture = onePlayersAddedImage;
				ammoutOfPlayersChanged = false;
			} else if (ammountOfPlayers == 2) {
				startTimer = true;
				panel.GetComponent<RawImage> ().texture = twoPlayersAddedImage;
				ammoutOfPlayersChanged = false;
			}

		}

		if (startTimer) 
		{
			timeLeft -= Time.deltaTime;
			uiTextElement.text = "Game starts in \n" + Mathf.RoundToInt(timeLeft).ToString ();

			if (timeLeft < 0) {
				SceneManager.LoadScene("Level 2");
			}
		}
        
    }
}
