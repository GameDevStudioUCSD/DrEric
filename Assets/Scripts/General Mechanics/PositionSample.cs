using System;
using System.Collections.Generic;
using UnityEngine;

class PositionSample
{
    public Vector2 position;
    public PositionSample parent, next;
    public PositionSample(Vector3 position, PositionSample parent)
    {
        this.position = position;
        this.parent = parent;
    }
    
}
