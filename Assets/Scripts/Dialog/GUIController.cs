using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

	private Text leftText;
	private Text rightText;
	private Image leftImage;
	private Image rightImage;

	// Use this for initialization

        /** 
         * Initialize the GUI controllers. 
         */
	void Start () {
        GameObject canvasObject = GameObject.Find("Canvas");
        Transform textLeftTr = canvasObject.transform.Find("LeftText");
        Transform textRightTr = canvasObject.transform.Find("RightText");
		leftText = textLeftTr.GetComponent<Text>();
		rightText = textRightTr.GetComponent<Text>(); 
        
        Transform imageLeftTr = canvasObject.transform.Find("LeftImage");
        Transform imageRightTr = canvasObject.transform.Find("RightImage");
        leftImage = imageLeftTr.GetComponent<Image>();
		rightImage = imageRightTr.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    /**  
     *  Recursively parses given JSONString, returning a JSObject object containing Dictionary{string, JSObject}, List{JSObject}, string, int, float, bool and null objects.
     *  This determines who's turn for dialog and display.
     *  @param dialog, the dialog that will be displayed
     */
    public void say(Dialog dialog) {
        rightText.enabled = false;
        leftText.enabled = true;
		leftText.text = dialog.getText();

        if (dialog.getIsLeft())
        {
            leftText.enabled = true;
            rightText.enabled = false;
            leftText.text = dialog.getText();
        }
        else
        {
            leftText.enabled = false;
            rightText.enabled = true;
            rightText.text = dialog.getText();
        }

    }
    

    /**  
     *  Hides all the text and image portraits pertaining to the current conversation. 
     */
    public void endSpeech()
    {
        rightText.enabled = false;
        leftText.enabled = false;
        leftImage.enabled = false;
        rightImage.enabled = false;
    }
}
