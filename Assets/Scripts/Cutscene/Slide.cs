using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slide : MonoBehaviour {

    public float length;
    public Slide[] subSlides;

    private Image[] imagesInSlide;
    private float startTime;
    private int subSlideIdx = 1;

    // Built in Unity methods 
	void Start () {
        startTime = Time.time;
        DetermineImagesInSlide();
        SetupFading();
	}
	void Update () {
        float timeEllapsed = Time.time - startTime;
        float ratioForNextSlide = ((float)subSlideIdx / (subSlides.Length+1))*length;
        if( subSlides.Length > 0 &&  subSlideIdx < (subSlides.Length +1) && timeEllapsed > ratioForNextSlide)
        {
            subSlides[subSlideIdx - 1].gameObject.SetActive(true);
            subSlideIdx++;
        }
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
        int numberOfTotalSlides = subSlides.Length + 1;
        imagesInSlide = new Image[numberOfTotalSlides];
        imagesInSlide[0] = GetImage();
        for(int i = 1; i < imagesInSlide.Length; i++)
        {
            imagesInSlide[i] = subSlides[i - 1].GetImage();
            subSlides[i - 1].SetLength(length);
        }
    }


    private void SetupFading()
    {
        FadeUI fadingScript = this.gameObject.GetComponent<FadeUI>();
        if (fadingScript == null)
            fadingScript = this.gameObject.AddComponent<FadeUI>();
        fadingScript.lengthBeforeFade = .9f * length;
        fadingScript.lengthOfFade = .1f * length;
        fadingScript.elementsToFade = imagesInSlide; 
    }
}
