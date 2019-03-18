using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemis : MonoBehaviour
{
    public int typeEnnemi;
    public bool spawned;
    public PoolingEnnemis poolScript;

    void Spawn()
    {
        if (!spawned)
        {
            spawned = true;
            poolScript.PoolEnnemi(transform.position, typeEnnemi);
        }
    }
}
/* 1 : Drain
 * 2 : Charge
 * 3 : Counter
 * 4 : Mur
 * 5 : Roulade
 * 6 : Grappin
 * 7 : Decoy
 * 8 : Racine
 * 9 : BouleDeFeu
 */