using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public GameObject firePointObj;
    public float spreadRange = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Spread();
        }
    }

    void Spread()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Flammable");

        foreach(GameObject obj in objs)
        {
            if (Vector3.Distance(obj.transform.position, transform.position) <= spreadRange)
            {
                if (obj.transform.Find("FirePoint") == null)
                {
                    var fireObj = Instantiate(firePointObj, obj.transform);
                    fireObj.transform.localPosition = Vector3.zero;
                }
            }
        }
    }
}
