using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

/**
 * Handles DrEric's death and respawning. Should be on the Respawner object.
 * There should only be one Respawner in the level.
 */
public class RespawnController : MonoBehaviour {
	private bool isDead;
	private double respawnTimer;
	public double respawnTime;
	public GameObject player;
    public UnityEvent spawnEvents;
    public UnityEvent deathEvents;
	private GameObject currentPlayer = null;
    private GameObject playerHolder;
    private GameObject squidLauncher;
    private List<UnityEvent> onSpawnList;
    private List<UnityEvent> onDeathList;
    protected RhythmController rhythmController;

	// Use this for initialization
	void Start () {
        rhythmController = RhythmController.GetController();
		respawnTimer = 0;
		Respawn(); //initial creation of DrEric
        playerHolder = GameObject.Find(Names.PLAYERHOLDER);
        squidLauncher = playerHolder.transform.Find(Names.SQUIDLAUNCHER).gameObject;
        if (playerHolder == null)
        {
            throw new System.Exception("No Player Holder!");
        }
        playerHolder.transform.position = transform.position;
        RegisterSpawnEvent(spawnEvents);
        RegisterDeathEvent(deathEvents);
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			respawnTimer += Time.deltaTime;
			if (respawnTimer >= respawnTime) {
				Respawn ();
			}
		}
	}
	/*
	 * Destroys DrEric and begins a countdown to respawning.
	 */
	public void kill() {
		if (currentPlayer != null) { //prevents attempting to delete null
			Destroy (currentPlayer.gameObject);
			isDead = true;
			respawnTimer = 0;

            //return camera to original position
            Vector3 squidPos = squidLauncher.transform.position;
            playerHolder.transform.position = transform.position;
            squidLauncher.transform.position = squidPos;

            rhythmController.SwitchToChannel(2);
            if (onDeathList != null)
            {
                foreach (UnityEvent e in onDeathList)
                    e.Invoke();
            }
			Debug.Log ("DrEric has died");
		}
	}

	/*
	 * Spawns a new DrEric for the player to control. Only one DrEric can exist at once.
	 */
	public void Respawn() {
		if (currentPlayer == null) { //DrEric must not already exist
			isDead = false;
			currentPlayer = (GameObject)Instantiate (player, transform.position, Quaternion.identity);
            rhythmController.SwitchToChannel(1);
			Debug.Log ("DrEric has spawned");
            GameObject playerHolder = GameObject.Find("Player Holder");
            if (playerHolder != null)
            {
                currentPlayer.transform.parent = playerHolder.transform;
                currentPlayer.transform.localPosition = Vector3.zero;
            }
            if(onSpawnList != null)
            foreach (UnityEvent e in onSpawnList)
            {
                e.Invoke();
            }
        }
    }
    public void RegisterDeathEvent( UnityEvent e )
    {
        if (onDeathList == null)
            onDeathList = new List<UnityEvent>();
        onDeathList.Add(e);
    }

    public void RegisterSpawnEvent( UnityEvent e )
    {
        if (onSpawnList == null)
            onSpawnList = new List<UnityEvent>();
        onSpawnList.Add(e);
    }
}
