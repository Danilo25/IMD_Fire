using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float kickRange = 3f;
    public float kickStrength = 1f;
    public float sitRange = 2f;

    private bool sitDown = false;
    private Vector3 beforeSittingPos;

    private MyUtils myUtils;
    private Animator anim;
    private Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        myUtils = GetComponent<MyUtils>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kicking
        if (Input.GetKeyDown(KeyCode.R))
        {
            Kick(GetNearestInteractable(), kickStrength);
        }

        // Sitting
        if (Input.GetKeyDown(KeyCode.F))
        {
            SitInChair(GetNearestInteractable("Chair").transform.position);
        }
    }

    #region SittingDown / Chair Interactions
    // Toggles between sitting and getting up a chair
    void SitInChair(Vector3 chair)
    {
        anim.enabled = true;

        if (sitDown) // getting up
        {
            anim.SetBool("SittingDown", !sitDown);
            anim.Play("GettingUp");
        }
        else { // sitting down

            Debug.Log(transform.position);
            Debug.Log(chair);

            // Setting the player to a 'sitting down' state
            coll.isTrigger = true; // in order to not collide with the chair
            beforeSittingPos = transform.position; // saving the position of the player before sitting down
            transform.position = chair; // teleporting the player to the chair
            UpdateParentPos(); // in order to play the animation using the player's relative position

            anim.SetBool("SittingDown", !sitDown);
            anim.Play("SittingDown");
        }

        sitDown = !sitDown;
    }

    // Reverts player to a 'not sitting down' state
    // To be called when the 'GettingUp' animation ends
    void getUpFromChair()
    {
        anim.enabled = false;
        coll.isTrigger = false;

        transform.position = beforeSittingPos;
        UpdateParentPos();
    }
    #endregion

    void Kick(GameObject targetObj, float strength = 1f)
    {
        // TODO Play Animation

        Vector3 dir = (targetObj.transform.position - transform.position).normalized;
        targetObj.gameObject.GetComponent<I_Interactable>().Kick(dir, strength);
    }

    // General Utility Functions

    void UpdateParentPos()
    {
        transform.parent.position += transform.localPosition;
        transform.localPosition = Vector3.zero;
    }

    GameObject GetNearestInteractable(string filter = "")
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, kickRange, 
        LayerMask.GetMask("Interactable"));

        return myUtils.GetNearestInteractable(myUtils.ToGameObjectArray(hitColliders), filter);
    }
}
