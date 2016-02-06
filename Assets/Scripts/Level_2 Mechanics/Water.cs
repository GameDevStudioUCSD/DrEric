using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Water
/// 
/// Script attached to water that gives player infinite jumps while in it.
/// While player is in water collider, player will repeatedly call OnTriggerStay2D.
/// Through player collider, give player infinite jumps.
///
/// Depends on player's "BallController" component.
/// 
/// </summary>

public class Water : MonoBehaviour {

    // Tag the player object will have
    static string PLAYER_TAG = "Player";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Called every frame collider stays in the water
    void OnTriggerStay2D(Collider2D other)
    {
       // If the collider is player's, then give player infinite jump
       if(other.tag == PLAYER_TAG)
       {
           // Land player to reset jumps
           other.gameObject.GetComponent<BallController>().Land();
       }
    }
}
