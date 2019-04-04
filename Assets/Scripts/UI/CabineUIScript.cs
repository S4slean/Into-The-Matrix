using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CabineUIScript : MonoBehaviour
{
    private GameObject cabineUI;
    private Button tpButton;
    private Button returnButton;
	private CharaController player;
	private bool UIvisible = false;

    private void Start()
    {
        cabineUI = GameObject.FindGameObjectWithTag("CabineUI");
        tpButton = transform.GetChild(0).GetComponent<Button>();
        returnButton = transform.GetChild(1).GetComponent<Button>();
		player = FindObjectOfType<CharaController>();
        tpButton.onClick.AddListener(TP);
        returnButton.onClick.AddListener(Exit);
    }

    public void Activate()
    {
		if (UIvisible)
			return;
        tpButton.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
		UIvisible = true;
		player.freezing = true;
    }

    void TP()
    {
		player.freezing = false;
		PlayerStats player = FindObjectOfType<PlayerStats>();
		player.StartCoroutine(player.BackToLobby());
		tpButton.gameObject.SetActive(false);
		returnButton.gameObject.SetActive(false);
		
	}

    void Exit()
    {
        tpButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
		player.freezing = false;
		StartCoroutine(DesactiveUI());
    }

	IEnumerator DesactiveUI()
	{
		yield return new WaitForSeconds(.1f);
		UIvisible = false;
	}
}
