using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, I_Interactable
{
    [SerializeField] float interactDis = 2.7f;
    public bool IsSittable = true;

    private Transform chair;
    private Transform player;
    private ChairText textPrompt;

    // Start is called before the first frame update
    void Start()
    {
        chair = transform;
        player = GameObject.FindWithTag("Player").gameObject.transform;
        textPrompt = transform.Find("TextPrompt").gameObject.GetComponent<ChairText>();
    }

    // Update is called once per frame
    void Update()
    {
        // distance from the player to the chair
        float dis = Vector3.Distance(player.position, chair.position);

        // Angle to the player
        Vector3 toPlayer = chair.position - player.position;
        float angToPlayer = Vector3.Angle(Vector3.forward, Vector3.Normalize(toPlayer));
        float side = -Vector3.Dot(Vector3.Cross(toPlayer, Vector3.forward).normalized, Vector3.up);

        // IsSittable
        float minSitAng = 45f;
        float xAng = chair.eulerAngles.x;
        float zAng = chair.eulerAngles.z;
        IsSittable = xAng > -minSitAng && xAng < minSitAng &&
                     zAng > -minSitAng && zAng < minSitAng;

        textPrompt.Show(dis <= interactDis && IsSittable, Mathf.Sign(side) * angToPlayer);
    }

    public void Kick(Vector3 dir, float strength = 1f)
    {
        float mag = 20f * strength;
        this.gameObject.GetComponent<Rigidbody>().AddForce(dir * mag);
    }

    public bool hasTag(string tag)
    {
        List<string> tags = new List<string>{"", "Chair"};
        return tags.Contains(tag);
    }
}
