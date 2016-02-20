using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/** Filename: RemoteSpawner.cs
 * Author: Daniel Griffiths
 * Contributing authors: N/A
 * Date Drafted: 2/20/2016
 * Description: Spawns an item
 */
public class RemoteSpawner : MonoBehaviour {

    public GameObject spawnThis; //prefab to spawn
    public float cooldown; //time between consecutive uses
    
    private float startTime = 0; //time switch last hit
	
	void Update () {
        if (startTime != 0 && (Time.time - startTime) >= cooldown)
            startTime = 0;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (Time.time > 2 && startTime == 0)
            spawn();
    }

    void spawn()
    {
        startTime = Time.time;
        Instantiate(spawnThis, transform.GetChild(0).position,
            spawnThis.transform.rotation);
    }
}
