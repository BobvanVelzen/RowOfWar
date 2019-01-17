using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiTextFeedback1 : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    public AnimationClip animation;
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= animation.length)
        {
            timer = 0f;
            this.gameObject.SetActive(false);
        }
        else timer += Time.deltaTime;
    }
}
