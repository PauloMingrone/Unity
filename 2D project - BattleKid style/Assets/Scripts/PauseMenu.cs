using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    //allow to be accessed by other scripts
    public static PauseMenu instance;
    public GameObject pauseGUI;
    public bool isGamePaused = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseGameOnOff();
        }
    }

    void PauseGameOnOff()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            pauseGUI.SetActive(false);
            Time.timeScale = 1f;

        } else
        {
            isGamePaused = true;
            pauseGUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
