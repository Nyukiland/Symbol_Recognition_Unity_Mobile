using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class AllSymbol
{
    public List<float> allAngles;

    public Material matToApply;

    public GameObject VFX;
}

public class SymbolRecognition : MonoBehaviour
{
    [Header ("Line Simplification setting (must be edited only at the beginning)")]
    [SerializeField]
    float angleDifferenceMax = 10;

    [SerializeField]
    float minimumDist = 0.05f;

    [Space(5)]
    [Header("recognition Property")]

    List<AllSymbol> symbolList;

    SymbolStorage DataBase;

    public List<Vector3> SymplifyPoints(List<Vector3> allPoints)
    {
        //compare angles direction with three points and and remove points step by step
        // maybe do a pos check first to go faster (remove all overlapping points or points too close to each other)

        List<Vector3> tempList = DistCheck(allPoints);
        tempList = AngleCheck(tempList);

        return tempList;
    }

    List<Vector3> DistCheck(List<Vector3> allPoints)
    {
        List<int> tempList = new();

        for (int i = 0; i < allPoints.Count - 1; i ++)
        {
            Debug.Log(Vector3.Distance(allPoints[i], allPoints[i + 1]) + " / dist");

            if (Vector3.Distance(allPoints[i], allPoints[i +1]) < minimumDist)
            {
                tempList.Add(i);
            }
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            allPoints.RemoveAt(tempList[i] - i);
        }

        if (tempList.Count > 0) return DistCheck(allPoints);

        return allPoints;
    }

    List<Vector3> AngleCheck(List<Vector3> allPoints)
    {

        List<int> tempList = new();

        for (int i = 0; i < allPoints.Count - 2; i += 2)
        {
            Vector3 firstDir = allPoints[i] - allPoints[i + 1];
            Vector3 SecondDir = allPoints[i] - allPoints[i + 2];

            Debug.Log(Vector3.Angle(firstDir, Vector3.up) - Vector3.Angle(SecondDir, Vector3.up) + " / angle");

            if (Vector3.Angle(firstDir, SecondDir) < angleDifferenceMax)
            {
                tempList.Add(i+1);
            }
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            allPoints.RemoveAt(tempList[i] - i);
        }

        if (tempList.Count > 0) return AngleCheck(allPoints);

        return allPoints;
    }

    public void SaveProperty(List<Vector3> points)
    {
        AllSymbol newEntry = new();

        Vector3 baseDir = points.Last() - points.First();

        for(int i = 0; i < points.Count - 1; i++)
        {
            Vector3 currentDir = points[i + 1] - points[i];
            newEntry.allAngles.Add(Vector3.Angle(currentDir, baseDir));
        }

        symbolList.Add(newEntry);
    }

    public void RecognitionSystem(Vector3[] allPoints)
    {
        //use distance and angle to compare shape
        //compare one by one the points with the points of the database and narrow it down

        //need to do a function to record symbol
    }

    private void OnApplicationQuit()
    {
        DataBase.symbolDataBase.Clear();

        for (int i = 0; i < symbolList.Count; i++)
        {
            DataBase.symbolDataBase.Add(new());
            DataBase.symbolDataBase[i].allAngles = symbolList[i].allAngles;
            DataBase.symbolDataBase[i].matToApply = symbolList[i].matToApply;
            DataBase.symbolDataBase[i].VFX = symbolList[i].VFX;
        }
    }
}
