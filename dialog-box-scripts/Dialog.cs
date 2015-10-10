using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialog : MonoBehaviour {
	private Text text;
	private Image image;
	
	void Start() {
		text = this.transform.GetChild(0).GetComponent<Text>();
		image = this.transform.GetChild(1).GetComponent<Image>();
	}

	void Update() {
		text.text = "test";
	}
}