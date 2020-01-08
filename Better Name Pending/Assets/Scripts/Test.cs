﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public Transform followThis;
    public Transform makeThisFollow;
    public bool shouldFollow;
    public float maxDistance;
    public Vector2 maxMinPlus;

    private void Update() {
        float f = Vector3.Distance(followThis.position, transform.position);
        Vector3 v = makeThisFollow.transform.position;
        v.z = Mathf.Clamp(followThis.localPosition.z, maxMinPlus.x, maxMinPlus.y);
        makeThisFollow.transform.position = v;          
    }
}
