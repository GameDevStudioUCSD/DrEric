using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour
{

    private Text leftText;
    private Text rightText;
    private Image leftImage;
    private Image rightImage;

    private RectTransform textLeftTr, textRightTr, imageLeftTr, imageRightTr;

    public const float DIALOG_MARGIN_LEFT_RIGHT = 0.05F;
    public const float DIALOG_MARGIN_TOP_BOTTOM = 0.05F;
    public const float DIALOG_IMAGE_WIDTH = 100;
    public const float DIALOG_IMAGE_HEIGHT = 100;
    
    /** 
     * Initialize the GUI controllers. 
     */
    void Start()
    {
        GameObject canvasObject = GameObject.Find("Canvas");
        textLeftTr = (RectTransform)canvasObject.transform.Find("LeftText");
        textRightTr = (RectTransform)canvasObject.transform.Find("RightText");

        imageLeftTr = (RectTransform)canvasObject.transform.Find("LeftImage");
        imageRightTr = (RectTransform)canvasObject.transform.Find("RightImage");

        setDialogToBottom();

        leftText = textLeftTr.GetComponent<Text>();
        rightText = textRightTr.GetComponent<Text>();


        leftImage = imageLeftTr.GetComponent<Image>();
        rightImage = imageRightTr.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setDialogToBottom()
    {
        int width = Screen.width;
        int height = Screen.height;

        imageLeftTr.sizeDelta = new Vector2(DIALOG_IMAGE_WIDTH, DIALOG_IMAGE_HEIGHT);
        imageRightTr.sizeDelta = new Vector2(DIALOG_IMAGE_WIDTH, DIALOG_IMAGE_HEIGHT);

        textLeftTr.sizeDelta = new Vector2(width - 2 * width * DIALOG_MARGIN_LEFT_RIGHT - DIALOG_IMAGE_WIDTH, DIALOG_IMAGE_HEIGHT);
        textRightTr.sizeDelta = new Vector2(width - 2 * width * DIALOG_MARGIN_LEFT_RIGHT - DIALOG_IMAGE_WIDTH, DIALOG_IMAGE_HEIGHT);


        imageLeftTr.pivot = textLeftTr.pivot = new Vector2(0, 0);
        imageRightTr.pivot = textRightTr.pivot = new Vector2(1, 0);

        imageLeftTr.position = new Vector2(width * DIALOG_MARGIN_LEFT_RIGHT, height * DIALOG_MARGIN_TOP_BOTTOM);
        imageRightTr.position = new Vector2(width - width * DIALOG_MARGIN_LEFT_RIGHT, height * DIALOG_MARGIN_TOP_BOTTOM);

        textLeftTr.position = new Vector2(width * DIALOG_MARGIN_LEFT_RIGHT + DIALOG_IMAGE_WIDTH, height * DIALOG_MARGIN_TOP_BOTTOM);
        textRightTr.position = new Vector2(width - width * DIALOG_MARGIN_LEFT_RIGHT - DIALOG_IMAGE_WIDTH, height * DIALOG_MARGIN_TOP_BOTTOM);
    }

    public void setDialogToTop()
    {
        int width = Screen.width;
        int height = Screen.height;

        imageLeftTr.sizeDelta = new Vector2(DIALOG_IMAGE_WIDTH, DIALOG_IMAGE_HEIGHT);
        imageRightTr.sizeDelta = new Vector2(DIALOG_IMAGE_WIDTH, DIALOG_IMAGE_HEIGHT);

        textLeftTr.sizeDelta = new Vector2(width - 2 * width * DIALOG_MARGIN_LEFT_RIGHT - DIALOG_IMAGE_WIDTH, DIALOG_IMAGE_HEIGHT);
        textRightTr.sizeDelta = new Vector2(width - 2 * width * DIALOG_MARGIN_LEFT_RIGHT - DIALOG_IMAGE_WIDTH, DIALOG_IMAGE_HEIGHT);

        imageLeftTr.pivot = textLeftTr.pivot = new Vector2(0, 1);
        imageRightTr.pivot = textRightTr.pivot = new Vector2(1, 1);

        imageLeftTr.position = new Vector2(width * DIALOG_MARGIN_LEFT_RIGHT, height - height * DIALOG_MARGIN_TOP_BOTTOM);
        imageRightTr.position = new Vector2(width - width * DIALOG_MARGIN_LEFT_RIGHT, height - height * DIALOG_MARGIN_TOP_BOTTOM);

        textLeftTr.position = new Vector2(width * DIALOG_MARGIN_LEFT_RIGHT + DIALOG_IMAGE_WIDTH, height - height * DIALOG_MARGIN_TOP_BOTTOM);
        textRightTr.position = new Vector2(width - width * DIALOG_MARGIN_LEFT_RIGHT - DIALOG_IMAGE_WIDTH, height - height * DIALOG_MARGIN_TOP_BOTTOM);
    }

    /**  
     *  Recursively parses given JSONString, returning a JSObject object containing Dictionary{string, JSObject}, List{JSObject}, string, int, float, bool and null objects.
     *  This determines who's turn for dialog and display.
     *  @param dialog, the dialog that will be displayed
     */
    public void say(Dialog dialog)
    {
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