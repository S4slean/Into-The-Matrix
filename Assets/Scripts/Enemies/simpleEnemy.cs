using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
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
		wait,
		follow
	}

	[Header ("Stats")]
	[Range(1,1000)] public int difficulty;
	public int health = 3;
	public int strength = 1;

	[Header ("Movement")]
	public int moveStep = 8;
	public int patrolWidth ;
	public int patrolHeight ;
	public int detectionRange = 6;

	[Header("States")]
	public State state;
	[SerializeField] private bool ismoving = false;
	public bool isAttacking = false;
	public bool unableToMove = false;
	public bool unableToRotate = false;



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

		CheckDeath();																//Vérifie si il est pas mort
		enemyToPlayer = player.transform.position - transform.position;				//vérifie la position relative du joueur par rapport à l'ennemie (permettra de récupérer la distance et l'axe le plus court jusqu'à lui)

		switch (state)
		{
			case State.patrolRight:													//Patrouille vers la droite
				{
					if (step > 0 && !ismoving)										//Si il lui reste des pas à faire il bouge à droite
					{
						step--;
						StartCoroutine(Move( Vector3.right));
					}
					if( step <= 0)													//Sinon il change de direction
					{
						step = patrolHeight;
						state = State.patrolDown;
					}
					DetectPlayer();													//Vérifie si le joueur est à portée de la zone de détection
					break;
				}

			case State.patrolDown:													//Patrouille vers le bas
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

			case State.patrolLeft:													//Patrouille vers la gauche
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

			case State.patrolUp:													//Patrouille vers le haut
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

			case State.SelectSkill:																									//Determine quel skill le mob doit utiliser puis pass
				{
					

					if(meleeSkills.Count == 0)																						//S'il n'a pas de skill au CaC il en prend un à distance
					{
						selectedSkill = rangedSkills[UnityEngine.Random.Range(0, rangedSkills.Count)];
						state = State.rangedAttack;
					}
					else if(rangedSkills.Count == 0)																				//S'il n'a pas de skill à distance il en prend un au Cac
					{
						selectedSkill = meleeSkills[UnityEngine.Random.Range(0, meleeSkills.Count)];
						state = State.meleeAttack;
					}

					else if(enemyToPlayer.magnitude < 4)																			//S'il a les deux mais est proche, il en prend un au Cac
					{
						selectedSkill = meleeSkills[UnityEngine.Random.Range(0, meleeSkills.Count)];
						state = State.meleeAttack;
					}
					else																											//S'il a les deux mais est éloigné, il en prend un à distance
					{
						selectedSkill = rangedSkills[UnityEngine.Random.Range(0, rangedSkills.Count)];
						state = State.rangedAttack;
					}


					break;
				}

			case State.meleeAttack:																									//Phase d'attaque au Cac
				{
					if (enemyToPlayer.magnitude < 2.5f)																				//s'il est à portée il utilise le skill
					{
						FacePlayer();
						state = State.wait;
						useSkill();
						return;
					}

					GetClose();																										//Sinon il se rapproche

					break;
				}

			case State.rangedAttack:
				{
					if(DetectPlayerInLine(selectedSkill.enemyActivationRange * 2))
					{
						FacePlayer();
						state = State.wait;
						useSkill();
						return;
					}
					else
						GetClose();

					break;
				}

			case State.follow:
				{
					GetClose();
					break;
				}

			case State.wait:																										//State vide en attente d'une coroutine de séléction d'action
				{
					break;
				}


		}


	}

	private bool DetectPlayerInLine(float range)
	{
		if (Mathf.Abs(enemyToPlayer.x) > Mathf.Abs(enemyToPlayer.z))
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, (Vector3.right * Mathf.Sign(enemyToPlayer.x)), out hit, range))
			{
				if (hit.transform.tag == "Player")
					return true;
				else
					return false;
			}
			else
				return false;

		}
		else
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, (Vector3.forward * Mathf.Sign(enemyToPlayer.z)), out hit, range))
			{
				if (hit.transform.tag == "Player")
					return true;
				else
					return false;
			}
			else
				return false;
		}
	}

	private void FacePlayer()
	{
		//si plus éloigné sur l'axe horizontal se rapprocher horizontalement
		if (Mathf.Abs(enemyToPlayer.x) > Mathf.Abs(enemyToPlayer.z))
		{
			transform.LookAt(transform.position +(Vector3.right * Mathf.Sign(enemyToPlayer.x)));
		}
		//si plus éloigné sur l'axe vertical se rapprocher verticalement
		else
		{
			transform.LookAt(transform.position + (Vector3.forward * Mathf.Sign(enemyToPlayer.z)));
		}
	}


	//vérifier si le mob est pas mort (t'es pas mourru l'âne, t'es pas mourru)
	private void CheckDeath()
	{
		if(health < 1)
		{
			Destroy(gameObject);
		}
	}

	//Utiliser le skill et passer en attente de la prochaine action
	public void useSkill()
	{
		StartCoroutine(selectedSkill.EnemyUse(this));
	}

	//Permet au mob de se rapprocher du joueur
	public void GetClose()
	{
		//si plus éloigné sur l'axe horizontal se rapprocher horizontalement
		if(Mathf.Abs(enemyToPlayer.x) > Mathf.Abs(enemyToPlayer.z) && !ismoving )
		{
			if( Mathf.RoundToInt(enemyToPlayer.x) <20)
				StartCoroutine(Move(Vector3.forward * Mathf.Sign(enemyToPlayer.z)));
			else
				StartCoroutine(Move(Vector3.right * Mathf.Sign(enemyToPlayer.x)));
		}
		//si plus éloigné sur l'axe vertical se rapprocher verticalement
		else if(!ismoving)
		{
			if (Mathf.RoundToInt(enemyToPlayer.z) <2)
				StartCoroutine(Move(Vector3.right * Mathf.Sign(enemyToPlayer.x)));
			else
				StartCoroutine(Move(Vector3.forward * Mathf.Sign(enemyToPlayer.z)));
		}
	}

	//Detection du joueur si il est dans le detection range(distance en case)
	private bool DetectPlayer()
	{
		if (enemyToPlayer.magnitude < detectionRange * 2)
		{
			RaycastHit hit;
			if(!Physics.Raycast(transform.position, enemyToPlayer, out hit, 9)) return false;

			if (hit.transform.name == "Player")
			{
				if(!isAttacking)
					state = State.SelectSkill;

				return true;
			}
			else return false;

		}
		else
			return false;
	}

	//Coroutine de mouvement sur une case. Prend en paramètre la direction du dépacement.
	IEnumerator Move(Vector3 axe)
	{
		ismoving = true;

		//Detection des obstacles. Si le chemin est obstrué le déplacement est annulé
		if (Physics.Raycast(transform.position + Vector3.up, axe,2) || unableToMove)						
		{
			ismoving = false;
			yield break;
		}

		if(!unableToRotate)
			transform.rotation = Quaternion.LookRotation(axe);


		//Déplacement du perso sur chaque frame pendant "moveStep" frame
		for (int i = 0; i < Mathf.Abs(moveStep); i++)
		{
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;

			yield return new WaitForSeconds(0);
		}
		ismoving = false;
	}

	//Phase d'attente après une attaque (Relance le patterne de l'ennemi)
	public IEnumerator WaitForNewCycle(float waitingTime)
	{
		state = State.wait;
		yield return new WaitForSeconds(waitingTime);
		if (!DetectPlayer())
		{
			state = State.patrolUp;
		}
	}
}
