using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TimePortal : MonoBehaviour
{
    public GameObject destination;
    public float timeBetweenTeleportation = 2f;

    public Material presentSkybox; //present
    public Material pastSkybox; //past
    public UnityEvent onTeleport;

    private Transform drEricTransform;
    private Transform playerHolderTrans;
    private SquidLauncher squid;
    private AudioSource audioSource;
    private Danmaku portalDanmaku;
    private Danmaku destinationDanmaku;
    private TrailRenderer drEricTrail;
    private bool onTree = false;
    private RhythmController rhythmController;
    private Rigidbody2D drEricRB;
    private GameObject timeIndicator;

    void Start()
    {
        portalDanmaku = GetComponent<Danmaku>();
        destinationDanmaku = destination.GetComponent<Danmaku>();
        audioSource = GetComponent<AudioSource>();
        timeBetweenTeleportation = audioSource.clip.length / 2;
        rhythmController = RhythmController.GetController();
        squid = GameObject.Find("Player Holder/Squid Launcher").GetComponent<SquidLauncher>();
        timeIndicator = GameObject.Find("TimeIndicator");
    }
       
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 9;
            GetComponent<Collider2D>().enabled = false;
        	onTree = false;
            audioSource.Play();
            // Invoke teleport later
            Invoke("Teleport", timeBetweenTeleportation * Time.timeScale);
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
            drEricRB = other.gameObject.GetComponent<Rigidbody2D>();
            drEricRB.velocity = Vector2.zero;
            drEricRB.angularVelocity = 0;
            drEricRB.gravityScale = 0;

            if (timeIndicator != null)
                timeIndicator.GetComponent<TimeIndicator>().ToggleTime();

        }
    }


    void Teleport()
    {
        Time.timeScale = 1;
        drEricTrail.enabled = false;
        drEricTransform.parent = playerHolderTrans;
        drEricRB.gravityScale = 1;
        Vector3 dest = destination.transform.position;
        playerHolderTrans.position = dest;
        ChangeSkybox();
        ChangeSong();
        onTeleport.Invoke();
        Invoke("ReenableTrail", drEricTrail.time);
        Invoke("ShutoffDestAnimation", timeBetweenTeleportation);
        portalDanmaku.Deactive();
        GetComponent<Collider2D>().enabled = true;
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
        squid.pastSprites = !squid.pastSprites;
    }

    void ReenableTrail()
    {
        drEricTrail.enabled = true;
    }
    void ResetTime()
    {
    }
    void ShutoffDestAnimation()
    {
        destinationDanmaku.Deactive();
    }
}
