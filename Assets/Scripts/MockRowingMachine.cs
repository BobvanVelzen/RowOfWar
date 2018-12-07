using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockRowingMachine : RowingMachine {

    [Tooltip("The mock of the frequency")]
    public float mFrequency = 60;
	
	void FixedUpdate () {
        frequency = mFrequency;
	}
}
