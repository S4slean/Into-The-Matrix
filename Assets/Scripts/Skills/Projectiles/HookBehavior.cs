using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehavior : MonoBehaviour
{
    public int squaresCrossed = 1;
    public bool aller = true;
    public bool contact;
    public bool returned;
    public GameObject grabbedObject;
    public bool pickUpObjectGrabbed;
    public PickUpScript pickedUpObjectScript;


   /* public IEnumerator HookThrow(int squareNb,Vector3 hookDir)
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
    }*/

    public IEnumerator HookThrow(Vector3 hookDir)
    {
        if (returned)
        {
            if (contact && !pickUpObjectGrabbed)
            { transform.GetChild(0).SetParent(null); }
            GetComponentInParent<Hook>().HookReturned();
            Destroy(gameObject);
        }
        if (squaresCrossed == 4)
        {
            aller = false;
        }
        if (!contact || aller)
        {
            transform.position += hookDir * 2;
            squaresCrossed++;
        }
        else
        {
            transform.position -= hookDir * 2;
            squaresCrossed--;
            if (squaresCrossed == 1)
            {
                if (pickUpObjectGrabbed)
                { pickedUpObjectScript.PickedUp(); }
                returned = true;
            }
        }
        yield return new WaitForSeconds(TickManager.tickDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        contact = true;
        if (other.tag == "PushableBloc")
        { other.transform.SetParent(this.transform); }
        else if (other.tag == "Pickup")
        {
            other.transform.SetParent(this.transform);
            pickUpObjectGrabbed = true;
            pickedUpObjectScript = other.GetComponent<PickUpScript>();
        }
        else if (other.tag == "Lever")
        {
            other.GetComponent<Lever>().ActivateLever();
        }
        Debug.Log("grabbed");
    }
}
