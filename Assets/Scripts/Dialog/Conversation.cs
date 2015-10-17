using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Conversation 
{
	private List<Dialog> leftDialogs;
	private List<Dialog> rightDialogs;

    private int leftIndex;
	private int rightIndex;

    private string conversationName;

	public Conversation(string jsonFilename, string conversationName)
	{
<<<<<<< HEAD
        TextAsset jsonAsset = Resources.Load(@jsonFilename) as TextAsset;
        string json = jsonAsset.text;

        dynamic parsed = JSONParser.parse(json);
        dynamic conversations = parsed["conversations"];

        leftDialogs = new List<Dialog>();
        rightDialogs = new List<Dialog>();

        for (int i = 0; i < conversations.Count; i++)
        {
            string name = conversations[i]["name"];
            if (!name.Equals(conversationName)) continue;

            dynamic left_strings = conversations[i]["left_strings"];
            for (int j = 0; j < left_strings.Count; j++)
            {
                leftDialogs.Add(new Dialog(left_strings[j], null));
        
            }
            dynamic right_strings = conversations[i]["right_strings"];
            for (int j = 0; j < right_strings.Count; j++)
            {
                rightDialogs.Add(new Dialog(right_strings[j], null));
            }

            //string leftPortrait = conversations[i]["left_portrait"];
            //string rightPortrait = conversations[i]["right_portrait"];
            //TODO PORTRAITS NULL CHARACTESR
        }
=======
        dynamic d = JSONParser.parseFile(filename);

		leftDialogs = new List<Dialog> ();
		rightDialogs = new List<Dialog> ();
>>>>>>> origin/Dialog
	}
	
	private void add(Dialog dialog, bool left)
	{
		if (left)
			leftDialogs.Add (dialog);
		else
			rightDialogs.Add (dialog);
	}
	
	public Dialog getNextLeft()
	{
        if (leftIndex >= leftDialogs.Count)
            return null;

		return leftDialogs [leftIndex++];
	}

	public Dialog getNextRight()
	{
        if (rightIndex >= rightDialogs.Count)
            return null;

		return rightDialogs[rightIndex++];
	}
}