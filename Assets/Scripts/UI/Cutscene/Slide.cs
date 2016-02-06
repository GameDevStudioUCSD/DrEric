using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slide : MonoBehaviour {

    public float length;
    public Slide[] subSlides;

    private Image[] imagesInSlide;

    // Built in Unity methods 
	void Start () {
        DetermineImagesInSlide();
        SetupFading();
	}
	void Update () {
	
	}

    // Public methods
    public Image GetImage()
    {
        return gameObject.GetComponent<Image>();
    }
    public void SetLength(float l)
    {
        length = l;
        SetupFading();
    }

    // Private methods
    private void DetermineImagesInSlide()
    {
        imagesInSlide = new Image[1];
        imagesInSlide[0] = GetImage();
    }

    private void SetupFading()
    {
        FadeUI fadingScript = this.gameObject.AddComponent<FadeUI>();
        fadingScript.lengthBeforeFade = .9f * length;
        fadingScript.lengthOfFade = .1f * length;
        fadingScript.elementsToFade = imagesInSlide; 
    }
}
