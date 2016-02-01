using UnityEngine;
using System.Collections;

public class ParticleRandoColor : MonoBehaviour
{

	ParticleSystem particleAni;

	void Start()
	{
	}
	void Update()
	{
		particleAni = GetComponent<ParticleSystem>();
		/*A lighter range of 100% A E S T H E T I C  colors */
		particleAni.startColor = new Color (Random.Range (0.5f, 1.0f), Random.Range (0.5f, 1.0f), Random.Range (0.5f, 1.0f));		
	}
}