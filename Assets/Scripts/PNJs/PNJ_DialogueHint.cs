using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_DialogueHint : MonoBehaviour
{
    dialogueBox dialogue;
    bool Started;
       

    // Start is called before the first frame update
    void Start()
    {
        dialogue = transform.GetComponentInChildren<dialogueBox>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.name == "Player" && Started == false)
        {
            StartCoroutine(NextLine());
            Started = true;
        }
    }

    IEnumerator NextLine()
    {
        for (int i = 0; i < dialogue.dialogueLines.Count; i++)
        {
            dialogue.DisplayDialogue();
            yield return new WaitForSeconds(3);
        }
        yield break;
    }
}
