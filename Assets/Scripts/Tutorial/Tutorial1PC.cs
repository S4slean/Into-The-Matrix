using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial1PC : MonoBehaviour
{
    public dialogueBox dialogue_pc; // Le script du dialogue du pc
    public PNJ_DialogueHint dialogue_donjon; // Le script du dialogue du donjon
    public GameObject player; // le joueur
    public GameObject cam; // la caméra
    public Transform tpPoint; // le point où le joueur est tp dans le donjon
    public GameObject canvasPNJ; // le canvas du dialogue du pnj
    public Text lastText; // Canvas du derier hint
    public GameObject machine; // la machine
    public GameObject UI; // L'UI du donjon
    public Animator loadingScreen; // Animator de l'écran de chargement

    private void Start()
    {
        if (PlayerPrefs.HasKey("TutoDone") != true)
        {
            PlayerPrefs.SetInt("TutoDone", 0);
        }

        if (PlayerPrefs.GetInt("TutoDone") != 1)
        {
            Debug.Log("You will play the tutorial now");
        }
        else
        {
            Debug.Log("You passed the tutorial");
            SceneManager.LoadScene("Lobby");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "AttackCollider")
        {

            if (dialogue_pc.index > dialogue_pc.dialogueLines.Count - 1)
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
        yield return new WaitForSeconds(1);
        player.transform.position = tpPoint.position;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<CharaController>().enabled = true;
        yield return new WaitForSeconds(1f);
        loadingScreen.Play("Disappear"); // Le loading screen disparaît
        yield return new WaitForSeconds(1f);
        dialogue_donjon.Activate(); // Affiche la première ligne du dialogue du donjon

        yield return null;
    }

    public void EndTutorial()
    {
        lastText.text = "";
        PlayerPrefs.SetInt("TutoDone",1);
    }



}
