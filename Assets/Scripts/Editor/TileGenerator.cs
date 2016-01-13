using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileGenerator : MonoBehaviour {


	[MenuItem("CONTEXT/Wall/GenerateTiles")]
	static void GenerateTiles() {
        DeleteTiles();
		Transform t = Selection.activeTransform;
		Wall wall = t.gameObject.GetComponent<Wall> ();
		bool isVertical = wall.isVertical;
		int i = wall.numTiles;
		Sprite sprite = wall.sprite;
		for(;i > 0; i--)
		{
			GameObject go = new GameObject("Tile" + i);
			go.AddComponent<SpriteRenderer>();
			go.AddComponent<BoxCollider2D>();
			go.GetComponent<SpriteRenderer>().sprite = sprite;
			go.GetComponent<BoxCollider2D>().size = go.GetComponent<SpriteRenderer>().bounds.size;
			go.transform.parent = t;
			go.transform.localPosition = isVertical? new Vector2(0,i*1.0f):new Vector2(i*1.0f,0);
		}
	}

	[MenuItem("CONTEXT/Wall/DeleteTiles")]
	static void DeleteTiles() {
		Transform parent = Selection.activeTransform;
		int count = parent.childCount;
		for (; count > 0; count--)
		{
			Transform child = parent.GetChild(count-1);
			if (child.name.Substring(0,4) == "Tile")
				DestroyImmediate(child.gameObject);
		}
	}
}
