using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Dot start;
    public Dot end;
    public bool valid = true;


    public void Check()
    {
        List<Collider2D> results = new List<Collider2D>();
        Debug.Log(Physics2D.OverlapCollider(this.GetComponent<EdgeCollider2D>(), new ContactFilter2D(), results));
        bool prevent = false;
        for(int i = 0; i < results.Count; i++)
        {
            if (results[i].GetComponent<SquareDot>())
            {
                prevent = false;
                valid = true;
                break;
            }
            if (results[i].GetComponent<Line>())
            {
                Line otherLine = results[i].GetComponent<Line>();
                //If we share no dots in common
                if (!(otherLine.start == start || otherLine.start == end || otherLine.end == start || otherLine.end == end))
                {
                    prevent = true;
                }
            }
        }
        if (prevent)
        {
            valid = false;
        }
        else
        {
            valid = true;
        }
        
    }

}
