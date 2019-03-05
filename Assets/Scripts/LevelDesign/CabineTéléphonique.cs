using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CabineTéléphonique : MonoBehaviour
{
    public GameObject cabineUI;
    public Button tpButton;
    public Button returnButton;

    private void Start()
    {
        tpButton.onClick.AddListener(TP);
        returnButton.onClick.AddListener(Exit);
    }

    void OnColliderEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            cabineUI.SetActive(true);
        }
    }

    void TP()
    {
        SceneManager.LoadScene(0);
    }

    void Exit()
    {
        cabineUI.SetActive(false);
    }
}
