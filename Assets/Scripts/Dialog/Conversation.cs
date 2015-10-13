using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversation 
{
	private List<Dialog> leftDialogs;
	private List<Dialog> rightDialogs;

	private int leftIndex;
	private int rightIndex;
	
	public Conversation(string filename)
	{
		leftDialogs = new List<Dialog> ();
		rightDialogs = new List<Dialog> ();
	}
	
	public void add(Dialog dialog, bool left)
	{
		if (left)
			leftDialogs.Add (dialog);
		else
			rightDialogs.Add (dialog);
	}
	
	public Dialog getNextLeft()
	{
		return leftDialogs [leftIndex++];
	}

	public Dialog getNextRight()
	{
		return rightDialogs[rightIndex++];
	}
}