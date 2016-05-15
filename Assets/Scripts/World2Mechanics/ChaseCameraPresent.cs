using UnityEngine;

public class ChaseCameraPresent : MonoBehaviour {
    public float speed = 0.5f;
    public float finalX = 100f;
    public bool active = false;

    protected GameObject playerHolder;
    protected SquidLauncher squid;
    protected Camera camera;
    protected GameObject boss;
    protected float lastUpdate;
    protected Vector3 startPos;
    protected GameObject missileSpawner;

    public enum Stage {
        MIDDLE_DOWN,
        MIDDLE_RIGHT,
        MIDDLE_UP,
        MIDDLE_TO_LAST
    }

    public Stage stage = Stage.MIDDLE_DOWN;

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
        if (active) {
            switch (stage) {
                case Stage.MIDDLE_DOWN:
                    transform.Translate(0, -speed, 0);
                    if (this.transform.position.y < -22)
                        stage = Stage.MIDDLE_RIGHT;
                    break;
                case Stage.MIDDLE_RIGHT:
                    transform.Translate(speed, 0,0);
                    Debug.Log(this.transform.position.x);
                    if (this.transform.position.x > -72.5f)
                        stage = Stage.MIDDLE_UP;
                    break;
                case Stage.MIDDLE_UP:
                    transform.Translate(0, speed, 0);
                    if (this.transform.position.y > -5)
                        stage = Stage.MIDDLE_TO_LAST;
                    break;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            Activate();
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