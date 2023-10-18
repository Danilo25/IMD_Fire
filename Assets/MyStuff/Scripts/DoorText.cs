using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorText : MonoBehaviour
{
    TextMeshPro text;
    [SerializeField] string openText = "Abrir (E)";
    [SerializeField] string closeText = "Fechar (E)";
    private string currentText;
    private float initialZPos;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        currentText = openText;
        initialZPos = transform.localPosition.z;
    }

    public void HidePrompt()
    {
        currentText = "";
        text.text = currentText;
    }

    // Changes the prompt of the door between 'openText' and 'closeText'
    public void ChangePrompt(bool open)
    {
        currentText = open ? closeText : openText;
        text.text = currentText;
    }

    // Toggles the visibility of the prompt & Changes the side its being displayed at
    public void Show(bool near, bool doorSide)
    {
        // visibility
        text.text = near ? currentText : "";

        // changing displayed side
        float yAng = transform.parent.transform.eulerAngles.y + (doorSide ? -180 : 0);
        float zPos = initialZPos * (doorSide ? 1 : -1);

        transform.localPosition = new Vector3 (
            transform.localPosition.x, transform.localPosition.y, zPos); // position
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yAng, transform.eulerAngles.z); // angle

    }
}
