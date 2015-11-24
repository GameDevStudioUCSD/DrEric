using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SliderText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void textUpdate(Slider slider)
    {
		Text sliderText = (GameObject.FindWithTag(slider.GetComponent<Text> ().text)).GetComponent<Text>();
		sliderText.text = "" + (int)(slider.value * 20);
    }


}
