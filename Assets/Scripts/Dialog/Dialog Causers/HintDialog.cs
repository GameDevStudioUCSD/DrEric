using UnityEngine;
using System.Collections;

public class HintDialog : MonoBehaviour {
	public string dialog;
	public DialogBox boxText;
	public Sprite characterImage;
	// Use this for initialization
	public void calledDialog()
	{
		
		boxText.DisplayText(dialog);
		if (characterImage != null)
			boxText.SetImage(characterImage);
	}
}
