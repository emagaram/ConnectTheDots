using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LineDraw: MonoBehaviour
{
    private LineRenderer line;
    private Vector3 linePos;
    public Material material;
    public Material materialDisable;
    public AudioSource connect;
    public Game game;

    private int currLines = 0;
    public static Dot dotTouching;
    public static Dot dotStarted;
    public static bool startedDrawing = false;
    public static bool stoppedDrawing = false;
    public string nextScene;


    List<GameObject> lines;
    private void Start()
    {
        lines = new List<GameObject>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            dotStarted = null;
            dotTouching = null;
            startedDrawing = false;
            stoppedDrawing = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        }
        //Start and stop drawing checks
        if (Input.GetMouseButtonDown(0) && startedDrawing)
        {
            stoppedDrawing = true;
            
            if(line) line.gameObject.SetActive(false);
            if (game.Evaluate())
            {
                dotStarted = null;
                dotTouching = null;
                startedDrawing = false;
                stoppedDrawing = false;
                SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            }
            else
            {
                for(int i=0; i < lines.Count; i++)
                {
                    Destroy(lines[i].gameObject);
                    stoppedDrawing = false;
                    startedDrawing = false;
                    dotStarted = null;
                    dotTouching = null;
                    startedDrawing = false;
                    stoppedDrawing = false;
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && dotTouching && !startedDrawing)
        {
            startedDrawing = true;
        }


        //Starting a new line on a dot
        if(startedDrawing && !stoppedDrawing && dotTouching && dotStarted == null && !line) { 
            createLine();
            dotStarted = dotTouching;
            dotStarted.connected = true;
            linePos = dotTouching.transform.position;
            linePos.z = 1;
            line.GetComponent<Line>().start = dotStarted;
            line.SetPosition(0, linePos);
            line.SetPosition(1, linePos);
            line.gameObject.transform.position = new Vector3(line.gameObject.transform.position.x, line.gameObject.transform.position.y, 1);
        }

        

        //Stopping a new line on a dot
        else if (startedDrawing && !stoppedDrawing && dotTouching && dotStarted != dotTouching && line && (!dotTouching.connected || dotTouching is SquareDot)&& line.GetComponent<Line>().valid)
        {
            dotTouching.connected = true;
            linePos = dotTouching.transform.position;
            linePos.z = 1;
            line.SetPosition(1, linePos);
            updateLineCollider(line.gameObject);
            line.GetComponent<Line>().end = dotTouching;
            connect.Play();
            lines.Add(line.gameObject);
            line = null;
            currLines++;
            dotStarted = null;
        }

        //moving line
        else if (line)
        {
            linePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            linePos.z = 0;
            line.SetPosition(1, linePos);
            updateLineCollider(line.gameObject);
            line.GetComponent<Line>().Check();
            if (line.GetComponent<Line>().valid)
            {
                line.material = material;
            }
            else
            {
                line.material = materialDisable;

            }

            if (stoppedDrawing)
            {
                line.gameObject.SetActive(false);
            }
        }
    }

    void createLine()
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.gameObject.AddComponent<EdgeCollider2D>();
        line.gameObject.AddComponent<Line>();
        line.GetComponent<EdgeCollider2D>().edgeRadius = 0.17f;
        //line.GetComponent<EdgeCollider2D>().isTrigger = true;
        line.tag = "Line";
        line.material = material;
        line.positionCount = 2;
        line.startWidth = 0.25f;
        line.endWidth = 0.25f;
        line.useWorldSpace = false;
        line.numCapVertices = 50;
    }

    void updateLineCollider(GameObject line)
    {
        EdgeCollider2D ec = line.GetComponent<EdgeCollider2D>();
        LineRenderer lr = line.GetComponent<LineRenderer>();
        List<Vector2> points = new List<Vector2>();
        points.Add(lr.GetPosition(0));
        points.Add(lr.GetPosition(1));
        ec.SetPoints(points);
    }
}