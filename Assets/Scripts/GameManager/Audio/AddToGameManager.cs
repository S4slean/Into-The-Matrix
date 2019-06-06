using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToGameManager : MonoBehaviour
{
    [SerializeField]

    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No AudioManagerFound in Scene");
        }
    }

    //A Chaque fois qu'on voudras jouer un son 

        public void TestSon()
    {
        Debug.Log("Joue le son");
        audioManager.Playsound("Bonus");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
