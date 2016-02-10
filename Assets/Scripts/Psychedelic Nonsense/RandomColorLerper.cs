using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/**
 * Filename: RandomColorLerper.cs \n
 * Author: Michael Gonzalez \n
 * Contributing Authors: N/A \n
 * Date Drafted: 8/2/2015 \n
 * Description: This script's primary intended purpose is to lerp an object
 *              between two random colors. The speed at which objects lerp 
 *              between colors may be defined. This script will work on 2D 
 *              objects, 3D objects, and text. If not specified, this script
 *              will assume the object is 3D. Furthermore, it provides a 
 *              static method that returns a random color.
 */
public class RandomColorLerper : MonoBehaviour {
    /** The length of the lerp in seconds */
    public float lerpDuration = .1f;
    Color currColor, goalColor;
    float initialTime;
    Renderer rend;
    public TrailRenderer trail;
    private Material trailMaterial;
    Image img;
    Text text;
    /** Set true if the object is a 2D image or sprite */
    public bool is2D = true;
    /** Set true this boolean if the object is text */
    public bool isText = false;
    /** Set true if the object is a sprite **/
    public bool isRenderer;
    /** Set true if the object is a trail renderer **/
    public bool isTrail;
    /** Setup overhead */
	void Start () {
        initialTime = -10;
        goalColor = GetRandomColor();
        if (isText)
            text = GetComponent<Text>();
        if (is2D)
            img = GetComponent<Image>();
        if (isTrail)
            trailMaterial = trail.material;
        if( isRenderer) 
            rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        // If the current time has exceeded the lerp duration, then define a 
        // new random goal color.
        if (Time.time > initialTime + lerpDuration)
        {
            currColor = goalColor;
            goalColor = GetRandomColor();
            initialTime = Time.time;
        }
        // Otherwise, continue lerping between the current color and the goal color
        else
        {
            float lerpVal = (Time.time - initialTime)/lerpDuration;
            Color lerpColor = Color.Lerp(currColor, goalColor, lerpVal );
            if (isText)
                text.color = lerpColor;
            if (is2D)
                img.color = lerpColor;
            if (isTrail)
                trailMaterial.color = lerpColor;
            if (isRenderer)
                rend.material.color = lerpColor;
        }
	}
    
    /** This method will return a random color with the alpha channel set to 1.*/
    public static Color GetRandomColor()
    {
        const float MAXRGB = 255;
        const float MINRGB = 128;
        float r, g, b;
        r = Random.Range(MINRGB, MAXRGB)/MAXRGB;
        g = Random.Range(MINRGB, MAXRGB)/MAXRGB;
        b = Random.Range(MINRGB, MAXRGB)/MAXRGB;
        return new Color(r,g,b);
    }
}
