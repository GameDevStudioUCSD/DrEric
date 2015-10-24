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
	
	public string getText() { return text; }
    public bool getIsLeft() { return left; }
    public Sprite getPortrait() { return character.getPortrait(expression); }
}