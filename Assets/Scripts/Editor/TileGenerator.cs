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
		Vector2 spriteSize = sprite.bounds.size;
		t.gameObject.AddComponent<BoxCollider2D> ();
		t.gameObject.GetComponent<BoxCollider2D> ().size
			= isVertical? new Vector2(spriteSize.x,spriteSize.y*i):new Vector2(spriteSize.x*i,spriteSize.y);
		//t.gameObject.GetComponent<BoxCollider2D>().

		for(;i > 0; i--)
		{
			GameObject go = new GameObject("Tile" + i);
			go.AddComponent<SpriteRenderer>();
			go.GetComponent<SpriteRenderer>().sprite = sprite;
			go.transform.parent = t;
			go.transform.localPosition = isVertical? new Vector2(0,(i-2)*1.0f):new Vector2((i-2)*1.0f,0);
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
		DestroyImmediate(parent.GetComponent<BoxCollider2D>());
	}
}
