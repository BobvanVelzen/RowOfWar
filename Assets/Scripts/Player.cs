using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(RowingMachine))]
public class Player : MonoBehaviour {

    private Vector3 startPos;
    private CharacterAnimator characterAnimator;
    private RowingMachine rowingMachine;
    public RowingMachine RowingMachine
    {
        get { return rowingMachine; }
    }
    public Team Team;
    public float speedModifier = 1f;
    
	void Awake () {
        characterAnimator = GetComponent<CharacterAnimator>();
        rowingMachine = GetComponent<RowingMachine>();
        startPos = transform.position;
	}

    public void Move(float movement)
    {
        transform.position += new Vector3(0f, 0f, movement * speedModifier);
        characterAnimator.Move(movement, Team.IsLeft);
    }

    public void ResetPosition()
    {
        transform.position = startPos;
        rowingMachine.PullStrength = 0f;
    }
}
