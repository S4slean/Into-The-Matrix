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
    private SimpleEnemy enemyScript;

    void Start()
    {
        ennemisArray = new GameObject[9][];
        ennemisIndex = new int[9];
        for (int i = 0; i < 9; i++)
        {
            ennemisIndex[i] = 0; ;
        }
        ennemisArray[0] = ennemisDrainArray;
        ennemisArray[1] = ennemisChargeArray;
        ennemisArray[2] = ennemisCounterArray;
        ennemisArray[3] = ennemisMurArray;
        ennemisArray[4] = ennemisRouladeArray;
        ennemisArray[5] = ennemisGrappinArray;
        ennemisArray[6] = ennemisDecoyArray;
        ennemisArray[7] = ennemisRacineArray;
        ennemisArray[8] = ennemisBouleDeFeuArray;
    }

    public void PoolEnnemi(Vector3 spawnLocation,int typeEnnemi, int patrolWidth, int patrolHeight)
    {
        spawnedEnemy = ennemisArray[typeEnnemi][ennemisIndex[typeEnnemi]];
        spawnedEnemy.GetComponent<Transform>().position = spawnLocation;
        enemyScript = spawnedEnemy.GetComponent<SimpleEnemy>();
        enemyScript.health = enemyScript.maxHealth;
        spawnedEnemy.SetActive(true);
        if (ennemisIndex[typeEnnemi] < ennemisArray[typeEnnemi].Length-1)
        { ennemisIndex[typeEnnemi]++; }
        else
        { ennemisIndex[typeEnnemi] = 0; }
    }
}
/* 0 : Drain 
 * 1 : Charge
 * 2 : Counter
 * 4 : Mur
 * 5 : Roulade
 * 6 : Grappin
 * 7 : Decoy
 * 8 : Racine
 * 9 : BouleDeFeu
 */
