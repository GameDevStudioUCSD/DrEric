using UnityEngine;
using System.Collections;

public class CutsceneSlides : MonoBehaviour {

    public GameObject[] slides;
    public float overideLength = -1;

    private int slideIdx = 0;
    private GameObject currentSlide;
	void Start () {
        CreateSlide();
	}
	
	void Update () {
        if (currentSlide == null && slideIdx < slides.Length)
            CreateSlide();
        if (slideIdx == slides.Length && currentSlide == null)
            LevelLoader.LoadLevel(World.MainMenu, Level.One);
	}


    void CreateSlide()
    {
        // Create the game object
        currentSlide = GameObject.Instantiate(slides[slideIdx]);
        // Fix the transform's position
        RectTransform trans = currentSlide.GetComponent<RectTransform>();
        trans.SetParent(transform);
        trans.offsetMax = trans.offsetMin = Vector2.zero;
        // Increment the slide index
        slideIdx++;
        // Override the slide's length 
        if (overideLength > 0)
        {
            Slide slideScript = currentSlide.GetComponent<Slide>();
            slideScript.SetLength(overideLength);
        }
    }
}
