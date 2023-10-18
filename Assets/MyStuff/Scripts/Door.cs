using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, I_Interactable
{
    [SerializeField] float doorSpd = 0.5f;
    [SerializeField] float interactDis = 2.7f;
    public bool open = true;
    public bool broken = false;

    private Transform knob;
    private Transform player;
    private Animator anim;
    private DoorText textPrompt;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.speed = doorSpd;
        knob = transform.Find("Knob").gameObject.transform;
        player = GameObject.FindWithTag("Player").gameObject.transform;
        textPrompt = transform.Find("TextPrompt").gameObject.GetComponent<DoorText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            // distance from the player to the knob
            float dis = Vector3.Distance(player.position, knob.position);
            bool closeToKnob = dis <= interactDis;

            // Getting in wich side of the door the player is
            Vector3 toPlayer = transform.position - player.position;
            Vector3 toKnob = transform.position - knob.position;

            float doorSide = Vector3.Dot(Vector3.Cross(toPlayer, toKnob).normalized, Vector3.up);

            textPrompt.Show(closeToKnob, doorSide < 0);

            // Only opening the door when close to the knob
            if (closeToKnob && Input.GetKeyDown(KeyCode.E))
            {
                Interact(!open);
            }
        }
    }

    // Opens and closes the door
    void Interact(bool opened)
    {
        open = opened;
        anim.SetBool("Open", open);
        textPrompt.ChangePrompt(open);

        if (open)
        {
            anim.Play("DoorOpen");
        }
    }

    public void Kick(Vector3 dir, float strength = 1f)
    {
        // Making the door fall on the ground as a physics object
        if (!broken)
        {
            broken = true;
            anim.enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().isKinematic = false;
            textPrompt.HidePrompt();
        }

        // Getting the side of the player in relation to the door
        Vector3 toKnob = transform.position - knob.position;
        float doorSide = Vector3.Dot(Vector3.Cross(dir, toKnob).normalized, Vector3.up);

        // Applying the force
        float mag = 200f * strength * Mathf.Sign(doorSide);
        rb.AddForce(Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward * mag);
    }

    public bool hasTag(string tag)
    {
        List<string> tags = new List<string>{"", "Door"};
        return tags.Contains(tag);
    }
}
