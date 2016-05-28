using UnityEngine;
using System.Collections;

public class moveRespawnOnTeleport : MonoBehaviour {

    private RespawnController respawner;

    // Use this for initialization
    void Start()
    {
        respawner = GameObject.Find("Respawner/Spawner").GetComponent<RespawnController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

        }
    }

}
