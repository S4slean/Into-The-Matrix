using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testson : MonoBehaviour
{
    public AudioSource Bonus;
    public AudioSource Deathrobot;
    public AudioSource GetRuneSound;
    public AudioSource TP;
    public AudioSource lasserShot;
    public AudioSource LevierOn;
    public AudioSource Reflect;
    public AudioSource PhoneRing;
    public AudioSource StartShield;
    public AudioSource BruitdesPique;

    private void Start()
    {
      
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("a");
            Bonus.Play();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Deathrobot.Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GetRuneSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TP.Play();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            lasserShot.Play();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            LevierOn.Play();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Reflect.Play();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            PhoneRing.Play();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartShield.Play();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            BruitdesPique.Play();
        }
    }
}
