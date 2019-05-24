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
	private CharaController controller;
	private bool UIvisible = false;

    private void Start()
    {
        cabineUI = GameObject.FindGameObjectWithTag("CabineUI");
        tpButton = transform.GetChild(0).GetComponent<Button>();
        returnButton = transform.GetChild(1).GetComponent<Button>();
		controller = FindObjectOfType<CharaController>();
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
		controller.enabled = false;
		controller.freezing = true;
    }

    void TP()
    {
		controller.freezing = false;
		PlayerStats player = FindObjectOfType<PlayerStats>();
		player.StartCoroutine(player.BackToLobby());
		tpButton.gameObject.SetActive(false);
		returnButton.gameObject.SetActive(false);
		StartCoroutine(DesactiveUI());
		
	}

    void Exit()
    {
        tpButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
		controller.freezing = false;
		StartCoroutine(DesactiveUI());
    }

	IEnumerator DesactiveUI()
	{
		yield return new WaitForSeconds(.1f);
		UIvisible = false;
		controller.enabled = true;
	}
}
