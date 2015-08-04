using UnityEngine;
using System.Collections;
/**
 * Filename: RandomRotations.cs \n
 * Author: Michael Gonzalez \n
 * Contributing Authors: N/A \n
 * Date Drafted: 8/2/2015 \n
 * Description: This script will wildly shake its parent object. It grabs a 
 *              reference to the parent's transform and rotates the parent by
 *              an origin by a definable range. The origin, or starting 
 *              rotation, may be shifted by public offsets. 
 */
public class RandomRotations : MonoBehaviour {
    /** The range determines the maximum absolute amount an object may rotate 
     * away from its origin. That is, the new rotation will be bounded by 
     * origin - range and origin + range
     */
    public float range = 20;
    /** The yOffset shifts the y origin by this amount */
    public float yOffset = 0;
    /** The yOffset shifts the x origin by this amount */
    public float xOffset = 0;
	
    /** Every update frame, shake the object! */
	void Update () {
        // First, set up the actual ranges
        float rangeXMin = xOffset - range;
        float rangeXMax = xOffset + range;
        float rangeYMin = yOffset - range;
        float rangeYMax = yOffset + range;
        // Second, determine the random x and y values
        float xVal = Random.Range( rangeXMin, rangeXMax);
        float yVal = Random.Range( rangeYMin, rangeYMax);
        // Finally, rotate!
        transform.rotation = Quaternion.Euler(xVal, yVal, 0);
	}
}
