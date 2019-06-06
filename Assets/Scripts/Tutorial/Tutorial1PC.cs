using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1PC : MonoBehaviour
{
    public dialogueBox dialogue;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "AttackCollider")
        {

            if (dialogue.index > dialogue.dialogueLines.Count - 1)
            {
                Debug.Log("Player get teleported to the tutorial dungeon");
            }
        }
    }

}
