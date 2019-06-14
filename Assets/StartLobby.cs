using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GameObject.FindGameObjectWithTag("Loading").GetComponent<Animator>().Play("Disappear");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
