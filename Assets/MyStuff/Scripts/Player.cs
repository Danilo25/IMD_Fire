using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float kickRange = 3f;
    public float kickStrength = 1f;
    MyUtils myUtils;

    // Start is called before the first frame update
    void Start()
    {
        myUtils = GetComponent<MyUtils>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kicking
        if (Input.GetKeyDown(KeyCode.R))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, kickRange, LayerMask.GetMask("Interactable"));
            Kick(myUtils.GetNearest(myUtils.ToGameObjectArray(hitColliders)), kickStrength);
        }
    }

    void Kick(GameObject targetObj, float strength = 1f)
    {
        // TODO Play Animation

        Vector3 dir = (targetObj.transform.position - transform.position).normalized;
        targetObj.gameObject.GetComponent<I_Interactable>().Kick(dir, strength);
    }
}
