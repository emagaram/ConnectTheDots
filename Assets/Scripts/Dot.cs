using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public bool connected = false;
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        LineDraw.dotTouching = this;
        Debug.Log("SD: " + LineDraw.startedDrawing);
        Debug.Log("SpD: " + LineDraw.stoppedDrawing);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        LineDraw.dotTouching = null;
    }
}
