using UnityEngine;
using System.Collections;

public class Consumable : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
            Destroy(this.gameObject);
    }
}
