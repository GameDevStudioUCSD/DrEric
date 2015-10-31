using UnityEngine;
using System.Collections;

/**
 * FileName: ScaleMultiplier.cs \n
 * Author: Michael Gonzalez
 * Date Drafted: 10/31/2015
 * Description: This script creates the appearance of a GameObject growing and 
 *              shrinking. More specifically, it lerps the scale of a 
 *              GameObject by a definable scalar.
 */
public class ScaleMultiplier : MonoBehaviour {

    [Range(0,10)]
    public float scale = 2f;
    [Range(0,10)]
    public float timeToLerp = 1f;
    private Vector3 initScale;
    private Vector3 destScale;
    private bool isScaling = true;
    public bool isNormalScale = true;
	void Start () {
        initScale = transform.localScale;
        destScale = initScale * scale;
	}
	
	// Update is called once per frame
	void Update () {
        if( isScaling )
        {
            if( isNormalScale ) {
                transform.localScale = Vector3.Lerp(transform.localScale, initScale, Time.deltaTime);
            }
            else {
                transform.localScale = Vector3.Lerp(transform.localScale, destScale, Time.deltaTime);
            }
        }
	
	}
    public void ToggleScale()
    {
        isNormalScale = !isNormalScale;
    }
}
