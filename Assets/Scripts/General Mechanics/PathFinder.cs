using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {


    public float rayDistance = 1;
    public int maxNumberOfRandomSamples = 10;
    public Vector2 direction = new Vector2(1, 1);
    public Vector2 goal;
    PriorityQueue<PositionSample> pq;
    Queue<PositionSample> queue;
    void Start()
    {
        pq = new PriorityQueue<PositionSample>( new SampleComparer(goal) );
        pq.push(new PositionSample(transform.position, null));
        //queue = new Queue<PositionSample>();
        //queue.Enqueue(new PositionSample(transform.position, null));
    }

    void Update () {
        PositionSample topSample = pq.pop();
        //PositionSample topSample = queue.Dequeue();
        for(int i = 0; i < maxNumberOfRandomSamples; i++)
        {
            CreateRandomSample(topSample);
        }
    }

    void CreateRandomSample(PositionSample origin)
    {
        // Declare a random vector to place our sample
        Vector2 direction = RandomVector(); 
        RaycastHit2D[] hit = new RaycastHit2D[1];
        for( int timeOut = 0; hit.Length != 0; timeOut++)
        {
            direction = rayDistance * RandomVector();
            hit = Physics2D.RaycastAll(origin.position, direction, rayDistance);
            if (timeOut > maxNumberOfRandomSamples) return;
        }
        PositionSample sample = new PositionSample(direction + origin.position, origin);
        pq.push(sample);
        //queue.Enqueue(sample);
        for(PositionSample s = sample;  s.parent != null; s = s.parent)
        {
            Debug.Log(s.position);
            Debug.DrawRay(s.parent.position, ( s.position - s.parent.position) );
        }
    }

    Vector2 RandomVector()
    {
        return new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
    }
}
