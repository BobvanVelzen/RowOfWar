using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItextFeedback : MonoBehaviour
{
    public AnimationClip animation;
    private float timer;
    // Start is called before the first frame update
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
            Disable();
        }
        else timer += Time.deltaTime;
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
