using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class symbolRecognition : MonoBehaviour
{
    public Vector3[] SymplifyPoints(Vector3[] allPoints)
    {
        //compare angles direction with three points and and remove points step by step
        // maybe do a pos check first to go faster (remove all overlapping points or points too close to each other)

        return allPoints;
    }

    public void RecognitionSystem(Vector3[] allPoints)
    {
        //use distance and angle to compare shape
        //compare one by one the points with the points of the database and narrow it down

        //need to do a function to record symbol
    }
}
