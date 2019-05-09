using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public void Detonation()
    {
        Destroy(gameObject);
        Debug.Log("Boom!");
    }
}
