using UnityEngine;
using System.Collections;

public class PlayerHolder : MonoBehaviour {
    private GameObject drEric;
    private GameObject squidLauncher;

	// Use this for initialization
	void Start () {
        squidLauncher = transform.Find(Names.SQUIDLAUNCHER).gameObject;
        drEric = squidLauncher.GetComponent<SquidLauncher>().getDrEric();
    }
	
	// Update is called once per frame
	void Update () {
        //
        if (drEric == null)
            drEric = squidLauncher.GetComponent<SquidLauncher>().getDrEric();
        if (drEric != null) {
            transform.position = drEric.transform.position;
            drEric.transform.localPosition = Vector3.zero;
        }
	}
}
