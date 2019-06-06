using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1PC : MonoBehaviour
{
    public dialogueBox dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "AttackCollider")
        {
            Debug.Log("tonch");

            if (dialogue.index >= dialogue.dialogueLines.Count)
            {
                Debug.Log("Player get teleported to the tutorial dungeon");
            }
        }
    }

}
