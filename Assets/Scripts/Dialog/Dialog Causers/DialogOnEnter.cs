using UnityEngine;
using System.Collections;

public class DialogOnEnter : MonoBehaviour {
public string dialog;
public DialogBox boxUhText;
	void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            boxUhText.AppendText(dialog);
            GameObject.Destroy(this.gameObject);
        }
    }
}
