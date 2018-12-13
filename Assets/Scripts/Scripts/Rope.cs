using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    public Transform player1;
    public Transform player2;
    public Transform rope;

    void FixedUpdate()
    {
        UpdateRope();
    }

    private void UpdateRope()
    {
        // Get start and end position of the rope
        Vector3 startPos = player1.position;
        Vector3 endPos = player2.position;

        // Change the center position of the rope
        Vector3 centerPos = (startPos + endPos) / 2f;
        transform.position = centerPos;

        // Change the length of the rope
        float newScale = Vector3.Distance(startPos, endPos);
        Vector3 scale = rope.localScale;
        rope.localScale = new Vector3(newScale, scale.y, scale.z);

        // Changle angle of the rope
        Vector3 direction = (startPos - endPos).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}