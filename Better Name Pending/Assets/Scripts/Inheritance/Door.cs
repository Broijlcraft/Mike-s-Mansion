﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {
    
    public GameObject rigidHandle;
    public Rigidbody rigidbodyDoor;
    
    [Header("HideInInspector")]
    public bool locked;
}
