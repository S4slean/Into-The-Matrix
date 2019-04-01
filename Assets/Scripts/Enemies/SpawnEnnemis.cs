using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemis : MonoBehaviour
{
    public int typeEnnemi;
    public bool spawned;
    public PoolingEnnemis poolScript;

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
            poolScript.PoolEnnemi(transform.position, typeEnnemi);
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