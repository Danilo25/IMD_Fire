using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChairText : MonoBehaviour
{
    TextMeshPro text;
    [SerializeField] string textPrompt = "Sentar (F)";
    private string currentText;
    private float initialXang;
    private float initialZang;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        currentText = textPrompt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HidePrompt()
    {
        currentText = "";
        text.text = currentText;
    }

    // Toggles the visibility of the prompt & Changes the angle its being displayed
    public void Show(bool near, float angToPlayer)
    {
        // visibility
        text.text = near ? currentText : "";

        // changing displaying angle
        Vector3 angs = transform.parent.transform.eulerAngles;
        transform.eulerAngles = new Vector3(0, angToPlayer, 0);

    }
}
