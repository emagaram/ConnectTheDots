using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public Dot[] dots;
    public Separator[] separators;
    // Start is called before the first frame update
    void Start()
    {
       dots = FindObjectsOfType(typeof(Dot)) as Dot[];
        separators= FindObjectsOfType(typeof(Separator)) as Separator[];
    }

    public bool Evaluate()
    {
        for(int i =0; i < separators.Length; i++)
        {
            if (!separators[i].Check())
            {
                Debug.Log("Separators failed");
                return false;
            }
        }
        for(int i =0; i< dots.Length; i++)
        {
            if (!dots[i].connected)
            {
                Debug.Log("Dots failed");

                return false;
            }
        }
        Debug.Log("LC");
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
