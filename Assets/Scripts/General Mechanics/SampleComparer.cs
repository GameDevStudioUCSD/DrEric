using System;
using System.Collections.Generic;
using UnityEngine;

class SampleComparer : Comparer<PositionSample>
{
    Vector2 goal;
    public SampleComparer(Vector2 goal)
    {
        this.goal = goal;
    }
    public override int Compare(PositionSample x, PositionSample y)
    {
        return (x.position - goal).magnitude < (y.position - goal).magnitude ? 1 : -1;
    }
}
