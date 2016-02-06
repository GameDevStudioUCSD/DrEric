using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathCount : MonoBehaviour {

	// Use this for initialization
    public int deathCount = 0;
    public const string label = "Death Count: ";
    public Text text;
    private RandomRotations rotator;
    private Color originalColor;
	void Start () {
        UpdateCount();
        rotator = GetComponent<RandomRotations>();
        originalColor = text.color;
	}

    public void Increment()
    {
        text.color = Color.red;
        rotator.enabled = true;
        deathCount++;
        UpdateCount();
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
        GameObject counter = GameObject.Find("DeathCount");
        if(counter != null)
            return counter.GetComponent<DeathCount>().deathCount;
        return -1;
    }
}
