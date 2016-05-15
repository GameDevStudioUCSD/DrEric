using UnityEngine;

public class WaterCollider : MonoBehaviour {
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    void OnCollisionEnter2D(Collision2D c) {
        Debug.Log("@#@");
    }

    void OnTriggerEnter2D(Collider2D c) {
        Debug.Log("@#@");
    }

    void OnCollisionEnter(Collision c) {
        Debug.Log("@#@");
    }

    void OnTriggerEnter(Collider c) {
        Debug.Log("@#@");
    }
}