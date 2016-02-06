using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class DialogBox : MonoBehaviour {

/**
 * Filename: DialogBox.cs \n
 * Author: Michael Gonzalez\n
 * Contributing Authors: None \n
 * Date Drafted: January 25, 2016 \n
 * Description: This script controls the dialog boxes. It prints a message 
 *              character by character to the user and plays noises to 
 *              emulate the sound of speech. 
 */
    private const string EMPTYBUF = "";

    public enum type { autoRead, clickToRead, nAutoRestClick }
    // The text to be displayed in the dialog box
    public string dialog;
    // The maximum character count per line (OVERWRITTEN AT RUNTIME)
    public int maxCharCount = 52;
    // References to the text UI elements of the dialogbox
    public Text firstLine;
    public Text secondLine;
    // The delay between printing each individual character in seconds
    public float delayBetweenChars = 0.05f;
    // The type of the dialog box
    // If autoRead, then the dialog box will continue to scroll through the 
    // text without input from the user
    // If clickToRead, then the dialog box will promt the user to click
    // the box to progress through the dialog
    // If nAutoRestClick, then the dialog box will auto read for n sentences
    // and then act the same as clickToRead.
    public type typeOfDialogBox;
    // The n auto reads for the type nAutoRestClick
    public int nAutoReads = 1;
    // The time between sentences in seconds for autoRead and nAutoRestClick
    public float timeBetweenAutoReads = 1.0f;
    // The image for the flashing arrow to indicate that the player should 
    // click the dialog box
    public Image arrowImage;
    //fast mode multiplier
    public int speedmultiplier;

    // The list of words to display to the user
    string[] wordList;
    // The current index of wordList
    private int wordIdx = 0;
    // The current word buffer
    string wordBuffer = EMPTYBUF;
    // The reference to the current line being printed on
    Text currentLine;
    // The time in seconds at which the last character was printed
    float timeOfLastPop = 0;
    //determines text speedup when clicked
    bool fastmode = false;
    // A reference to the AudioSource to emulate talking
    AudioSource chipSource;
    // A copy of the length of the AudioClip attached to clipSource
    float chipLength;
    bool isNotAtEndOfSentence = true;
    // An approximate value of how wide each character is
    // TODO: Generalize
    int charWidth = 15;
	void Start () {
        // Set the word list
        wordList = dialog.Split(' ');
        // Save the start time
        timeOfLastPop = Time.time;
        // Setup word list 
        currentLine = firstLine;
        // Clear text
        ClearText();
        // Setup playback
        chipSource = GetComponent<AudioSource>();
        chipLength = chipSource.clip.length;
	}
	
	void Update () {
        // Define max char count
        maxCharCount = (int)(currentLine.rectTransform.rect.width)/charWidth;
        
        //check for fast mode
        int fastmodeflag = 0;
        if (fastmode) fastmodeflag++;
        if (Time.time - timeOfLastPop > (delayBetweenChars / ( fastmodeflag* speedmultiplier+1)))
        {
            if (HasFinished())
                Invoke("DeactiveObject", timeBetweenAutoReads);
            timeOfLastPop = Time.time;
            CheckBuffer();
            if (IsLineInBounds() && isNotAtEndOfSentence)
                PopCharOffBuffer();
            else if (isNotAtEndOfSentence)
                ScrollDown();
            // If we've finished the current sentence, then the type of
            // dialog box this is determines what we should do next
            else if (!isNotAtEndOfSentence)
            {
                fastmode = false;
                // The following switch statement handles autoreading
                // Click to read should be handled by a button script within
                // the unity editor
                timeOfLastPop += timeBetweenAutoReads;
                switch (typeOfDialogBox)
                {
                    case type.autoRead:
                        isNotAtEndOfSentence = true;
                        break;
                    case type.nAutoRestClick:
                        isNotAtEndOfSentence = true;
                        nAutoReads--;
                        if (nAutoReads == 0)
                            typeOfDialogBox = type.clickToRead;
                        break;
                    case type.clickToRead:
                        arrowImage.enabled = !arrowImage.enabled;
                        break;
                }
            }
        }
        else
            chipSource.Stop();
        
	}
    void DeactiveObject() { 
        this.gameObject.SetActive(false);
    }
    public void ClearText()
    {
        firstLine.text = secondLine.text = EMPTYBUF;
        wordBuffer = EMPTYBUF;
        currentLine = firstLine;
        wordIdx = 0;
    }
    
    // Pops a character off the word buffer onto the dialog box
    private void PopCharOffBuffer()
    {
        if( wordBuffer != EMPTYBUF ) {
            arrowImage.enabled = false;
            currentLine.text = currentLine.text + wordBuffer[0];
            if (IsPunctuation(wordBuffer[0].ToString()))
                isNotAtEndOfSentence = false;
            if (wordBuffer.Length == 1)
                wordBuffer = EMPTYBUF;
            else
                wordBuffer = wordBuffer.Substring(1);
            // Play a random noise from this character's voice to emulate speech
            chipSource.Play();
            chipSource.time = Random.Range(0, chipLength);
        }
    }

    // Returns true if the buffer is empty.
    // If it is, then it loads a new word into the buffer and prints white 
    // space onto the current line and increments the word list index
    private bool CheckBuffer()
    {
        if (wordBuffer == EMPTYBUF && !HasFinished())
        {
            wordBuffer = wordList[wordIdx];
            if (!IsPunctuation(wordBuffer))
                currentLine.text = currentLine.text + "   ";
            
            wordIdx++;
            return true;
        }
        return false;
    }
    // Checks if popping the buffer will overflow the current line. 
    private bool IsLineInBounds()
    {
        bool retVal = (currentLine.text + wordBuffer).Length <= maxCharCount;
        return retVal;
    }
    // If it will, then this function will push the current line to the first
    // and wipe the second.
    public void ScrollDown()
    {
        if (!IsLineInBounds())
        {
            firstLine.text = currentLine.text;
            secondLine.text = EMPTYBUF;
            currentLine = secondLine;
        }
    }
    public void ReadMore()
    {
        if (isNotAtEndOfSentence == true)
        {//if is at end of sentence = false, make it faster
            fastmode = true;
        }
        isNotAtEndOfSentence = true;
        if (HasFinished())
            this.gameObject.SetActive(false);
    }
    bool IsPunctuation( string str ) {
        return (str == "." || str == "?" || str == "!");
    }
    public bool HasFinished()
    {
        return (wordBuffer == EMPTYBUF && wordIdx >= wordList.Length);
    }
    public void DisplayText(string text)
    {
        SetText(text);
        this.gameObject.SetActive(true);
    }
    public void AppendText(string text)
    {
        string[] newWords = text.Split(' ');
        wordList= (string[])wordList.Concat(newWords);
        this.gameObject.SetActive(true);
    }
    public void SetText(string text)
    {
        ClearText();
        dialog = text;
        wordList = dialog.Split(' ');
    }
}
