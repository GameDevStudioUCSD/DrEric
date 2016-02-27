using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogOnEnter : MonoBehaviour {
    public string dialog;
    public DialogBox boxText;
    public bool appendText;
    public bool destroyOnEnter = true;
    public Sprite characterImage;
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
            if (characterImage != null)
                boxText.SetImage(characterImage);
        }
    }
}
