﻿using UnityEngine;
using System.Collections;

public class TimePortal : MonoBehaviour
{
    public GameObject destination;
    public BikiniSapling platform;

    public float timeBetweenTeleportation = 2f;

    private Transform drEricTransform;
    private Transform playerHolderTrans;
    private AudioSource audioSource;
    private Danmaku portalDanmaku;
    private Danmaku destinationDanmaku;
    private TrailRenderer drEricTrail;
    void Start()
    {
        portalDanmaku = GetComponent<Danmaku>();
        destinationDanmaku = destination.GetComponent<Danmaku>();
        audioSource = GetComponent<AudioSource>();
        timeBetweenTeleportation = audioSource.clip.length / 2;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            audioSource.Play();
            // Invoke teleport later
            Invoke("Teleport", timeBetweenTeleportation);
            // Cue the visuals
            portalDanmaku.enabled = true;
            destinationDanmaku.enabled = true;
            // Move Dr Eric into the portal
            drEricTransform = other.transform;
            playerHolderTrans = drEricTransform.parent;
            drEricTransform.parent = transform;
            drEricTransform.position = Vector2.zero;
            drEricTrail = other.GetComponent<TrailRenderer>();
            // Stop Dr Eric from moving
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            if (platform.playerOnTop == true)
            {
                float yPos = platform.presentTree.transform.position.y;
            }

        }
    }


    void Teleport()
    {
        drEricTrail.enabled = false;
        drEricTransform.parent = playerHolderTrans;
        playerHolderTrans.position = destination.transform.position;
        Invoke("ReenableTrail", drEricTrail.time);
        Invoke("ShutoffDestAnimation", timeBetweenTeleportation);
        portalDanmaku.Deactive();
    }

    void ReenableTrail()
    {
        drEricTrail.enabled = true;
    }
    void ShutoffDestAnimation()
    {
        destinationDanmaku.Deactive();
    }
}
