using UnityEngine;
using System.Collections;

public class DialogOnEnter : MonoBehaviour {
    public string dialog;
    public DialogBox boxText;
    public bool appendText;
    public bool destroyOnEnter = true;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            if (appendText)
                boxText.AppendText(dialog);
            else
                boxText.DisplayText(dialog);
            if(destroyOnEnter)
                GameObject.Destroy(this.gameObject);
        }
    }
}
