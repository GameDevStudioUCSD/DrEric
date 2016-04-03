using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class MultiDialogOnEnter : MonoBehaviour {

    public DialogBox dialogBox;
    public DialogCharacterPair[] dialogs;
    public bool appendText;
    public bool destroyOnEnter = true;
    public bool pauseWhenActivated;

    private Queue dialogPairs;
    private bool hasActivated = false;

    void Start()
    {
        dialogPairs = new Queue();
    }

	void Update () {
        if(dialogPairs.Count != 0 && !dialogBox.gameObject.activeSelf )
        {
            DialogCharacterPair dialogPair;
            dialogPair = (DialogCharacterPair)dialogPairs.Dequeue();
            if(dialogPair.character != null)
            {
                dialogBox.SetImage(dialogPair.character.characterImage);
                dialogBox.SetAudioClip(dialogPair.character.talkingNoise);
            }
            dialogBox.DisplayText(dialogPair.dialog);
        }
        else if( (hasActivated && dialogPairs.Count == 0 ) && destroyOnEnter )
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && dialogPairs.Count == 0 )
        {
            foreach(DialogCharacterPair d in dialogs)
            {
                dialogPairs.Enqueue(d);
                hasActivated = true;
            }
        }
    }
}

[System.Serializable]
public class DialogCharacterPair: System.Object
{
    [Multiline()]
    public string dialog;
    public DialogCharacter character;
}
