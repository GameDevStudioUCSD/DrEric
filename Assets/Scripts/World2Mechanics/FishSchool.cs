using UnityEngine;
using System.Collections;

public class FishSchool : HasDialog {

	public int numFish;
	public GameObject barrier;
	public GameObject[] backgrounds;
	public DialogBox dialog;

    public string[] startDialog;
	public string[] midDialog;
	public string[] endDialog;

	string[] currDialog;
	int dialogProgress = 0;

	// Use this for initialization
	void Start () {
        currDialog = startDialog;

	}

	// Update is called once per frame
	void Update () {
	
	}

	public void KillAFish()
	{
		Debug.Log("KILL FISH");
		numFish--;
		if (numFish == 0)
		{
			backgrounds[2].SetActive(false);
			Destroy(this.gameObject);
		}
		else if (numFish < 2)
		{
			backgrounds[1].SetActive(false);
			backgrounds[2].SetActive(true);
			currDialog = endDialog;
			dialogProgress = 0;
		}
		else if (numFish < 4)
		{
			backgrounds[0].SetActive(false);
			backgrounds[1].SetActive(true);
			currDialog = midDialog;
			dialogProgress = 0;
		}
	}

	public override void showDialog()
	{
		Debug.Log ("AAA");
        if (image != null)
            dialog.SetImage(image);
        dialog.SetText(currDialog[dialogProgress]);
		if (dialogProgress < currDialog.Length - 1) {
			dialogProgress++;
		}

        dialog.gameObject.SetActive(true);
	}

}
