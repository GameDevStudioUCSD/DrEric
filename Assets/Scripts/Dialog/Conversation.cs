using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Conversation 
{
    private List<Dialog> dialogs;
    private int index;

    private string conversationName;

	public Conversation(string jsonFilename, string conversationName)
	{
        TextAsset jsonAsset = Resources.Load(@jsonFilename) as TextAsset;
        string json = jsonAsset.text;

        JSObject parsed = JSONParser.parse(json);
        JSObject conversations = parsed["conversations"];

        dialogs = new List<Dialog>();

        JSObject raw_dialogs = conversations[conversationName];

        foreach (JSObject raw_dialog in raw_dialogs)
        {
            string text = raw_dialog["text"];
            string characterName = raw_dialog["character"];
            bool isLeft = raw_dialog["isLeft"];

            //TODO character should be loading from somewhere that instantiates all the characters to a lookup table
            Character character = CharacterTable.Instance.getCharacter("john");

            dialogs.Add(new Dialog(text, character, isLeft));
        }
	}
	
	public Dialog getNext()
	{
        if (index >= dialogs.Count)
            return null;

		return dialogs[index++];
	}

    public bool hasNext()
    {
        return index < dialogs.Count;
    }
}