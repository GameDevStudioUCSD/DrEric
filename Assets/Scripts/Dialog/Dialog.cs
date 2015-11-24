using UnityEngine;
using System.Collections;

public class Dialog
{
    public enum Expression
    {
        NEUTRAL,
        HAPPY,
        SAD
    };

    private string text;
	private Character character;
    private bool left;
    private Expression expression;
	
	public Dialog(string dialogText, Character character, bool isLeft)
	{
		text = dialogText;
		this.character = character;
        left = isLeft;
	}

    /**  
     *  Returns the text of the dialog.
     *  
     *  @return A string object representing the Dialog data. 
     */
    public string getText() { return text; }

    /**  
     *  Determines if the next dialogue is left oriented.
     *  
     *  @return A boolean representing if the dialog is left positioned or not. 
     */
    public bool getIsLeft() { return left; }
    public Sprite getPortrait() { return character.getPortrait(expression); }
}