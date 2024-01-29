using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrawSystem : MonoBehaviour
{
    [SerializeField]
    Vector3 lineOffset = new Vector3(0, 0, 1);

    LineRenderer line;
    List<Vector3> linePos = new();

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartRecord(Camera.main.ScreenToWorldPoint(touch.position) + lineOffset);
                    break;
                case TouchPhase.Moved:
                    ContinueRecord(Camera.main.ScreenToWorldPoint(touch.position) + lineOffset);
                    break;
                case TouchPhase.Ended:
                    StartCoroutine(EndRecordChecking(linePos));
                    break;

            }
        }
    }

    void StartRecord(Vector3 pos)
    {
        StopAllCoroutines();

        line.positionCount = 0;
        linePos.Clear();
        linePos.Add(pos);
    }

    void ContinueRecord(Vector3 pos)
    {
        linePos.Add(pos);

        line.positionCount = linePos.Count;
        line.SetPositions(linePos.ToArray());
    }

    IEnumerator EndRecordChecking(List<Vector3> allPos)
    {
        int pointCount = allPos.Count;

        List<Vector3> temp = new(allPos);
        temp = GetComponent<SymbolRecognition>().SymplifyPoints(allPos);

        Debug.Log(temp.Count + "/" + pointCount);

        while (pointCount == temp.Count) yield return null;

        line.positionCount = temp.Count;
        line.SetPositions(temp.ToArray());

        //GetComponent<symbolRecognition>().RecognitionSystem(temp);

    }
}
