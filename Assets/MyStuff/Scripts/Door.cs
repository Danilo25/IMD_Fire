using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //[SerializeField] float ang = 0;
    //[SerializeField] float fullyOpenedAngle = -90f;
    [SerializeField] float doorSpd = 0.5f;
    public bool open = true;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        anim.speed = doorSpd;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Interact(!open);
        }
    }

    void Interact(bool opened)
    {
        open = opened;
        anim.SetBool("Open", open);

        if (open)
        {
            anim.Play("DoorOpen");
        }
    }
}
