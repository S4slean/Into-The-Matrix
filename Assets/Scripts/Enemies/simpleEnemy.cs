using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemy : MonoBehaviour
{
	public enum State
	{
		patrolRight,
		patrolDown,
		patrolLeft,
		patrolUp,
		SelectSkill,
		rangedAttack,
		meleeAttack,
		wait
	}

	[Header ("Stats")]
	[Range(1,1000)] public int difficulty;
	public int moveStep = 8;
	public int patrolWidth ;
	public int patrolHeight ;
	public int detectionRange = 6;
	public float waitingTime = 2;

	[Header("States")]
	public State state;
	[SerializeField] private bool ismoving = false;
	int step;
	private GameObject player;
	private Vector3 enemyToPlayer;
	private Skill selectedSkill;

	[Header("Skills")]
	public List<Skill> meleeSkills;
	public List<Skill> rangedSkills;


	private void Start()
	{
		player = FindObjectOfType<CharaController>().gameObject;
		state = State.patrolUp;
		step = patrolWidth;
	}

	private void Update()
	{


		enemyToPlayer = player.transform.position - transform.position;

		switch (state)
		{
			case State.patrolRight:
				{
					if (step > 0 && !ismoving)
					{
						step--;
						StartCoroutine(Move( Vector3.right));
					}
					if( step <= 0)
					{
						step = patrolHeight;
						state = State.patrolDown;
					}
					DetectPlayer();
					break;
				}

			case State.patrolDown:
				{
					if (step > 0 && !ismoving)
					{
						step--;
						StartCoroutine(Move(Vector3.back));
					}
					if(step <= 0)
					{
						step = patrolWidth;
						state = State.patrolLeft;
					}
					DetectPlayer();
					break;
				}

			case State.patrolLeft:
				{
					if (step > 0 && !ismoving)
					{
						step--;
						StartCoroutine(Move(Vector3.left));
					}
					if (step <= 0)
					{
						step = patrolHeight;
						state = State.patrolUp;
					}
					DetectPlayer();
				
					break;
				}

			case State.patrolUp:
				{
					if (step > 0 && !ismoving)
					{
						step--;
						StartCoroutine(Move(Vector3.forward));
					}
					if (step <= 0)
					{
						step = patrolWidth;
						state = State.patrolRight;
					}
					DetectPlayer();
					break;
				}

			case State.SelectSkill:
				{
					if(meleeSkills.Count == 0)
					{
						selectedSkill = rangedSkills[UnityEngine.Random.Range(0, meleeSkills.Count)];
						state = State.rangedAttack;
					}
					else if(rangedSkills.Count == 0)
					{
						selectedSkill = meleeSkills[UnityEngine.Random.Range(0, meleeSkills.Count)];
						state = State.meleeAttack;
					}

					else if(enemyToPlayer.magnitude < 4)
					{
						selectedSkill = meleeSkills[UnityEngine.Random.Range(0, meleeSkills.Count)];
						state = State.meleeAttack;
					}
					else
					{
						selectedSkill = rangedSkills[UnityEngine.Random.Range(0, meleeSkills.Count)];
						state = State.rangedAttack;
					}


					break;
				}

			case State.meleeAttack:
				{
					if (enemyToPlayer.magnitude < 2.5f)
					{
						useSkill();
						return;
					}

					GetClose();

					break;
				}

			case State.wait:
				{
					break;
				}


		}


	}

	private void useSkill()
	{
		selectedSkill.Activate();
		state = State.wait;
		StartCoroutine(Wait());
	}

	public void GetClose()
	{

		if(Mathf.Abs(enemyToPlayer.x) > Mathf.Abs(enemyToPlayer.z) && !ismoving)
		{
			StartCoroutine(Move(Vector3.right * Mathf.Sign(enemyToPlayer.x)));
		}
		else if(!ismoving)
		{
			StartCoroutine(Move(Vector3.forward * Mathf.Sign(enemyToPlayer.z)));
		}
	}

	private void DetectPlayer()
	{
		if (enemyToPlayer.magnitude < detectionRange*2 )
		{
			state = State.SelectSkill;
			return;
		}
	}

	IEnumerator Move(Vector3 axe)
	{
		ismoving = true;

		if (Physics.Raycast(transform.position + Vector3.up, axe, 3))
		{
			ismoving = false;
			yield break;
		}

		//Déplacement du perso sur chaque frame pendant "moveStep" frame
		for (int i = 0; i < Mathf.Abs(moveStep); i++)
		{
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;

			yield return new WaitForSeconds(0);
		}
		ismoving = false;
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(waitingTime);
		DetectPlayer();
		state = State.patrolUp;
	}
}
