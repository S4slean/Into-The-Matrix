using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseButton;
    public GameObject ResumeButton;
	public AudioMixer mixer;

	public Slider master;

	public Slider music;

	public Slider sfx;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateMaster( )
    {
		mixer.SetFloat("Master", master.value);

    }

	public void UpdateMusic()
	{
		mixer.SetFloat("Music", music.value);
	
	}

	public void UpdateSfx()
	{
		mixer.SetFloat("SFX", sfx.value);
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
