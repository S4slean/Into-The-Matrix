using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehavior : MonoBehaviour
{
    public int squaresCrossed = 1;
    public bool aller;
    public bool contact;
    public GameObject grabbedObject;


    public IEnumerator HookThrow(int squareNb,Vector3 hookDir)
    {
        aller = true;
        while (squaresCrossed < squareNb && aller == true)
        {
            transform.position += hookDir * 2;
            squaresCrossed++;
            yield return new WaitForSeconds(TickManager.tickDuration);
        }
        if (squaresCrossed == squareNb)
        { aller = false; }
        while (squaresCrossed > 1 && aller == false)
        {
            transform.position -= hookDir * 2;
            squaresCrossed--;
            yield return new WaitForSeconds(TickManager.tickDuration);
        }
        if (squaresCrossed == 1 && aller == false)
        {
            if (contact == true)
            { transform.GetChild(0).SetParent(null); }
            Destroy(gameObject);
            GetComponentInParent<Hook>().HookReturned();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        contact = true;
        other.transform.SetParent(this.transform);
        Debug.Log("grabbed");
    }
}
