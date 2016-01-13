using UnityEngine;
using System.Collections;

public class ItemRespawner : MonoBehaviour {

	// Use this for initialization
    public GameObject item;
    private GameObject newObj;
    private Vector3 loc;
	void Start () {
        loc = item.transform.position;
        CreateClone();
	}
	
	// Update is called once per frame
	void Update () {
        if (item == null)
            ActivateClone();
	}
    void CreateClone()
    {
        newObj = GameObject.Instantiate(item);
        newObj.SetActive(false);
    }
    void ActivateClone()
    {
        newObj.transform.position = loc;
        newObj.SetActive(true);
        item = newObj;
        CreateClone();
    }
}
