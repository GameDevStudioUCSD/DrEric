using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class MultiDialogOnEnter : MonoBehaviour {

    public DialogBox dialogBox;
    public DialogCharacterPair[] dialogs;
    public bool appendText;
    public bool destroyOnEnter = true;
    public bool pauseWhenActivated;
    public bool randomizeText = false; // Randomize which dialog is played
    public float cooldown = 0.0f; // How long until the dialog can be triggered again
    public float SecondsWait;
    
    public string triggerTag = "Player";

    private Queue dialogPairs;
    private bool hasActivated = false;

    private float nextCooldownTime; // The next time dialog can be triggered

    void Start()
    {
        dialogPairs = new Queue();

        nextCooldownTime = Time.time; // Initialize
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
        // Check if dialog is cooling down
        if (Time.time < nextCooldownTime)
        {
            // Dialog is still cooling down, don't allow triggering
            return;
        }
        else
        {
            nextCooldownTime = Time.time + cooldown;
        }

        if (collider.tag == triggerTag && dialogPairs.Count == 0 )
        {
            if(randomizeText)
            {
                // Queue up random dialog
                int index = Random.Range(0, dialogs.Length);
                dialogPairs.Enqueue(dialogs[index]);
            }
            else
            {
                // Queue up all dialogs
                foreach (DialogCharacterPair d in dialogs)
                {
                    dialogPairs.Enqueue(d);
                    hasActivated = true;
                }
            }
            timeDelayDialog();
        }
    }
    
    IEnumerator timeDelayDialog()
    {
        yield return new WaitForSeconds(SecondsWait);
    }
    
}

[System.Serializable]
public class DialogCharacterPair: System.Object
{
    [Multiline()]
    public string dialog;
    public DialogCharacter character;
}
