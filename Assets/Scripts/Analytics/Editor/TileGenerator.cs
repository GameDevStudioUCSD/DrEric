using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileGenerator : MonoBehaviour {


	[MenuItem("CONTEXT/Wall/GenerateTiles")]
	static void GenerateTiles() {
        DeleteTiles();
		Transform t = Selection.activeTransform;
		Wall wall = t.gameObject.GetComponent<Wall>();
		bool isVertical = wall.isVertical;
		int i = wall.numTiles;
        // The sprite gets applied to the wall
		Sprite sprite = wall.sprite;
		Vector2 spriteSize = sprite.bounds.size;
        // Create the box collider
		t.gameObject.AddComponent<BoxCollider2D>();
        // Set the box collider's size
		t.gameObject.GetComponent<BoxCollider2D> ().size
			= isVertical? new Vector2(spriteSize.x,spriteSize.y*i):new Vector2(spriteSize.x*i,spriteSize.y);
        // Magical offset settings
		t.gameObject.GetComponent<BoxCollider2D> ().offset
			= isVertical? new Vector2(0,(float)spriteSize.y*i/2+0.5f):new Vector2((float)spriteSize.x*i/2+0.5f,0);

		for(;i > 0; i--)
		{
			GameObject go = new GameObject("Tile" + i);
			go.AddComponent<SpriteRenderer>();
			go.GetComponent<SpriteRenderer>().sprite = sprite;
			go.transform.parent = t;
			go.transform.localPosition = isVertical? new Vector2(0,i*1.0f):new Vector2(i*1.0f,0);
		}
	}


	[MenuItem("CONTEXT/Wall/GenerateTiledSection")]
    static void GenerateTiledSection()
    {
        // Delete any existing tiles
        DeleteTiles();
        // Grab refrences to our transform and the wall attached
		Transform t = Selection.activeTransform;
		Wall wall = t.gameObject.GetComponent<Wall>();
        // Declare local variables for height and width
        int w = wall.width-1;
        int h = wall.height - 1;
        // Error handling - This function shouldn't handle 1 x n or n x 1 sections
        if( w < 1 || h < 1) {
            Debug.LogError("You're trying to make a tiled section too small for this function to handle! Try another method");
            return;
        }
        // Sprite scale modifier 
        float size = wall.topLeftCorner.bounds.size.y;
        // Create the box collider
		BoxCollider2D collider = t.gameObject.AddComponent<BoxCollider2D>();
        // Set the box collider's size
		collider.size =  new Vector2(size*(w+1),size*(h+1));
        // Set the collider's offset
        collider.offset = 0.5f * (collider.size - new Vector2(size, size));
        // Set corners
        MakeGameObject(wall.topLeftCorner, 0, h*size, "TopLeftCorner", t);
        MakeGameObject(wall.topRightCorner, w*size, h*size, "TopRightCorner", t);
        MakeGameObject(wall.bottomLeftCorner, 0, 0, "BottomLeftCorner", t);
        MakeGameObject(wall.bottomRightCorner, w*size, 0, "BottomRightCorner", t);
        // Make the horzontal tiling
        for (int x = 1; x < w; x++)
        {
            MakeGameObject(wall.topSprite, x * size, h * size, "Top Tile " + x, t);
            MakeGameObject(wall.bottomSprite, x * size, 0, "Bottom Tile " + x, t);
        }
        // Make the vertical tiling
        for (int y = 1; y < h; y++)
        {
            MakeGameObject(wall.rightSprite, w * size, y * size, "Right Tile " + y, t);
            MakeGameObject(wall.leftSprite, 0, y*size, "Left Tile " + y, t);
        }
        // Make the center tiling
        for (int x = 1; x < w; x++)
            for (int y = 1; y < h; y++)
                MakeGameObject(wall.middleSprite, x * size, y * size, "Middle Tile " + x  + ", " + y, t);

    }

    static void MakeGameObject(Sprite sprite, float x, float y, string name, Transform parent)
    {
        GameObject obj = new GameObject(name);
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        obj.transform.parent = parent;
        obj.transform.localPosition = new Vector2(x, y);
    }

	[MenuItem("CONTEXT/Wall/DeleteTiles")]
	static void DeleteTiles() {
		Transform parent = Selection.activeTransform;
		int count = parent.childCount;
		for (; count > 0; count--)
		{
			Transform child = parent.GetChild(count-1);
			//if (child.name.Substring(0,4) == "Tile")
				DestroyImmediate(child.gameObject);
		}
		DestroyImmediate(parent.GetComponent<BoxCollider2D>());
	}
}
