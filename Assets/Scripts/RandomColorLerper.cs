using UnityEngine;
using System.Collections;

public class RandomColorLerper : MonoBehaviour {

    public float lerpTime = 1;
    Color currColor, nextColor;
    float startTime;
    Renderer rend;
	// Use this for initialization
	void Start () {
        startTime = -10;
        nextColor = GetRandomColor();
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > startTime + lerpTime)
        {
            currColor = nextColor;
            nextColor = GetRandomColor();
            startTime = Time.time;
        }
        else
        {
            float lerpVal = (Time.time - startTime)/lerpTime;
            Color lerpColor = Color.Lerp(currColor, nextColor, lerpVal );
            rend.material.color = lerpColor;
        }
	}
    

    Color GetRandomColor()
    {
        const float MAXRGB = 255;
        float r, g, b;
        r = Random.Range(0, MAXRGB)/MAXRGB;
        g = Random.Range(0, MAXRGB)/MAXRGB;
        b = Random.Range(0, MAXRGB)/MAXRGB;
        return new Color(r,g,b);
    }
}
