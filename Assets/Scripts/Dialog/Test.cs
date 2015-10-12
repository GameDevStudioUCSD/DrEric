using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Conversation 
{
	private List<Dialog> dialogs;
	private int index;

	public Conversation(string filename)
	{
		dialogs = new List<Dialog> ();
		//parse file into dialogs
	}

	public void add(Dialog dialog)
	{
		dialogs.Add(dialog);
	}

	public Dialog getNext()
	{
		int currentIndex = index;
		index++;
		return dialogs[currentIndex];
	}
}

public class Dialog
{
	private SpeechText text;
	private SpeechPortrait portrait;

	private Character character;
	private bool left;

	public Dialog(string dialogText, Character character, bool left)
	{
		text = new SpeechText(dialogText);
		this.character = character;
		portrait = new SpeechPortrait(this.character.getPicture());

		if (left)
		{
			text.setPosition(300, 300);
			portrait.setPosition(300, 300);
		}
		else
		{
			text.setPosition(400, 300);
			portrait.setPosition(700, 300);
		}
	}

	public void display()
	{
		text.display();
		portrait.display();
	}

	public void hide()
	{
		text.hide();
		portrait.hide();
	}
}

public class SpeechText
{
	//public UI.Text text;
	public string text;

	public SpeechText(string rawText)
	{
		//do something here to intiialize text with rawText
	}

	public void display()
	{
		Debug.Log ("display text" + text);
		//text.enabled = true;
	}

	public void hide()
	{
		Debug.Log ("hide text" + text);
		//text.enabled = false;
	}

	public void setPosition(int x, int y)
	{
		Debug.Log (x + " " + y);
	}
}

public class SpeechPortrait
{
	//public UI.Image portrait;
	public string portrait;

	public SpeechPortrait(string portrait)
	{
		this.portrait = portrait;
		//portrait.transform = Vector3(200, 200, 300);
	}
	
	public void display()
	{
		Debug.Log (portrait + "display");
		//portrait.enabled = true;
	}

	public void hide()
	{
		Debug.Log (portrait + "hide");
		//portrait.enabled = false;
	}

	public void setPosition(int x, int y)
	{
		Debug.Log (x + " " + y);
	}
}

public class Character
{
	public string getPicture()
	{
		return "my picture";
	}
}