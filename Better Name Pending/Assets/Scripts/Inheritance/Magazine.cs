﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : Interactable {
    public int bullets;
    public GameObject magazine;
    Vector3 magStartPos;
    Quaternion magStartRot;
    bool inRange;
    Gun gun;

    private void Start() {
        StartSetUp();
    }

    private void Update() {
        if (beingHeld) {
            Collider[] colliders = Physics.OverlapSphere(origin.position, range);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].CompareTag("MagazineCollider") && !colliders[i].GetComponentInParent<Gun>().beingHeld) {
                    gun = colliders[i].GetComponentInParent<Gun>();
                    magazine.transform.SetPositionAndRotation(gun.magazineOrigin.position, gun.magazineOrigin.rotation);
                    magazine.transform.position = gun.magazineOrigin.position;
                    magazine.transform.rotation = gun.magazineOrigin.rotation;
                    inRange = true;
                    break;
                }
                gun = null;
                inRange = false;
                magazine.transform.localPosition = magStartPos;
                magazine.transform.localRotation = magStartRot;
            }
        } else {
            if (inRange) {
                gun.InsertMagazine(transform);
            }
        }
    }

    public void SetLocalPositionAndRotation(Transform transform, Vector3 localPosition, Quaternion localRotation) {
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
    }

    public override void StartSetUp() {
        base.StartSetUp();
        magStartPos = magazine.transform.localPosition;
        magStartRot = magazine.transform.localRotation;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(origin.position, range);
    }
}
