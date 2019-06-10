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
	public bool attack = false;
    public SimpleEnemy enemyScript;
	CharaController player;
	Animator anim;
    Vector3 startPos;

    void Start()
	{
		player = FindObjectOfType<CharaController>();
		enemyScript = gameObject.GetComponent<SimpleEnemy>();
		anim = GetComponent<Animator>();
        startPos = transform.localPosition;
		nextWayPoint = PatrolWayPoints[index];
		if (patrol)
			TickManager.OnTick += StartPatrol;
	}

	public void EnemyDecision()
	{
		if (attack)
		{
			Attack();
			return;
		}

		else if(Vector3.Magnitude(player.transform.position - transform.position) < 2.5f)
		{
			transform.LookAt(player.transform.position);
			attack = true;
		}

		else
		{
			StartPatrol();
		}
	}

	public void Attack()
	{
		anim.Play("Attack");
		attack = false;
	}

	private void StartPatrol()
	{
				StartCoroutine(Patrol());
	}

	// Update is called once per frame
	void Update()
    {
        
    }

    public IEnumerator Patrol()
    {
        if (transform.position.x == startPos.x + nextWayPoint.x && transform.position.z == startPos.z + nextWayPoint.z)
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
