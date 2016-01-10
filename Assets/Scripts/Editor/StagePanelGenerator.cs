using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class StagePanelGenerator : MonoBehaviour {

	static int panelSpacing = 550;

	[MenuItem("CONTEXT/StageDetails/GeneratePanels")]
	static void GeneratePanels() {
		Transform t = Selection.activeTransform;
		StageDetails stageDetails = t.gameObject.GetComponent<StageDetails> ();
		int i = stageDetails.numberOfStage;
		GameObject prefab = stageDetails.panelPrefab;
		for(;i > 0; i--)
		{
			GameObject stagePanel = Instantiate (prefab);
			stagePanel.transform.localPosition = new Vector2(i*panelSpacing,0);
			stagePanel.transform.parent = t;

			// Set the name of the stage
			GameObject nameTransform = GameObject.FindGameObjectWithTag("StageName");
			Text stageName = nameTransform.GetComponent<Text> ();
			stageName.text = stageDetails.stageName[i];

			// Set the score of the stage
			GameObject scoreTransform = GameObject.FindGameObjectWithTag("StageScore");
			Text scoreValue = scoreTransform.GetComponent<Text> ();
			scoreValue.text = ""+stageDetails.stageScore[i];
		}
	}

	[MenuItem("CONTEXT/StageDetails/DeletePanels")]
	static void DeletePanels() {
		Transform parent = Selection.activeTransform;
		int count = parent.childCount;
		for (; count > 0; count--)
		{
			Transform child = parent.GetChild(count-1);
			if (child.name.Substring(0,5) == "Stage")
				DestroyImmediate(child.gameObject);
		}
	}
}
