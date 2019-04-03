using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemis : MonoBehaviour
{
    public int typeEnnemi;
    public bool spawned;
    private PoolingEnnemis poolScript;
    public int patrolWidth;
    public int patrolHeight;

    private void Start()
    {
        poolScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PoolingEnnemis>();
    }

    //fonction de test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        { Spawn(); }
    }

    public void Spawn()
    {
        if (!spawned)
        {
            spawned = true;
            poolScript.PoolEnnemi(transform.position, typeEnnemi, patrolWidth, patrolHeight);
        }
    }
}
/* 0 : Drain
 * 1 : Charge
 * 2 : Counter
 * 3 : Mur
 * 4 : Roulade
 * 5 : Grappin
 * 6 : Decoy
 * 7 : Racine
 * 8 : BouleDeFeu
 */