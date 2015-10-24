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
            Debug.Log(raw_dialog["text"]);
            dialogs.Add(
                new Dialog(
                    raw_dialog["text"]
                  , null //implement the character class
                  , raw_dialog["isLeft"]
                )
            );
        }
	}
	
	public Dialog getNext()
	{
        if (index >= dialogs.Count)
            return null;

		return dialogs[index++];
	}

    /**  
     *  Determines whether there's dialog remaining in the conversation.
     *  
     *  @return true if there is dialog remaining.
     */
    public bool hasNext()
    {
        return index < dialogs.Count;
    }
}