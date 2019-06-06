using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
    public AudioSource myaudio;
    public AudioClip SecondeSon;

    // Start is called before the first frame update
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
        Invoke("audioFinish", myaudio.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("change");
           // SwitchSon();
        }
    }

    public void SwitchSon ()
    {
        myaudio.clip = SecondeSon;
        myaudio.Play();
    }

    void audioFinish ()
    {
        Debug.Log("Le son est finie");
        SwitchSon();
        myaudio.loop = true;
    }
}
