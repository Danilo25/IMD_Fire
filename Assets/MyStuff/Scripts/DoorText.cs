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

    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponent<TextMeshPro>();
        currentText = openText;
    }

    // Changes the prompt of the door between 'openText' and 'closeText'
    public void changePrompt(bool open)
    {
        currentText = open ? closeText : openText;
        text.text = currentText;
    }

    // Toggles the visibility of the prompt
    public void show(bool near)
    {
        text.text = near ? currentText : "";
    }
}
