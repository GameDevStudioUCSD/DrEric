using UnityEngine;
using System.Collections;

public class PlayerHolder : MonoBehaviour {
    public bool enableDrEricCamera = true;
    private GameObject drEric;
    private GameObject squidLauncher;
    private GameObject gameCamera;
    private OrientWithGravity cameraOrienter;

	// Use this for initialization
	void Start () {
        squidLauncher = transform.Find(Names.SQUIDLAUNCHER).gameObject;
        gameCamera = transform.Find(Names.CAMERA).gameObject;
        drEric = squidLauncher.GetComponent<SquidLauncher>().getDrEric();
        cameraOrienter = gameCamera.GetComponent<OrientWithGravity>();
        if (!enableDrEricCamera)
        {
            gameCamera.GetComponent<Camera>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        cameraOrienter.CheckOrientation();

        if (drEric == null)
            drEric = squidLauncher.GetComponent<SquidLauncher>().getDrEric();
        if (drEric != null) {
            Vector3 squidPos = squidLauncher.transform.position;
            transform.position = drEric.transform.position;
            drEric.transform.localPosition = Vector3.zero;
            squidLauncher.transform.position = squidPos;
            ///drEric.transform.position = squidPos;
        }
	}
}
