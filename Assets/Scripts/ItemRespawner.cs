using UnityEngine;
using System.Collections;

public class ItemRespawner : MonoBehaviour {
    public enum Type  {ONDEATH, OTHER }
	// Use this for initialization
    public GameObject item;
    public Type type = Type.ONDEATH;
    private GameObject newObj;
    private Vector3 loc;
	void Start () {
        loc = item.transform.position;
        CreateClone();
        switch (type)
        {
            case Type.ONDEATH:
                RegisterWithRespawner();
                break;
        }
	}

    private void RegisterWithRespawner()
    {
        GameObject respawner = GameObject.Find("Respawner/Spawner");
        RespawnController rs = respawner.GetComponent<RespawnController>();
        UnityEngine.Events.UnityEvent e = new UnityEngine.Events.UnityEvent();
        e.AddListener(Respawn);
        rs.RegisterSpawnEvent(e);
    }
    void Respawn()
    {
        if (item == null)
            ActivateClone();
    }
	
	// Update is called once per frame
	void Update () {
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
