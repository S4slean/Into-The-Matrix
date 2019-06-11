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
        Debug.Log("hint");

        if(other.name == "Player" && Started == false)
        {
            Debug.Log("hint2");
            StartCoroutine(NextLine());
            Started = true;
        }
    }

    IEnumerator NextLine()
    {
        Debug.Log("nextline");
        for (int i = 0; i < dialogue.dialogueLines.Count; i++)
        {
            Debug.Log("index " + dialogue.index);
            dialogue.DisplayDialogue();
            yield return new WaitForSeconds(3);
        }
        yield break;
    }
}
