using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallSquareBehavior : MonoBehaviour
{
    public int dureeVieEnTicks;
    public int damages;

    // Update is called once per frame
    void Update()
    {
        if (TickManager.tick > TickManager.tickDuration)
        {
            dureeVieEnTicks--;
            if (dureeVieEnTicks <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SimpleEnemy>() != null)
        {
            other.GetComponent<SimpleEnemy>().health -= damages;
        }
    }
}
