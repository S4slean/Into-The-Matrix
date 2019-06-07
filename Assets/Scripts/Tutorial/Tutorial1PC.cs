using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1PC : MonoBehaviour
{
    public dialogueBox dialogue; // Le script du dialogue du pnj
    public GameObject player; // le joueur
    public GameObject cam; // la caméra
    public Transform tpPoint; // le point où le joueur est tp dans le donjon
    public GameObject canvasPNJ; // le canvas du dialogue du pnj
    public GameObject machine; // la machine
    public Animator loadingScreen; // Animator de l'écran de chargement

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
                StartCoroutine("TutoTP");
            }
        }
    }

    private IEnumerator TutoTP()
    {
        player.GetComponent<CharaController>().enabled = false;
        canvasPNJ.SetActive(false); // le dialogue disparaît _wait
        yield return new WaitForSeconds(1);
        
        machine.SetActive(true); // la machine se matérialise 
        yield return new WaitForSeconds(1);

        // la machine fait son anim de TP ?


        loadingScreen.Play("Appear");// le joueur est téléporté (fade in écran noir) (fade out écran noir)
        yield return new WaitForSeconds(0.5f);
        player.transform.position = tpPoint.position;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<CharaController>().enabled = true;
        yield return new WaitForSeconds(1f);
        loadingScreen.Play("Disappear");
        yield return null;
    }

}
