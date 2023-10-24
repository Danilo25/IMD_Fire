using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private SpawnController spwnControl;

    // Start is called before the first frame update
    void Start()
    {
        myUtils = GetComponent<MyUtils>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        spwnControl = GameObject.FindWithTag("SpawnController").gameObject.GetComponent<SpawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kicking
        if (Input.GetKeyDown(KeyCode.R) && !sitDown)
        {
            Kick(GetNearestInteractable(), kickStrength);
        }

        // Sitting
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameOver("AAAAAAAAA");

            SitInChair(GetNearestInteractable("Chair"));
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.position = spwnControl.NormalSpawn();
        }
    }

    #region SittingDown / Chair Interactions
    // Toggles between sitting and getting up a chair
    void SitInChair(GameObject chair)
    {
        if (chair.GetComponent<Chair>().IsSittable)
        {
            Vector3 chairPos = chair.transform.position;
            anim.enabled = true;

            if (sitDown) // getting up
            {
                anim.SetBool("SittingDown", !sitDown);
                anim.Play("GettingUp");
            }
            else { // sitting down

                // Setting the player to a 'sitting down' state
                coll.isTrigger = true; // in order to not collide with the chair
                beforeSittingPos = transform.position; // saving the position of the player before sitting down
                transform.position = chairPos; // teleporting the player to the chair
                UpdateParentPos(); // in order to play the animation using the player's relative position

                anim.SetBool("SittingDown", !sitDown);
                anim.Play("SittingDown");
            }

            sitDown = !sitDown;
        }
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

    // Game Restart
    void GameOver(string reason)
    {
        PlayerPrefs.SetString("DeathReason", reason);
        SceneManager.LoadScene("GameOver");
    }
}
