using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MainMenuObj;
    public GameObject SettingsObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Testes");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            MainMenuObj.SetActive(!MainMenuObj.activeSelf);
            SettingsObj.SetActive(!SettingsObj.activeSelf);
        }
        
    }
}
