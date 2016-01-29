using UnityEngine;
using System.Collections;

public class Consumable : MonoBehaviour
{
    public bool resetJumps = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            if(resetJumps) {
                BallController.ResetJumps();
            }
        }
    }
}
