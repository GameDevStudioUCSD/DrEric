using UnityEngine;
using System.Collections;

public class ChaosControl : MonoBehaviour {

	public void StopTime()
	{
		Time.timeScale = 0f;
	}

	public void StartTime()
	{
		Time.timeScale = 1f;
	}
}
