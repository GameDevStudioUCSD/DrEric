using UnityEngine;
using System.Collections;

public class DialogTest : MonoBehaviour {
	private Conversation conversation;
	// Use this for initialization
	void Start () {
		conversation = new Conversation("dialogs.json");
		conversation.add (new Dialog("hello", new Character(), true));
		conversation.add (new Dialog("H2LLENUMBER2", new Character(), true));
	}
	
	// Update is called once per frame
	void Update () {
        conversation.getNext ().display ();
	}
}
