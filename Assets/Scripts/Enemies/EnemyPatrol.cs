using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Vector3[] PatrolWayPoints;
    public bool patrol = true;
    private Vector3 nextWayPoint;
    public int index = 0;
    public bool isMoving = false;
	public bool attack = false;
    public simpleEnemy enemyScript;
	CharaController player;
	Animator anim;
    Vector3 startPos;
	Vector3 Dest;

    void Start()
	{
		player = FindObjectOfType<CharaController>();
		enemyScript = GetComponent<simpleEnemy>();
		anim = GetComponent<Animator>();
        startPos = transform.position;
		nextWayPoint = PatrolWayPoints[index];
		Dest = startPos + nextWayPoint;
		if (patrol)
			TickManager.OnTick += EnemyDecision;
	}

	public void EnemyDecision()
	{
		if (attack)
		{
			Attack();
			return;
		}

		else if(Vector3.Magnitude(player.transform.position - transform.position) < 2.1f)
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
		
		
		Debug.Log(index);
        if (Vector3.Magnitude(Dest - transform.position) < .1f)
        {
			index++;
			if (index >= PatrolWayPoints.Length)
			{
				index = 0;

			}

			nextWayPoint = PatrolWayPoints[index];
			Dest = startPos + nextWayPoint;
        }
        else
        {
			Vector3 compare = Dest - transform.position;

			transform.LookAt(Dest);
			if(Mathf.Abs(compare.x) > Mathf.Abs(compare.z))
			{
				if(compare.x > 0)
				{
					enemyScript.StartCoroutine(enemyScript.Move(Vector3.right));
				}
				else if(compare.x < 0)
				{
					enemyScript.StartCoroutine(enemyScript.Move(Vector3.left));
				}
			}
			else
			{
				if (compare.z > 0)
				{
					enemyScript.StartCoroutine(enemyScript.Move(Vector3.forward));
				}
				else if(compare.z < 0)
				{
					enemyScript.StartCoroutine(enemyScript.Move(Vector3.back));
				}
			}
        }

        yield return new WaitForSeconds(TickManager.tickDuration);
    }
    
    public void OnDisable()
    {
        TickManager.OnTick -= EnemyDecision;
    }
}
