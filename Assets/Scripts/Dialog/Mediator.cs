using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mediator : MonoBehaviour {

    private static GUIController controller;
    private IDictionary<string, Conversation> conversations;
    private Conversation currentConversation;

	private float previousTime;
	private int conversationTurn = 0;

    //private DialogState dialogState;

    /** 
     * gets the refernce to the GUI controller from teh canvas. 
     */
	void Awake() {
		controller = GameObject.Find ("Canvas").GetComponent<GUIController> ();
	}

    /**
     * Initializlization
     */
	void Start () {
		
        conversations = new Dictionary<string, Conversation>();
        //parse dialogs.json into conversations
        //conversations[json.conversationName] = new Conversation(dialogs.json, json.conversationName);

        conversations["name_of_convo1"] = new Conversation("dialogs/test1", "name_of_convo1");
        loadConversation("name_of_convo1");
      
		previousTime = Time.time;
	}
	
	// Update is called once per frame

        /** 
         * Update the conversation once per frame. 
         */

	void Update () {
        if (currentConversation != null && Input.GetMouseButtonDown(0))
            advanceConversation();
	}

    /**  
     *  Loads conversation from the conversation's dictionary.
     *  
     *  @param conversationName 
     */
    public void loadConversation(string conversationName)
    {
        currentConversation = conversations[conversationName];
    }

    /**  
     *  Moves the conversation to the next dialog.
     */
    public void advanceConversation()
    {
        if (currentConversation == null)
        {
            Debug.Log("Tried to read from a null conversation. "
                + "You're not assigning currentConversation somewhere. "
                + "Try calling loadConversation([conversationName]) as "
                + "specified in the json.");
            return;
        }

        if (currentConversation.hasNext())
        {
            controller.say(currentConversation.getNext());
        }
        else
        {
            endConversation();
        }

        conversationTurn++;
    }

    /**  
     *  This method should be called when reach the end of the conversation. It hides all the text.
     */
    private void endConversation()
    {
        //dialogState.conversationEnded();
        controller.endSpeech();
        currentConversation = null;
    }
}
