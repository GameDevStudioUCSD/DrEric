using UnityEngine;
using System.Collections;

public class Dialog
{
	private string text;
	private int portrait;
	
	private Character character;
	
	public Dialog(string dialogText, Character character)
	{
		text = dialogText;
		this.character = character;
		portrait = -1;
	}
	
	public string getText()
	{
		return text;
	}
}