using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemy : MonoBehaviour
{
	public enum State
	{
		SelectSkill,
		rangedAttack,
		meleeAttack,
		wait,
		follow
	}

	[Header ("Stats")]
	[Range(1,1000)] public int difficulty;
    public int maxHealth =1;
	public int health = 1;
	public int strength = 1;

	[Header ("Movement")]
	int moveStep = 12;
	float stepDuration = TickManager.tickDuration/2;
	float stepFreeze = TickManager.tickDuration*3;
	public int detectionRange = 6;

	[Header("States")]
	public State state;
	[SerializeField] private bool ismoving = false;
	public bool isAttacking = false;
	public bool unableToMove = false;
	public bool unableToRotate = false;



	int step;
	private GameObject player;
	public Vector3 enemyToPlayer;
	private Skill selectedSkill;

	[Header("Skills")]
	public List<Skill> meleeSkills;
	public List<Skill> rangedSkills;

    [Header("Son")]
    public testson SoundDj;
	public Animator anim;

    private void Start()
	{
		player = FindObjectOfType<CharaController>().gameObject;
		state = State.wait;

		//TickManager.OnTick += delegate (object sender, TickManager.OnTickEventArgs e) 
		//{
		//	//Le tick est appelé ici !!!!!

		//};
	}

	private void OnEnable()
	{
		state = State.wait;
		StartCoroutine(WaitForNewCycle(1));
	}

	private void Update()
	{

		CheckHole();

		CheckDeath();																//Vérifie si il est pas mort
		enemyToPlayer = player.transform.position - transform.position;				//vérifie la position relative du joueur par rapport à l'ennemie (permettra de récupérer la distance et l'axe le plus court jusqu'à lui)
	}

	private void CheckHole()
	{
		//if (Physics.Raycast(transform.position, Vector3.down, ))
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
            SoundDj.Deathrobot.Play();
			Debug.Log("aie!");
			gameObject.SetActive(false);
		}
	}

	//Utiliser le skill et passer en attente de la prochaine action
	public void useSkill()
	{
		if(selectedSkill != null)
			StartCoroutine(selectedSkill.EnemyUse(this));
	}

	//Permet au mob de se rapprocher du joueur
	public void GetClose()
	{
		//si plus éloigné sur l'axe horizontal se rapprocher horizontalement
		if(Mathf.Abs(enemyToPlayer.x) > Mathf.Abs(enemyToPlayer.z) && !ismoving )
		{
			if( Mathf.RoundToInt(Mathf.Abs(enemyToPlayer.z)) != 0)
				StartCoroutine(Move(Vector3.forward * Mathf.Sign(enemyToPlayer.z)));
			else
				StartCoroutine(Move(Vector3.right * Mathf.Sign(enemyToPlayer.x)));
		}
		//si plus éloigné sur l'axe vertical se rapprocher verticalement
		else if(!ismoving)
		{
			if (Mathf.RoundToInt(Mathf.Abs(enemyToPlayer.x)) != 0)
				StartCoroutine(Move(Vector3.right * Mathf.Sign(enemyToPlayer.x)));
			else
				StartCoroutine(Move(Vector3.forward * Mathf.Sign(enemyToPlayer.z)));
		}
	}



	//Coroutine de mouvement sur une case. Prend en paramètre la direction du dépacement.
	public IEnumerator Move(Vector3 axe)
	{
		

		ismoving = true;

		//Detection des obstacles. Si le chemin est obstrué le déplacement est annulé
		if (Physics.Raycast(transform.position + Vector3.up, axe,2,9) || unableToMove)						
		{
			ismoving = false;
			yield break;
		}

		if(!unableToRotate)
			transform.rotation = Quaternion.LookRotation(axe);

		anim.Play("Walk");
		////Déplacement du perso sur chaque frame pendant "moveStep" frame
		for (int i = 0; i < Mathf.Abs(moveStep); i++)
		{
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;

			yield return new WaitForSeconds(stepDuration / moveStep);
		}
		ismoving = false;
		//Vector3 startPos = transform.position;

		//for (float count = 0; count < TickManager.tickDuration - .05f; count += Time.deltaTime)
		//{
		//	transform.localPosition = startPos + axe * (count / TickManager.tickDuration) * 2;

		//	yield return new WaitForEndOfFrame();
		//}
		//transform.localPosition = startPos + axe * 2;

		StartCoroutine(WaitForNewCycle(stepFreeze));
	}

	//Phase d'attente après une attaque (Relance le patterne de l'ennemi)
	public IEnumerator WaitForNewCycle(float waitingTime)
	{
		state = State.wait;
		yield return new WaitForSeconds(waitingTime * TickManager.tickDuration);

		state = State.SelectSkill;
	}


}
