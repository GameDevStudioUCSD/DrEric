using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Used for List<T>()

public class SaplingSpawner : MonoBehaviour {

    // Heights corresponding to short, middle, and tall trees
    public static List<float> Heights = new List<float>{-2.0f, -8.0f, -14.0f};

    public GameObject sapling = null; //prefab to spawn
    public GameObject tree = null; //analagous tree
    public float cooldown = 5; //time between consecutive uses
    //distance between a spot in the past and the analagous spot in the present
    public float distanceToPresent = -100;

    // The height of the corresponding tree
    public float treeHeight = 0.0f;

    private float startTime = 0; //time switch last hit

    void Start()
    {
        // Randomize which height this sapling spawner will get.
        int index = Random.Range(0, SaplingSpawner.Heights.Count);
        treeHeight = SaplingSpawner.Heights[index];
        SaplingSpawner.Heights.RemoveAt(index);

    }

    void Update()
    {
        if (startTime != 0 && (Time.time - startTime) >= cooldown)
            startTime = 0;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            if (Time.time > 2 && startTime == 0)
                spawn();
        }
    }

    void spawn()
    {
        startTime = Time.time;
        GameObject spawned = (GameObject)Transform.Instantiate(sapling,
            transform.GetChild(0).position, sapling.transform.rotation);
        Vector3 treePosition = transform.GetChild(0).position;
        treePosition.y = treeHeight;
        GameObject spawnedTree = (GameObject)Transform.Instantiate(tree,
            treePosition, tree.transform.rotation);
        spawned.GetComponent<PlatformSapling>().presentTree = spawnedTree.GetComponent<PlatformTree>();
        spawned.GetComponent<Rigidbody2D>().isKinematic = false;
        spawnedTree.GetComponent<PlatformTree>().shiftX(distanceToPresent);
    }
}
