using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathCount : MonoBehaviour {

	// Use this for initialization
    public string label = "Organism Death Count: ";
    public Text text;
	public static DeathCount singleton;
    private RandomRotations rotator;
    private Color originalColor;
	private int deathCount;

	void Start () {
        UpdateCount();
        rotator = GetComponent<RandomRotations>();
        originalColor = text.color;
		label = text.text;
		deathCount = 0;
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
