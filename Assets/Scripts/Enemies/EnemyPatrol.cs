using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Vector3[] PatrolWayPoints;
    public bool patrol = true;
    private Vector3 nextWayPoint;
    public int index;

    void Start()
    {
        nextWayPoint = PatrolWayPoints[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Patrol()
    {
        if (transform.position.x == nextWayPoint.x && transform.position.z == nextWayPoint.z)
        {
            if (index == PatrolWayPoints.Length - 1)
            { index = 0; }
            else
            { index++; }
            nextWayPoint = PatrolWayPoints[index];
        }
        else
        {
            if (transform.position.x > nextWayPoint.x)
            { transform.position -= new Vector3(2, 0, 0); }
            else if (transform.position.x < nextWayPoint.x)
            { transform.position += new Vector3(2, 0, 0); }
            else if (transform.position.z > nextWayPoint.z)
            { transform.position -= new Vector3(0, 0, 2); }
            else if (transform.position.z < nextWayPoint.z)
            { transform.position += new Vector3(0, 0, 2); }
        }

        yield return new WaitForSeconds(TickManager.tickDuration);

        if (patrol)
        { StartCoroutine(Patrol()); }
    }
}
