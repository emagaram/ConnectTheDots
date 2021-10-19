using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LineDraw: MonoBehaviour
{
    private LineRenderer line;
    private Vector3 linePos;
    public Material material;
    private int currLines = 0;
    public static int dotTouching = -1;
    public static int dotStarted = -1;
    public static Vector3 dotPos;
    public static bool startedDrawing = false;
    public static bool stoppedDrawing = false;


    void Update()
    {
        //Start and stop drawing checks
        if(Input.GetMouseButtonDown(0) && startedDrawing)
        {
            stoppedDrawing = true;
        }

        if (Input.GetMouseButtonDown(0) && dotTouching!=-1 && !startedDrawing)
        {
            startedDrawing = true;
        }


        //Starting a new line on a dot
        if(startedDrawing && !stoppedDrawing && dotTouching != -1 && dotStarted == -1 && !line) { 
            createLine();
            dotStarted = dotTouching;
            linePos = dotPos;
            linePos.z = 1;
            line.SetPosition(0, linePos);
            line.SetPosition(1, linePos);
            Debug.Log("Created Line");
        }

        

        //Stopping a new line on a dot
        else if (startedDrawing && !stoppedDrawing && dotTouching != -1 && dotStarted != dotTouching && line)
        {
            linePos = dotPos;
            linePos.z = 1;
            line.SetPosition(1, linePos);
            line = null;
            currLines++;
            dotStarted = -1;
            Debug.Log("Ending line");
        }

        //moving line
        else if (line)
        {
            linePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            linePos.z = 0;
            line.SetPosition(1, linePos);
        }
    }

    void createLine()
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.material = material;
        line.positionCount = 2;
        line.startWidth = 0.25f;
        line.endWidth = 0.25f;
        line.useWorldSpace = false;
        line.numCapVertices = 50;
    }
}