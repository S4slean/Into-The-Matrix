using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseButton;
    public GameObject ResumeButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;

        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
        ResumeButton.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;

        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
        ResumeButton.SetActive(false);
    }
}
