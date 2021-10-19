using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public static int dotIDCounter = 0;
    public int ID;

    void Awake()
    {
        ID = dotIDCounter;
        dotIDCounter++;
    }

    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        LineDraw.dotTouching = ID;
        LineDraw.dotPos = transform.position;
        Debug.Log("DT: " + LineDraw.dotTouching);
        Debug.Log("DS: " + LineDraw.dotStarted);
        Debug.Log("SD: " + LineDraw.startedDrawing);
        Debug.Log("SpD: " + LineDraw.stoppedDrawing);


    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        LineDraw.dotTouching = -1;
    }
}
