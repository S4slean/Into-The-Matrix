using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingEnnemis : MonoBehaviour
{
    public GameObject[] ennemisDrainArray;
    public GameObject[] ennemisChargeArray;
    public GameObject[] ennemisCounterArray;
    public GameObject[] ennemisMurArray;
    public GameObject[] ennemisRouladeArray;
    public GameObject[] ennemisGrappinArray;
    public GameObject[] ennemisDecoyArray;
    public GameObject[] ennemisRacineArray;
    public GameObject[] ennemisBouleDeFeuArray;

    public GameObject[][] ennemisArray;
    public int[] ennemisIndex;
    private GameObject spawnedEnemy;

    void Start()
    {
        for (int i = 1; i < 10; i++)
        {
            ennemisIndex[i] = i;
        }
        ennemisArray[1] = ennemisDrainArray;
        ennemisArray[2] = ennemisChargeArray;
        ennemisArray[3] = ennemisCounterArray;
        ennemisArray[4] = ennemisMurArray;
        ennemisArray[5] = ennemisRouladeArray;
        ennemisArray[6] = ennemisGrappinArray;
        ennemisArray[7] = ennemisDecoyArray;
        ennemisArray[8] = ennemisRacineArray;
        ennemisArray[9] = ennemisBouleDeFeuArray;
    }

    public void PoolEnnemi(Vector3 spawnLocation,int typeEnnemi)
    {
        spawnedEnemy = ennemisArray[typeEnnemi][ennemisIndex[typeEnnemi]];
        spawnedEnemy.GetComponent<Transform>().position = spawnLocation;
        spawnedEnemy.SetActive(true);
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
