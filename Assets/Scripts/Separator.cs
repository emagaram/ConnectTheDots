using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separator : MonoBehaviour
{
    public bool Check()
    {
        List<Collider2D> results = new List<Collider2D>();
        Debug.Log(Physics2D.OverlapCollider(this.GetComponent<BoxCollider2D>(), new ContactFilter2D(), results));
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].GetComponent<Line>())
            {
                return true;
            }
        }
        return false;

    }
}
