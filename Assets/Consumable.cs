using UnityEngine;
using System.Collections;

public class Consumable : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
