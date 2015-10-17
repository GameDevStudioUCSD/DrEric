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

	void Awake() {
		controller = GameObject.Find ("Canvas").GetComponent<GUIController> ();
	}

	// Use this for initialization
	void Start () {
		
        conversations = new Dictionary<string, Conversation>();
        //parse dialogs.json into conversations
        //conversations[json.conversationName] = new Conversation(dialogs.json, json.conversationName);

        conversations["name_of_convo1"] = new Conversation("dialogs/test", "name_of_convo1");
        loadConversation("name_of_convo1");
      
		previousTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - previousTime > 0.5)
        {
            advanceConversation();
            previousTime = Time.time;
        }
	}

    public void loadConversation(string conversationName)
    {
        currentConversation = conversations[conversationName];
    }

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

        if (conversationTurn % 2 == 0)
        {
            Dialog nextDialogLeft = currentConversation.getNextLeft();
            if (nextDialogLeft == null)
            {
                endConversation();
                return;
            }
            controller.leftSay(nextDialogLeft.getText());
        }
        else
        {
            Dialog nextDialogRight = currentConversation.getNextRight();
            if (nextDialogRight == null)
            {
                endConversation();
                return;
            }
            controller.rightSay(nextDialogRight.getText());
        }
        previousTime = Time.time;
        conversationTurn++;
    }

    private void endConversation()
    {
        //dialogState.conversationEnded();
        currentConversation = null;
    }
}
