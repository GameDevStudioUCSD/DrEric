using UnityEngine;

/**
 * Filename: PlayerHolder.cs
 * Author: Daniel Griffiths
 * Contributing Authors: None
 * Date Drafted: January 6, 2016
 * Description: This script is used by the player holder, which contains both
 *              DrEric and the SquidLauncher, as well as the standard camera.
 *              DrEric's motion is transferred to the holder by this script.
 */
public class PlayerHolder : MonoBehaviour {
    public bool enableDrEricCamera = true;

    private GameObject drEric;
    private GameObject squidLauncher;
    public GameObject gameCamera;
    private OrientWithGravity cameraOrienter;

	/**
     * Description: Initializes reference fields
     */
	void Start () {
        squidLauncher = transform.Find(Names.SQUIDLAUNCHER).gameObject;
        gameCamera = transform.Find(Names.CAMERA).gameObject;
        drEric = GameObject.Find(Names.DRERIC);
        cameraOrienter = gameCamera.GetComponent<OrientWithGravity>();
        if (!enableDrEricCamera)
        {
            gameCamera.GetComponent<Camera>().enabled = false;
        }
    }
	
	/**
     * Description: Orients camera, reads DrEric when dead, and handles
     *              movement. All forces are applied to DrEric, but the
     *              PlayerHolder should move while DrEric stays at local
     *              position (0, 0). Squid position is preserved.
     */
	void Update () {
        cameraOrienter.CheckOrientation();
        if (drEric == null) {
            drEric = GameObject.Find(Names.DRERIC);
            transform.parent = null;
        }
        if (drEric != null) {
            Vector3 squidPos = squidLauncher.transform.position;
            transform.position = drEric.transform.position;
            drEric.transform.localPosition = Vector3.zero;
            squidLauncher.transform.position = squidPos;
        }
	}
}
