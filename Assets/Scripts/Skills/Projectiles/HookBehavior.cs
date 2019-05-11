using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehavior : MonoBehaviour
{
    public int squaresCrossed = 1;


    public IEnumerator HookThrow(int squareNb,Vector3 hookDir)
    {
        while (squaresCrossed < squareNb)
        {
            transform.position += hookDir * 2;
            squaresCrossed++;
            yield return new WaitForSeconds(TickManager.tickDuration);
        }
    }
}
