using UnityEngine;
using System.Collections;

public class Dialog
{
	private string text;
	private int portrait;
	private Character character;

    private bool left;
	
	public Dialog(string dialogText, Character character, bool isLeft)
	{
		text = dialogText;
		this.character = character;
		portrait = -1;
        left = isLeft;
	}

    /**  
     *  Returns the text of the dialog.
     *  
     *  @return A string object representing the Dialog data. 
     */
    public string getText() { return text; }
    public bool getIsLeft() { return left; }
}