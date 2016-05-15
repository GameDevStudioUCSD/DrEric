using UnityEngine;

public class ChaseCameraPast : MonoBehaviour {
    public float speed = 0.5f;
    public float finalX = 100f;
    public bool active = false;
    public float movingSpeed = .05f;

    public enum Stage {
        INITIAL,
        INITIAL_AFTER,
        TO_RIGHT,
        TO_UP,
        FINISH
    };

    public Stage stage = Stage.INITIAL;

    protected GameObject playerHolder;
    protected SquidLauncher squid;
    protected Camera camera;
    protected GameObject boss;
    protected float lastUpdate;
    protected Vector3 startPos;
    protected GameObject missileSpawner;

    protected void Start() {
        playerHolder = GameObject.Find(Names.PLAYERHOLDER);
        squid = playerHolder.GetComponentInChildren<SquidLauncher>();
        camera = this.transform.FindChild("Camera").gameObject.GetComponent<Camera>();
        boss = this.transform.FindChild("FishBoss").gameObject;
        missileSpawner = boss.transform.FindChild("MissileSpawner").gameObject;
        startPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        if (active) Activate();
        else {
            boss.SetActive(false);
        }
    }

    void Update() {
       // if (active && transform.localPosition.x < finalX)
         //   transform.Translate(speed * (Time.time - lastUpdate), 0, 0);
        lastUpdate = Time.time;

        switch (stage) {
            case Stage.INITIAL_AFTER:
                transform.Translate(70, -20, 0);
                stage = Stage.TO_RIGHT;
                break;
            case Stage.TO_RIGHT:
                transform.Translate(movingSpeed, 0,0);
                if (transform.position.x > 114) {
                    stage = Stage.TO_UP;
                }
                break;
            case Stage.TO_UP:
                transform.Translate(0,movingSpeed, 0);
                if (transform.position.y > -3) {
                    stage = Stage.FINISH;
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Activate();
            if (other.transform.position.x > 95) {
                this.stage = Stage.INITIAL_AFTER;
            }
        }
    }

    public void Activate() {
        active = true;
        boss.SetActive(true);
        missileSpawner.SetActive(true);
        playerHolder.GetComponent<PlayerHolder>().enableDrEricCamera = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }


    public void Reset() {
        transform.localPosition = new Vector3(startPos.x, startPos.y, startPos.z);
        Deactivate();
    }

    void Deactivate() {
        active = false;
        boss.SetActive(false);
        missileSpawner.SetActive(false);
        playerHolder.GetComponent<PlayerHolder>().enableDrEricCamera = true;
        squid.activeCamera.gameObject.SetActive(false);
        playerHolder.GetComponent<PlayerHolder>().gameCamera.SetActive(true);
        squid.activeCamera = playerHolder.GetComponent<PlayerHolder>().gameCamera.GetComponent<Camera>();
        GetComponent<BoxCollider2D>().enabled = true;
    }
}