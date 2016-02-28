using UnityEngine;
using System.Collections;

public class Facing : MonoBehaviour
{

    public GameObject target;
    public float turnspeed;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnspeed);
        if (vectorToTarget.x < 0 && transform.localScale.y > 0 || vectorToTarget.x > 0 && transform.localScale.y < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -1 * transform.localScale.y, transform.localScale.z);
        }
    }
}