using UnityEngine;
using System.Collections;

public class SplatterGrowth : MonoBehaviour {
    public float startScale = 0.0f;
    public float finalScale = 0.5f;
    public float growthTime = 1.0f;
    private Vector3 destScale;

	// Use this for initialization
	void Start () {
        destScale = new Vector3(finalScale, finalScale, 1);
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.localScale.x < finalScale)
            transform.localScale = Vector3.MoveTowards(transform.localScale, destScale,
                                                 1/growthTime * Time.deltaTime);
	}

    public void ResetSplatter()
    {
        transform.localScale = new Vector3(startScale, startScale, 1);
    }
}
