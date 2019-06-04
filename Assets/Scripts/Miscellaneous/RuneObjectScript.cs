using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneObjectScript : MonoBehaviour
{
    public PlayerStats playerStatsScript;

    // Start is called before the first frame update
    void Start()
    {
        playerStatsScript = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerStatsScript.AddRunePiece();
            Destroy(gameObject);
        }
    }
}
