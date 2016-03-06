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
    private float lastUpdate;
    
	void Start () {
        playerHolder = GameObject.Find(Names.PLAYERHOLDER);
        squid = playerHolder.GetComponentInChildren<SquidLauncher>();
        camera = this.transform.FindChild("Camera").gameObject.GetComponent<Camera>();
        boss = this.transform.FindChild("FishBoss").gameObject;
	}
	
	void Update ()
    {
        if (active)
        {
            boss.SetActive(true);
            if (transform.position.x < finalX)
                transform.Translate(speed * (Time.time - lastUpdate), 0, 0);
            squid.activeCamera = camera;
        }
        else
        {
            boss.SetActive(false);
        }
        lastUpdate = Time.time;
    }

    public void Activate()
    {
        playerHolder.GetComponent<PlayerHolder>().enableDrEricCamera = false;
        active = true;
    }
}
