﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Used for List<T>()

public class SaplingSpawner : MonoBehaviour {

    // Heights corresponding to short, middle, and tall trees
    public static List<float> Heights = new List<float>{-2.0f, -8.0f, -14.0f};

    public List<SaplingSpawner> saplingSpawners = new List<SaplingSpawner>();

    public GameObject leftTree;

    public Animator happyButtonAnimator = null;

    public GameObject sapling = null; //prefab to spawn
    public GameObject tree = null; //analagous tree
    public float cooldown = 5; //time between consecutive uses
    //distance between a spot in the past and the analagous spot in the present
    public float distanceToPresent = -100;

    // Is button pressable or not
    public bool pressable = true;

    // The height of the corresponding tree
    private float treeHeight = 0.0f;

    private float startTime = 0; //time switch last hit
    private AudioSource aSource;


    void Start()
    {
        // Randomize which height this sapling spawner will get.
        int index = Random.Range(0, SaplingSpawner.Heights.Count);
        treeHeight = SaplingSpawner.Heights[index];
        SaplingSpawner.Heights.RemoveAt(index);
        aSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (startTime != 0 && (Time.time - startTime) >= cooldown)
            startTime = 0;

        // If button is not active, then set the animation to down state
        if (!pressable)
        {
            happyButtonAnimator.SetBool("pressed", true);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            if (pressable)
            {
                if (Time.time > 2 && startTime == 0)
                {
                    spawn();
                    happyButtonAnimator.SetBool("pressed", true);
                    aSource.Play();

                    // Disable all sapling spawners if this one is pressed
                    foreach(SaplingSpawner sapling in saplingSpawners)
                    {
                        sapling.setPressable(false);
                    }
                } 
            }
        }
    }

    void spawn()
    {
        startTime = Time.time;
        // Sapling spawned
        GameObject spawned = (GameObject)Transform.Instantiate(sapling,
            transform.GetChild(0).position, sapling.transform.rotation);
        // Make sapling able to go through left tree boundary
        Collider2D[] saplingColliders = spawned.GetComponents<Collider2D>(); // Get all colliders on sapling (it has more than one)
        // Make all colliders on sapling ignore the tree collider
        for (int i = 0; i < saplingColliders.Length; i++)
        {
            Physics2D.IgnoreCollision(saplingColliders[i], leftTree.GetComponent<Collider2D>()); 
        }

        Vector3 treePosition = transform.GetChild(0).position;
        treePosition.y = treeHeight;
        GameObject spawnedTree = (GameObject)Transform.Instantiate(tree,
            treePosition, tree.transform.rotation);
        spawned.GetComponent<PlatformSapling>().presentTree = spawnedTree.GetComponent<PlatformTree>();
        spawned.GetComponent<Rigidbody2D>().isKinematic = false;
        spawnedTree.GetComponent<PlatformTree>().shiftX(distanceToPresent);
    }

    void pressEnd()
    {
        happyButtonAnimator.SetBool("pressed", false);
    }

    public void setPressable(bool flag)
    {
        this.pressable = flag;
    }
}
