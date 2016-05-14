using UnityEngine;

    public class PIDController : MonoBehaviour {
        /**
     * FileName: PIDController.cs
     * Author: Michael Gonzalez
     * Date Drafted: 2/9/2016
     * Description: A Generic proportional–integral–derivative controller 
     *              We can use this as a an alternative to lerping when 
     *              the moving object may collide with another object
     * Underlying Phyics: https://en.wikipedia.org/wiki/PID_controller
     */

        public enum TrackingType { Transform, Vector }

        public float kP = 1.0f;
        public float kI = 1.0f;
        public float kD = 1.0f;

        public float maximumImpulse = 1.0f;
        public float minimumImpulse = 0f;
        public float maxVelocity = 100000f;
        public float velocityDeviation = 0f;

        public float impuseRate;

        public TrackingType trackingType = TrackingType.Transform;
        public Transform destinationTransform;
        public Vector3 destinationVector;

        // P term variables
        private Vector3 previousError;
        private Vector3 currError;

        // I term variables
        private Vector3 integral = Vector3.zero;

        // D term variables
        private Vector3 derivative = Vector3.zero;
        private float inverseDeltaTime;

        private float lastUpdate;

        private Rigidbody rb3D;
        private Rigidbody2D rb2D;
        private bool is2DObj;
        private float velocityLB;
        private float velocityUB;

        void Start () {
            lastUpdate = Time.time;
            inverseDeltaTime = 1.0f / impuseRate;
            SetupRigidBody();
            velocityLB = (1.0f-velocityDeviation) * maxVelocity;
            velocityUB = (1.0f+velocityDeviation) * maxVelocity;
        }
	
        void Update () {
            if (rb2D.velocity.magnitude > maxVelocity)
                rb2D.velocity = Random.Range(velocityLB, velocityUB ) * rb2D.velocity.normalized;
            if(Time.time - lastUpdate > impuseRate)
            {
                Vector3 impulseVect = CalculatePID();
                ApplyImpulse(impulseVect);
                lastUpdate = Time.time;
            }
        }

        void OnCollisionEnter2D(Collision2D c)
        {
            integral = Vector3.zero;
        }
        void OnTriggerEnter2D(Collider2D c)
        {
            integral = Vector3.zero;
        }
        void OnCollisionEnter(Collision c)
        {
            integral = Vector3.zero;
        }
        void OnTriggerEnter(Collider c)
        {
            integral = Vector3.zero;
        }

        void ApplyImpulse( Vector3 impulse )
        {
            Vector3 clampedVector = ClampVector3(impulse, minimumImpulse, maximumImpulse);
            if (is2DObj)
                rb2D.AddForce(clampedVector, ForceMode2D.Impulse);
            else
                rb3D.AddForce(clampedVector, ForceMode.Impulse);
        }

        Vector3 CalculatePID()
        {
            CalculateError();
            CalculateDerivative();
            Vector3 p = kP * currError;
            Vector3 i = kI * integral;
            Vector3 d = kD * derivative;
            return p + i + d;

        }

        void SetupRigidBody()
        {
            rb2D = GetComponent<Rigidbody2D>();
            rb3D = GetComponent<Rigidbody>();
            if (rb2D != null)
                is2DObj = true;
            else if (rb3D != null)
                is2DObj = false;
            else
                Debug.LogError("No rigid body found!");
        }

        public static Vector3 ClampVector3( Vector3 v, float minMagnitude, float maxMagnitude )
        {
            Vector3 res = v;
            res = res.magnitude > maxMagnitude ? maxMagnitude * v.normalized : res; 
            res = res.magnitude < minMagnitude ? minMagnitude * v.normalized : res; 
            return res;
        }
        void CalculateError()
        {
            Vector3 dest = destinationVector;
            switch(trackingType)
            {
                case TrackingType.Transform:
                    dest = destinationTransform.position;
                    break;
                case TrackingType.Vector:
                    dest = destinationVector;
                    break;
            }
            currError = dest - transform.position;
        }

        void CalculateDerivative()
        {
            derivative = inverseDeltaTime * (currError - previousError);
            previousError = currError;
        }

        void CalculateIntegral()
        {
            integral += inverseDeltaTime * currError;
        }
    }
