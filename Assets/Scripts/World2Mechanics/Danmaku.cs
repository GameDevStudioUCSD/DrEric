using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class Danmaku : MonoBehaviour {

        public int numberOfBullets = 2;
        public float distanceFromCenter = 5f;
        public GameObject bullet;
        public float circleTime = 2;
        public float returnTime = 1;
        public float maxDistance = 1000;


        public enum State { Circling, Returning }
        public State state = State.Circling;
        List<PIDController> pidControllers;
        List<GameObject> bulletInstances;
        float stateChangeTime = 0f;
        float waitTime;

        void Start() {
            waitTime = circleTime;
            stateChangeTime = Time.time;
            Spawn();
        }
	
        void Update () {
            if (bulletInstances[0] == null)
                Spawn();
            if (Time.time - stateChangeTime > waitTime)
            {
                DetermineTrackingType();
                stateChangeTime = Time.time;
                if(bulletInstances[0].GetComponent<Transform>().position.magnitude > maxDistance)
                {
                    foreach (GameObject bullet in bulletInstances)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        bullet.GetComponent<Transform>().position = transform.position;
                    }
                }
            }
        }
        void DetermineTrackingType()
        {
            switch (state)
            {
                case State.Circling:
                    state = State.Returning;
                    waitTime = returnTime;
                    foreach (PIDController pid in pidControllers)
                        pid.trackingType = PIDController.TrackingType.Vector;
                    break;
                case State.Returning:
                    state = State.Circling;
                    waitTime = circleTime;
                    foreach (PIDController pid in pidControllers)
                        pid.trackingType = PIDController.TrackingType.Transform;
                    break;
            }
        }

        void Spawn()
        {
            pidControllers = new List<PIDController>();
            Vector2 defaultPlacementVector = distanceFromCenter * Vector2.up;
            bulletInstances = new List<GameObject>();
            for (int i = 0; i < numberOfBullets; i++)
            {
                Vector3 placementVector = VectorFunctions.RotateVector(defaultPlacementVector, i * 360 / numberOfBullets);
                placementVector += transform.position;
                bulletInstances.Add((GameObject)GameObject.Instantiate(bullet, transform.position, Quaternion.identity));
                PIDController pid = bulletInstances[i].GetComponent<PIDController>();
                pidControllers.Add(pid);
                pid.destinationVector = placementVector;
            }
            for (int i = 0; i < numberOfBullets; i++)
            {
                PIDController pid = bulletInstances[i].GetComponent<PIDController>();
                if (i == numberOfBullets - 1)
                    pid.destinationTransform = bulletInstances[0].GetComponent<Transform>();
                else
                    pid.destinationTransform = bulletInstances[i + 1].GetComponent<Transform>();
            }
            DetermineTrackingType();
        }


        public void Deactive()
        {
            this.enabled = false;
            foreach (GameObject bullet in bulletInstances)
                GameObject.Destroy(bullet);
        }
    }
}
