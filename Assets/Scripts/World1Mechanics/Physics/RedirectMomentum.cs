using UnityEngine;
using System.Collections;

public class RedirectMomentum : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void Redirect(Rigidbody2D rigid, Vector2 dirr)
    {
        Vector2 unitV = dirr.normalized;
        float magnitude = rigid.velocity.magnitude;
        rigid.velocity = magnitude * unitV;
    }
}
