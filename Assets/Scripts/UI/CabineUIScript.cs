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

    private void Start()
    {
        cabineUI = GameObject.FindGameObjectWithTag("CabineUI");
        tpButton = transform.GetChild(0).GetComponent<Button>();
        returnButton = transform.GetChild(1).GetComponent<Button>();
        tpButton.onClick.AddListener(TP);
        returnButton.onClick.AddListener(Exit);
    }

    public void Activate()
    {
        tpButton.gameObject.SetActive(true);
        tpButton.gameObject.SetActive(true);
    }

    void TP()
    {
        SceneManager.LoadScene(0);
    }

    void Exit()
    {
        tpButton.gameObject.SetActive(false);
        tpButton.gameObject.SetActive(false);
    }
}
