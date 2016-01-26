using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox : MonoBehaviour {

    public enum type { autoRead, clickToRead, nAutoRestClick }
	// Use this for initialization
    public string dialog;
    public int maxCharCount = 52;
    public Text firstLine;
    public Text secondLine;
    public float speed = 0.2f;
    public type typeOfDialogBox;
    public int nAutoReads = 1;
    public float timeBetweenAutoReads = 1.0f;

    string[] wordList;
    string wordBuffer = "";
    private int wordIdx = 0;
    Text currentLine;
    float lastCharPrinted = 0;
    AudioSource chipSource;
    float chipLength;
    bool isReading = true;
    int charWidth = 15;
	void Start () {
        // Set the word list
        wordList = dialog.Split(' ');
        // Save the start time
        lastCharPrinted = Time.time;
        // Setup word list 
        currentLine = firstLine;
        // Clear text
        firstLine.text = "";
        secondLine.text = "";
        // Setup playback
        chipSource = GetComponent<AudioSource>();
        chipLength = chipSource.clip.length;
	}
	
	void Update () {
        // Define max char count
        maxCharCount = (int)(currentLine.rectTransform.rect.width)/charWidth;
        if (Time.time - lastCharPrinted > speed)
        {
            if (HasFinished())
                Invoke("DeactiveObject", timeBetweenAutoReads);
            lastCharPrinted = Time.time;
            CheckBuffer();
            if (IsLineInBounds() && isReading)
                PopBuffer();
            else if (isReading)
                ScrollDown();
            else if (!isReading)
            {
                switch (typeOfDialogBox)
                {
                    case type.autoRead:
                        isReading = true;
                        lastCharPrinted += timeBetweenAutoReads;
                        break;
                    case type.nAutoRestClick:
                        isReading = true;
                        lastCharPrinted += timeBetweenAutoReads;
                        nAutoReads--;
                        if (nAutoReads == 0)
                            typeOfDialogBox = type.clickToRead;
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
    
    // Pops a character off the word buffer onto the dialog box
    private void PopBuffer()
    {
        if( wordBuffer != "" ) {
            currentLine.text = currentLine.text + wordBuffer[0];
            if (IsPunctuation(wordBuffer[0].ToString()))
                isReading = false;
            if (wordBuffer.Length == 1)
                wordBuffer = "";
            else
                wordBuffer = wordBuffer.Substring(1);
            // Play a random noise from this character's voice to emulate speech
            chipSource.Play();
            chipSource.time = Random.RandomRange(0, chipLength);
        }
    }

    // Returns true if the buffer is empty.
    // If it is, then it loads a new word into the buffer and prints white 
    // space onto the current line and increments the word list index
    private bool CheckBuffer()
    {
        if (wordBuffer == "" && !HasFinished())
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
            secondLine.text = "";
            currentLine = secondLine;
        }
    }
    public void ReadMore()
    {
        isReading = true;
        if (HasFinished())
            this.gameObject.SetActive(false);
    }
    bool IsPunctuation( string str ) {
        return (str == "." || str == "?" || str == "!");
    }
    public bool HasFinished()
    {
        return (wordBuffer == "" && wordIdx >= wordList.Length);

    }
    public void ADebug()
    {
            Debug.Log("Clicked dialog box!");
    }
}
