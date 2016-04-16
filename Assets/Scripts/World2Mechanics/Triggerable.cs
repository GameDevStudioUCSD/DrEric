using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Triggerable
/// 
/// Script component that has Trigger function that can be called by other object.
/// For example a button when pressed can call Trigger function in other object with this script.
/// 
/// TODO: Currently is not a true "interface" this needs to be refactored
/// 
/// </summary>

public abstract class Triggerable : MonoBehaviour {

    /*
     * Trigger
     * 
     * Access function for obstacle.  Button will have to know about this function
     * to cause the obstacle to lower/raise.
     */
    public abstract void Trigger();
}
