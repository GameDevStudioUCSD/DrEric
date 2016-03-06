using UnityEngine;
using System.Collections;

public class ChaseCamera : MonoBehaviour {
    public float speed = 0.5f;
    public float finalX = 100f;
    public bool active = false;

    private GameObject playerHolder;
    private SquidLauncher squid;
    private Camera camera;
    private GameObject boss;
    private GameObject walls;
    private float lastUpdate;
    
	void Start () {
        playerHolder = GameObject.Find(Names.PLAYERHOLDER);
        squid = playerHolder.GetComponentInChildren<SquidLauncher>();
        camera = this.transform.FindChild("Camera").gameObject.GetComponent<Camera>();
        boss = this.transform.FindChild("FishBoss").gameObject;
        walls = transform.FindChild("SolidBorders").gameObject;

        if (active) Activate();
        else
        {
            boss.SetActive(false);
            walls.SetActive(false);
        }
	}
	
	void Update ()
    {
        if (active && transform.localPosition.x < finalX)
            transform.Translate(speed * (Time.time - lastUpdate), 0, 0);
        lastUpdate = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            Activate();
    }

    public void Activate()
    {
        active = true;
        boss.SetActive(true);
        playerHolder.GetComponent<PlayerHolder>().enableDrEricCamera = false;
        squid.activeCamera.gameObject.SetActive(false);
        squid.activeCamera = camera;
        walls.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
