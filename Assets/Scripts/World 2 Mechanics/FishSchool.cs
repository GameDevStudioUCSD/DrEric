using UnityEngine;
using System.Collections;

public class FishSchool : MonoBehaviour {

	public int numFish;
	public GameObject barrier;
	public GameObject[] backgrounds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void KillAFish()
	{
		numFish--;
		if (numFish == 0)
		{
			backgrounds[2].SetActive(false);
			Destroy(barrier);
		}
		else if (numFish < 2)
		{
			backgrounds[1].SetActive(false);
			backgrounds[2].SetActive(true);
		}
		else if (numFish < 4)
		{
			backgrounds[0].SetActive(false);
			backgrounds[1].SetActive(true);
		}
	}
}
