using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : MonoBehaviour
{
    LineRenderer line;
    List<Vector2> linePos;

    void Start()
    {
        line = new GameObject().AddComponent<LineRenderer>();
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
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Ended:
                    break;

            }
        }
    }

    void StartRecord()
    {
        //line.
    }
}
