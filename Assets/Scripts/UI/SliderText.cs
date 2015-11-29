using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SliderText : MonoBehaviour {

	Text text;
	public Slider slider;

	// Use this for initialization
	void Start () {
		text = slider.gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void textUpdate()
    {
		text.text = "" + (int)(slider.value * 20);
    }


}
