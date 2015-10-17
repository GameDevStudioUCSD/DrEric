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

    public Text value;
    public Slider volumeSlider;

    public void textUpdate()
    {
        value.text = "" + (int)(volumeSlider.value*20);
    }


}
