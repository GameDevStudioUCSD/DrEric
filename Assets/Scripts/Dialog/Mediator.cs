using UnityEngine;
using System.Collections;

public class Mediator : MonoBehaviour {

	private static GUIController controller;
	private Conversation conversation;

	private float previousTime;
	private int conversationTurn = 0;

	void Awake() {
		controller = GameObject.Find ("Canvas").GetComponent<GUIController> ();
	}

	// Use this for initialization
	void Start () {
		conversation = new Conversation("dialogs.json");

		conversation.add (new Dialog("Hello right", new Character()), true);
		conversation.add (new Dialog("Hello left", new Character()), false);
		conversation.add (new Dialog("How's it going right", new Character()), true);
		conversation.add (new Dialog("Fantastic left", new Character()), false);
		conversation.add (new Dialog("Great weather we're having right", new Character()), true);
		conversation.add (new Dialog("No, I hate the humidity, left", new Character()), false);

		previousTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - previousTime > 1) {
			if (conversationTurn % 2 == 0) {
				Dialog nextDialogLeft = conversation.getNextLeft();
				controller.setLeftText(nextDialogLeft.getText ());
			}
			else {
				Dialog nextDialogRight = conversation.getNextRight();
				controller.setRightText(nextDialogRight.getText ());
			}
			previousTime = Time.time;
			conversationTurn++;

		}
	}
}
