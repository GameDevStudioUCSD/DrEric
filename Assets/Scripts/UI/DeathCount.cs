using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathCount : MonoBehaviour {

	// Use this for initialization
    public int deathCount = 0;
    public string label = "Death Count: ";
    public Text text;
	public static DeathCount singleton;
    private RandomRotations rotator;
    private Color originalColor;
	void Start () {
        UpdateCount();
        rotator = GetComponent<RandomRotations>();
        originalColor = text.color;
		label = text.text;
		singleton = this;
	}

    public void Increment()
    {
        text.color = Color.red;
        rotator.enabled = true;
        deathCount++;
        UpdateCount();
    }
	public static void IncrementDC() {
		singleton.Increment ();
	}
    private void UpdateCount()
    {
        text.text = label + deathCount;
    }
    public void ReturnToNormal()
    {
        text.color = originalColor;
        rotator.enabled = false;
    }
    public static int GetDeathCount()
    {
		GameObject counter = DeathCount.singleton.gameObject;
        if(counter != null)
            return counter.GetComponent<DeathCount>().deathCount;
        return -1;
    }
}
