using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float doorSpd = 0.5f;
    [SerializeField] float interactDis = 2.7f;
    public bool open = true;

    private Transform knob;
    private Transform player;
    private Animator anim;
    private DoorText textPrompt;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        anim.speed = doorSpd;
        knob = transform.Find("Knob").gameObject.transform;
        player = GameObject.FindWithTag("Player").gameObject.transform;
        textPrompt = transform.Find("TextPrompt").gameObject.GetComponent<DoorText>();
    }

    // Update is called once per frame
    void Update()
    {
        // distance from the player to the knob
        float dis = Vector3.Distance(player.position, knob.position);
        bool closeToKnob = dis <= interactDis;

        textPrompt.show(closeToKnob);

        // Only opening the door when close to the knob
        if (closeToKnob && Input.GetKeyDown(KeyCode.E))
        {
            Interact(!open);
        }

    }

    // Opens and closes the door
    void Interact(bool opened)
    {
        open = opened;
        anim.SetBool("Open", open);
        textPrompt.changePrompt(open);

        if (open)
        {
            anim.Play("DoorOpen");
        }
    }
}
