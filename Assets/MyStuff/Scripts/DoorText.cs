using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorText : MonoBehaviour
{
    TextMeshPro text;
    [SerializeField] string openText = "Abrir (E)";
    [SerializeField] string closeText = "Fechar (E)";
    public string currentText;
    private float initialZPos;

    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponent<TextMeshPro>();
        currentText = openText;
        initialZPos = transform.localPosition.z;
    }

    // Changes the prompt of the door between 'openText' and 'closeText'
    public void changePrompt(bool open)
    {
        currentText = open ? closeText : openText;
        text.text = currentText;
    }

    // Toggles the visibility of the prompt
    public void show(bool near, bool doorSide)
    {
        text.text = near ? currentText : "";

        float yAng = transform.parent.transform.eulerAngles.y + (doorSide ? -180 : 0);
        float zPos = initialZPos * (doorSide ? 1 : -1);

        transform.localPosition = new Vector3 (
            transform.localPosition.x, transform.localPosition.y, zPos);
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yAng, transform.eulerAngles.z);

    }
}
