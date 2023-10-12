using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtils : MonoBehaviour
{

    // Returns the nearest element from 'objs' to the calling object
    public GameObject GetNearest(GameObject[] objs)
    {
        GameObject nearest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject obj in objs)
        {
            float dist = Vector3.Distance(obj.transform.position, currentPos);
            if (dist < minDist)
            {
                nearest = obj;
                minDist = dist;
            }
        }

        return nearest;
    }

    // Returns the nearest element from 'objs' to the calling object
    public GameObject GetNearestInteractable(GameObject[] objs, string filter)
    {
        GameObject nearest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject obj in objs)
        {
            if (obj.gameObject.GetComponent<I_Interactable>().hasTag(filter))
            {
                float dist = Vector3.Distance(obj.transform.position, currentPos);
                if (dist < minDist)
                {
                    nearest = obj;
                    minDist = dist;
                }
            }
        }

        return nearest;
    }

    // Converts a Collider array to a GameObject array
    public GameObject[] ToGameObjectArray(Collider[] objs)
    {
        GameObject[] array = new GameObject[objs.Length];

        for (int i = 0;i < objs.Length;i ++)
        {
            array[i] = objs[i].gameObject;
        }

        return array;

    }
}
