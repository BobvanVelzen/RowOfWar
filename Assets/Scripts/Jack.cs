using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jack : MonoBehaviour
{

    public Image circleFeedback;
    public MockRowingMachine rowScript;
    // Start is called before the first frame update
    


    // Update is called once per frame
    void Update()
    {
        circleFeedback.fillAmount = rowScript.triggerValue;
    }
}
