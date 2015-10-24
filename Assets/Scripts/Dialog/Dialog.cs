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
	
	public string getText() { return text; }
    /**  
     *  Determines if the next dialogue is left oriented.
     *  
     *  @return A boolean representing if the dialog is left positioned or not. 
     */
    public bool getIsLeft() { return left; }
}