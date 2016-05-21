using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    [Header("For AnhQuan's Original Script")]
	public Sprite sprite;
	public bool isVertical;
	public int numTiles;
    [Header("Corners")]
	public Sprite topLeftCorner;
	public Sprite topRightCorner;
	public Sprite bottomLeftCorner;
	public Sprite bottomRightCorner;
    [Header("Edges")]
    public Sprite topSprite;
    public Sprite bottomSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    [Header("Middle")]
    public Sprite middleSprite;

    public int width = 2;
    public int height = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
