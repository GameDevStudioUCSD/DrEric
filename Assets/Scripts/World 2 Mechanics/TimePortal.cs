using UnityEngine;
using System.Collections;

public class TimePortal : MonoBehaviour
{
    public GameObject destination;
    public PlatformSapling platform;

    public float timeBetweenTeleportation = 2f;

    public Material presentSkybox; //present
    public Material pastSkybox; //past

    private Transform drEricTransform;
    private Transform playerHolderTrans;
    private AudioSource audioSource;
    private Danmaku portalDanmaku;
    private Danmaku destinationDanmaku;
    private TrailRenderer drEricTrail;
    private bool onTree = false;
    private RhythmController rhythmController;
    void Start()
    {
        portalDanmaku = GetComponent<Danmaku>();
        destinationDanmaku = destination.GetComponent<Danmaku>();
        audioSource = GetComponent<AudioSource>();
        timeBetweenTeleportation = audioSource.clip.length / 2;
        rhythmController = RhythmController.GetController();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
        	onTree = false;
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
            if (platform != null && platform.playerOnTop == true)
            {
                onTree = true;
            }

        }
    }


    void Teleport()
    {
        drEricTrail.enabled = false;
        drEricTransform.parent = playerHolderTrans;
        Vector3 dest = destination.transform.position;
        if (onTree) {
        	dest = new Vector3(platform.presentTree.transform.position.x, dest.y + 10, dest.z);
        }
        playerHolderTrans.position = dest;
        ChangeSkybox();
        ChangeSong();
        Invoke("ReenableTrail", drEricTrail.time);
        Invoke("ShutoffDestAnimation", timeBetweenTeleportation);
        portalDanmaku.Deactive();
    }

    void ChangeSong()
    {
        if (RenderSettings.skybox == presentSkybox)
            rhythmController.SwapToSong(0);
        else
            rhythmController.SwapToSong(1);
    }

    void ChangeSkybox()
    {
        if (presentSkybox != null && pastSkybox != null)
        {
            if (RenderSettings.skybox == presentSkybox)
                RenderSettings.skybox = pastSkybox;
            else
                RenderSettings.skybox = presentSkybox;
        }
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
