using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Vector3[] PatrolWayPoints;
    public bool patrol = true;
    private Vector3 nextWayPoint;
    public int index;
    public bool isMoving = false;
    public SimpleEnemy enemyScript;

    void Start()
    {
        enemyScript = gameObject.GetComponent<SimpleEnemy>();
        nextWayPoint = PatrolWayPoints[index];

        TickManager.OnTick += delegate (object sender, TickManager.OnTickEventArgs e)
        {if(patrol)
                StartCoroutine(Patrol());
        };
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
            { StartCoroutine(enemyScript.Move(Vector3.left)); }
            else if (transform.position.x < nextWayPoint.x)
            { StartCoroutine(enemyScript.Move(Vector3.right)); }
            else if (transform.position.z > nextWayPoint.z)
            { StartCoroutine(enemyScript.Move(Vector3.back)); }
            else if (transform.position.z < nextWayPoint.z)
            { StartCoroutine(enemyScript.Move(Vector3.forward)); }
        }

        yield return new WaitForSeconds(TickManager.tickDuration);
    }
}
