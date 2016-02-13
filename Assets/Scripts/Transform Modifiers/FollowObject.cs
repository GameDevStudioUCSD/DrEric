using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    public GameObject followTarget = null;
    public float movementSpeed = 10; //0 for instant

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (followTarget != null)
        {
            Follow(followTarget.transform.position);
        }
    }

    /*
     * Constant movement toward followTarget. MoveTowards is similar to linear interpolation, but uses a constant speed.
     */
    private void Follow(Vector2 destination)
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);

        if (movementSpeed == 0)
        {
            transform.position = destination;
        }
        else
        {
            transform.position = Vector2.MoveTowards(position,
                                                 destination,
                                                 movementSpeed * Time.deltaTime);
        }
    }
}
