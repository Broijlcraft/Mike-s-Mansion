﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Gun : Interactable {

    public Transform magazineOrigin;
    [HideInInspector] public Magazine magazine;
    public Transform bulletCasingOrigin;
    public GameObject slideModel;
    public FollowObject slideFollow;
    public GunSlide slideToFollow;
    public float ejectForce;
    public GameObject emptyCasingPrefab;
    public GameObject bulletPrefab;
    public float shotsPerSecond;
    public int bulletInChamber;
    bool hasShot;
    float coolDown;
    public bool showRay;
    public float hitForce;
    [Header("Animations")]
    public string shotName;
    public string ammoToChamber;
    public string triggerPress;
    Animator animator;
    [Header("SFX")]
    public AudioClip shot;
    public AudioClip empty;

    [Header("Test")]
    public bool devTest;
    public Vector3 maxSlideBack;
    public bool shouldSlideForward;

    private void Start() {
        StartSetUp();
        if (slideModel && slideModel.GetComponent<FollowObject>()) {
            slideFollow = slideModel.GetComponent<FollowObject>();
        }
    }

    private void Update() {
        if (hasShot) {
            coolDown += Time.deltaTime;
            if (coolDown > 1 / shotsPerSecond) {
                hasShot = false;
                coolDown = 0;
            }
        }

        if (Input.GetButtonDown("Reload") && beingHeld) {
            ChamberLoader();
            //AnimatorCheckAndExecute(true);
            if(magazine && magazine.bullets > 0) {
                EjectShell(bulletPrefab);
            }
        }
    }

    public override void Use(bool down) {
        if (down) {
            if (!hasBeenDown == shot && hasShot == false) {
                if (bulletInChamber == 1) {
                    if (shot) {
                        AudioManager.PlaySound(shot, audioGroup);
                    }
                    RaycastHit hit;
                    if (Physics.Raycast(origin.position, origin.forward, out hit, range)) {
                        if (hit.transform.GetComponent<Rigidbody>()) {
                            hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(origin.transform.forward * hitForce, hit.point);
                        }
                    }
                    EjectShell(emptyCasingPrefab);
                    ChamberLoader();
                    //AnimatorCheckAndExecute(true);
                } else {
                    if (empty) {
                        AudioManager.PlaySound(empty, audioGroup);
                    }
                }
                hasShot = true;
            }
            hasBeenDown = true;
        } else {
            hasBeenDown = false;
        }
    }

    void EjectShell(GameObject shell) {
        if (shell && bulletCasingOrigin) {
            GameObject g = Instantiate(shell, bulletCasingOrigin.position, bulletCasingOrigin.rotation);
            g.GetComponent<Rigidbody>().AddForce(bulletCasingOrigin.forward * ejectForce);
        }
    }

    public void AnimatorCheckAndExecute(bool shoot) {
        if (animator) {
            if (slideFollow) {
                slideFollow.enabled = false;
            }
            if (bulletInChamber == 0) {
                animator.SetBool(ammoToChamber, false);
            } else {
                animator.SetBool(ammoToChamber, true);
            }
            if (shoot) {
                animator.SetTrigger(shotName);
                //animator.SetTrigger(triggerPress);
            }
        } else {
            print("No Animator");
        }
    }

    public void ChamberLoader() {
        bulletInChamber = 0;
        if (Manager.dev) {
            bulletInChamber = 1;
        } else {
            if (magazine && magazine.bullets > 0) {
                bulletInChamber = 1;
                //using -- caused issues
                magazine.bullets -= 1;
            }
        }
    }

    public override GameObject SpecialInteraction(Transform setParent) {
        if (setParent) {
            slideToFollow.transform.position = slideModel.transform.position;
            slideFollow.enabled = true;
            slideToFollow.AttachToHand(setParent, true);
            return slideToFollow.gameObject;
        } else {
            slideToFollow.AttachToHand(setParent, false);
            return null;
        }
    }

    public override void StartSetUp() {
        base.StartSetUp();
        animator = GetComponent<Animator>();
        if (GetComponentInChildren<Magazine>()) {
            InsertMagazine(GetComponentInChildren<Magazine>().transform);
        }
        if (!devTest) {
            ChamberLoader();
        }
        //AnimatorCheckAndExecute(false);
    }

    public void InsertMagazine(Transform mag) {
        mag.SetParent(magazineOrigin);
        mag.GetComponent<Rigidbody>().isKinematic = true;
        mag.GetComponent<BoxCollider>().enabled = false;
        magazine = mag.GetComponent<Magazine>();
        mag.transform.localPosition = Vector3.zero;
        mag.transform.localRotation = Quaternion.Euler(Vector3.zero);
        magazine.magazine.transform.localPosition = Vector3.zero;
        magazine.magazine.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnDrawGizmos() {
        if (showRay) {
            if (origin) {
                Debug.DrawRay(origin.position, origin.transform.forward, Color.red * 1000);
            } else {
                print("No Origin Set");
            }
            if (bulletCasingOrigin) {
                Debug.DrawRay(bulletCasingOrigin.position, bulletCasingOrigin.forward, Color.blue * 1000);
            } else {
                print("No Bulletcasing Origin Set");
            }
        }
    }
}
