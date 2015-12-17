using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Filename: FadeUI.cs \n
 * Author: Michael Gonzalez \n
 * Contributing Authors: N/A \n
 * Date Drafted: ??? (Unfold Script)\n
 * Description: This script will make one UI element or a list of UI elements 
 *              fade away. The fading speed and the time before the fade may be
 *              defined. \n\n 
 *              -- WARNING: This script may have unintended effects if
 *              used in conjunction with another script that modifies an 
 *              element's color!! --
 */
public class FadeUI : MonoBehaviour {
    /** This is the list of elements to fade */
    public Graphic[] elementsToFade;
    /** This is the duration in seconds it takes for the script to fully fade 
     *  away */
    public float lengthOfFade = 2;
    /** This is the amount of time in seconds the script will wait before it 
     *  begins to fade the UI elements list */
    public float lengthBeforeFade = 2;
    private Color[] originalColors;
    private Color[] endColors;
    private float startTime;
    /** This method saves a copy of each elements' original color */
	void Start () {
        originalColors = new Color[elementsToFade.Length];
        endColors = new Color[elementsToFade.Length];
        int i = 0;
        float r, g, b, a;
        // Save a copy of each original color
        foreach (Graphic elem in elementsToFade)
        {
            r = elem.color.r;
            g = elem.color.g;
            b = elem.color.b;
            a = elem.color.a;
            originalColors[i] = new Color(r, g, b, a);
            endColors[i] = new Color(r, g, b, 0);
            i++;
        }
        // Set the start and end times
        startTime = Time.time + lengthBeforeFade;
	}
    /** Fades the element list and destroys the objects when the fade completes 
     */
	void Update () {
        if (Time.time < startTime)
            return;
        float percentToLerp = (Time.time - startTime)/lengthOfFade;
        int i = 0;
        foreach(Graphic elem in elementsToFade)
        {
            elem.color = Color.Lerp(originalColors[i], endColors[i], percentToLerp);
            i++;
        }
        if (Time.time - startTime > lengthOfFade)
            Destroy(this.GetComponent<Transform>().gameObject);
	}
}
