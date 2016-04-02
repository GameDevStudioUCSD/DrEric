using UnityEngine;
using System.Collections;

public class StageBallController : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    Transform target;
    private bool isRight;   // Object is moving to the right

    public void moveLeft(float torque)
    {
        rigidBody.AddTorque(torque, ForceMode2D.Force);
        isRight = false;
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void moveRight(float torque)
    {
        rigidBody.AddTorque(-torque, ForceMode2D.Force);
        isRight = true;
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void SetRight(bool right) {
        isRight = right;
    }


    public void SetTarget(Transform target) {
        this.target = target;
    }

    // Update is called once per frame
    void Update(){
        if (target != null){

            if ((isRight && transform.position.x >= target.position.x) || (!isRight && transform.position.x <= target.position.x))
            {
                target = null;
                rigidBody.velocity = Vector2.zero;
                rigidBody.angularVelocity = 0;
            }
        }
    }

    void Start(){
        rigidBody = transform.GetComponent<Rigidbody2D>();
    }

}