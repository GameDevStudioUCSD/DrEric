using UnityEngine;
using System.Collections;

public class DialogOnEnter : MonoBehaviour {
public string dialog;
public DialogBox boxText;
	void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            boxText.DisplayText(dialog);
            GameObject.Destroy(this.gameObject);
        }
    }
}
