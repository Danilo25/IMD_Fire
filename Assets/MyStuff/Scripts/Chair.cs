using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, I_Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kick(Vector3 dir, float strength = 1f)
    {
        float mag = 5f * strength;
        this.gameObject.GetComponent<Rigidbody>().AddForce(dir * mag);
    }
}
