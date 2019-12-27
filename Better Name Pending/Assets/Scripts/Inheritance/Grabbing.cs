﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grabbing : Hands {

    public GameObject itemInHand;
    public Interactable gun;
    public string testInput;
    public LayerMask interactable;

    public GameObject cam;

    Vector3 oldPosition;
    Vector3 velocity;
    Vector3 oldRotation;
    Vector3 angularVelocity;

    public float throwMulti;
    public float rotMulti;

    public bool hasGiven;

    bool buttonStillDown;

    private void FixedUpdate() {
        if (XRDevice.isPresent) {
            transform.localPosition = InputTracking.GetLocalPosition(nodeName);
            transform.localRotation = InputTracking.GetLocalRotation(nodeName);
        }
    }

    private void Update() {
        if (MouseInputAndVRAxisCheck(1, gripInput, "Useless_Input")) {
            if (buttonStillDown == false && !hasGiven) {
                GrabAndLetGo(transform);
                buttonStillDown = true;
            }
        } else {
            if (buttonStillDown == true) {
                GrabAndLetGo(null);
                hasGiven = false;
                buttonStillDown = false;
            }
        }
        //if (MouseInputAndVRAxisCheck(1, gripInput, "Useless_Input") && !hasGiven) {
        //    if(buttonStillDown == false) {
        //        GrabAndLetGo(transform);
        //        buttonStillDown = true;
        //    }
        //    if (!hasGiven && itemInHand) {
        //        velocity = (oldPosition - itemInHand.transform.position) / Time.deltaTime;
        //        angularVelocity = (oldRotation - itemInHand.transform.rotation.eulerAngles) / Time.deltaTime;
        //        oldPosition = itemInHand.transform.position;
        //        oldRotation = itemInHand.transform.rotation.eulerAngles;
        //    }
        //} else {
        //    if (buttonStillDown == true) {
        //        if (itemInHand) {                    
        //            itemInHand.GetComponent<Rigidbody>().velocity = velocity * throwMulti;
        //            itemInHand.GetComponent<Rigidbody>().angularVelocity = angularVelocity * rotMulti;
        //            GrabAndLetGo(null);

        //            buttonStillDown = false;
        //        }
        //    }
        //}

        if (MouseInputAndVRAxisCheck(0, triggerInput, "Useless_Input")) {
            HeldItemInteract();
        }
    }

    public void GrabAndLetGo(Transform makeParent) {
        if (!itemInHand) {
            GameObject closest = CheckClosest(Physics.OverlapSphere(origin.position, range));
            if (closest && closest.GetComponent<Interactable>()) {
                itemInHand = closest;
                itemInHand.GetComponent<Interactable>().AttachToHand(makeParent);
            }
        } else {
            //itemInHand.GetComponent<Interactable>().AttachToHand(null);
            itemInHand = null;
        }


        ////////if (!itemInHand) {
        ////////    itemInHand = CheckClosest(Physics.OverlapSphere(origin.position, range));
        ////////    if (itemInHand) {
        ////////        if (itemInHand.GetComponentInParent<Grabbing>()) {
        ////////            itemInHand.GetComponentInParent<Grabbing>().GrabAndLetGo(transform);
        ////////        } else {
        ////////            if (XRDevice.isPresent) {
        ////////                if (itemInHand.GetComponent<Interactable>()) {
        ////////                    switch (itemInHand.GetComponent<Interactable>().onGrab){
        ////////                        case Interactable.OnGrab.Follow:
        ////////                        break;
        ////////                        case Interactable.OnGrab.Pickup:
        ////////                        itemInHand.transform.SetParent(makeParent);
        ////////                        if (itemInHand.GetComponent<Interactable>().transferPositionAndRotation) {
        ////////                            itemInHand.transform.localPosition = itemInHand.GetComponent<Interactable>().setPosition;
        ////////                            itemInHand.transform.localRotation = Quaternion.Euler(itemInHand.GetComponent<Interactable>().setRotation);
        ////////                        }
        ////////                        break;
        ////////                    }
        ////////                }
        ////////            } else {
        ////////                itemInHand.transform.SetParent(cam.transform);
        ////////            }
        ////////        }
        ////////        if (itemInHand && itemInHand.GetComponent<Rigidbody>()) {
        ////////            itemInHand.GetComponent<Rigidbody>().isKinematic = true;
        ////////        }
        ////////        if (itemInHand && itemInHand.GetComponent<Interactable>()) {
        ////////            Interactable interactableInHand = itemInHand.GetComponent<Interactable>();
        ////////            if (interactableInHand.transferPositionAndRotation) {
        ////////                itemInHand.transform.localRotation = Quaternion.Euler(interactableInHand.setRotation);
        ////////                itemInHand.transform.localPosition = interactableInHand.setPosition;
        ////////            }
        ////////        }
        ////////    }
        ////////} else {
        ////////    if (itemInHand.GetComponent<Rigidbody>()) {
        ////////        itemInHand.GetComponent<Rigidbody>().isKinematic = false;
        ////////    }
        ////////    itemInHand.transform.SetParent(makeParent);
        ////////    itemInHand = null;
        ////////}
        //if (!itemInHand) {
        //    itemInHand = CheckClosest(Physics.OverlapSphere(origin.position, range));
        //    if (itemInHand) {
        //        if (itemInHand.GetComponentInParent<Grabbing>()) {
        //            itemInHand.GetComponentInParent<Grabbing>().GrabAndLetGo(transform);
        //        } else {
        //            if (XRDevice.isPresent) {
        //                if (itemInHand.GetComponent<Interactable>()) {
        //                    switch (itemInHand.GetComponent<Interactable>().onGrab){
        //                        case Interactable.OnGrab.Follow:
        //                        break;
        //                        case Interactable.OnGrab.Pickup:
        //                        itemInHand.transform.SetParent(makeParent);
        //                        if (itemInHand.GetComponent<Interactable>().transferPositionAndRotation) {
        //                            itemInHand.transform.localPosition = itemInHand.GetComponent<Interactable>().setPosition;
        //                            itemInHand.transform.localRotation = Quaternion.Euler(itemInHand.GetComponent<Interactable>().setRotation);
        //                        }
        //                        break;
        //                    }
        //                }
        //            } else {
        //                itemInHand.transform.SetParent(cam.transform);
        //            }
        //        }
        //        if (itemInHand && itemInHand.GetComponent<Rigidbody>()) {
        //            itemInHand.GetComponent<Rigidbody>().isKinematic = true;
        //        }
        //        if (itemInHand && itemInHand.GetComponent<Interactable>()) {
        //            Interactable interactableInHand = itemInHand.GetComponent<Interactable>();
        //            if (interactableInHand.transferPositionAndRotation) {
        //                itemInHand.transform.localRotation = Quaternion.Euler(interactableInHand.setRotation);
        //                itemInHand.transform.localPosition = interactableInHand.setPosition;
        //            }
        //        }
        //    }
        //} else {
        //    if (itemInHand.GetComponent<Rigidbody>()) {
        //        itemInHand.GetComponent<Rigidbody>().isKinematic = false;
        //    }
        //    itemInHand.transform.SetParent(makeParent);
        //    itemInHand = null;
        //}
    }

    GameObject CheckClosest(Collider[] colliders) {
        if (colliders.Length > 0) {
            for (int i = 0; i < colliders.Length; i++) {
                if(colliders[i].tag == "Interactable") {
                    return colliders[i].gameObject;
                }
            }
        }
        return null;
    }

    void HeldItemInteract() {
        if (itemInHand && itemInHand.GetComponent<Interactable>()) {
            itemInHand.GetComponent<Interactable>().Use();
        }
    }

    private void OnDrawGizmos() {
        if (origin) {
            Gizmos.DrawWireSphere(origin.position, range);
        }
    }
}
