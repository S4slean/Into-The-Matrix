using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharaController : MonoBehaviour
{
	[Header("References")]
	public GameObject AttackBox;
	public GameObject MoveBox;
	private Animator anim;

	[Header("Move Stats")]
	[Range(0, 300)] public int swipeTolerance = 30;
	public float stepDistance;
	private float stepDuration = 1;
	private float delayBeforeRun = .19f;
	private float stepFreeze = .2f;
	public Vector3 lastMove = Vector3.forward;

	[Header("Attack Stats")]
	public int attackStrength = 1;
	public float attackLength = 1;
	public float attackWidth = 1;
	public static Collider[] targets;

	[Header("States")]
	[SerializeField] bool unableToMove = false;
	[SerializeField] bool unableToRotate = false;
	[SerializeField] public bool isMoving = false;
	[SerializeField] bool inUI = false;
	[SerializeField] public bool freezing = false;
	public enum Buffer { None, Attack, Move, Rotate	};
	public Buffer buffer = Buffer.None;
	float debugTick = 0;

	private Vector3 startMousePos;
	private Vector3 hitPosition;
	public Vector3 swipe;
	private float holdedTime;
	public bool hooked = false;

	private bool dosmthing = false;

    public Vector3 enemyDir;
    public bool moveTowardsEnemy;
    public GameObject enemyConfronted;

    public bool moveIsOver;

	private void Awake()
	{
		if (FindObjectsOfType<CharaController>().Length > 1)
			Destroy(this.gameObject);
	}

	private void Start()
	{


		DontDestroyOnLoad(this.gameObject);


		if (AttackBox == null)
			AttackBox = transform.GetChild(1).gameObject;
		if (MoveBox == null)
			MoveBox = transform.GetChild(2).gameObject;

		stepDuration = TickManager.tickDuration*2;
		stepFreeze = TickManager.tickDuration/2.1f;

		anim = GetComponent<Animator>();

		TickManager.OnTick += PlayerAction;
	}

	public void PlayerAction()
	{
		dosmthing = true;
		
	}

	public void GetPlayerInTick()
	{
		TickManager.OnTick += PlayerAction;
	}



	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
			Debug.Log("playerprefCleared");
			TickManager.ClearDelegate();
		}

		debugTick += Time.deltaTime;

		HandleFall();

		if (Input.GetMouseButtonDown(0))
		{
			startMousePos = Input.mousePosition;
			holdedTime = 0;
			if (startMousePos.y < 200)
				inUI = true;
			else
				inUI = false;

		
		}

		hitPosition = Input.mousePosition;
		swipe = hitPosition - startMousePos;

		if (Input.GetMouseButtonUp(0) && !inUI)
		{
			if (swipe.magnitude < swipeTolerance && holdedTime <delayBeforeRun && !freezing)
			{
				if (HandleTargetting())
					return;

				buffer = Buffer.Attack;
				return;
			}
			else
			{
				HandleMove();
				buffer = Buffer.Move;
			}
			
		}

		if (Input.GetMouseButton(0) && holdedTime > delayBeforeRun && !inUI)
		{
			if (swipe.magnitude > swipeTolerance)
			{
				HandleMove();
			}
				
			buffer = Buffer.Move;
			startMousePos = Input.mousePosition;
		}

		if (dosmthing)
		{
			switch (buffer)
			{
				case Buffer.Attack:
					Attack();
					break;

				case Buffer.Move:
					StartCoroutine(Move(lastMove));
					break;

				case Buffer.Rotate:
					break;

				case Buffer.None:
					break;
			}

			dosmthing = false;
		}


		holdedTime += Time.deltaTime;
	}

	private void HandleFall()
	{
		if(!Physics.Raycast(transform.position + .1f * Vector3.up, Vector3.down,2 ) && !freezing && !hooked/* && anim.GetCurrentAnimatorStateInfo(0).IsName("Fall")*/)
		{
			anim.Play("Fall");
			GetComponent<PlayerStats>().KillPlayer();
			GetComponent<PlayerStats>().CheckDeath();
		}
	}

	public void HandleMove()
	{

		if (freezing)
			return;

		//Mouvement Horizontal
		if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
		{
			int step = Mathf.RoundToInt(swipe.x / stepDistance);

			if (Mathf.Sign(step) < 0)
				lastMove = Vector3.left;

			if (Mathf.Sign(step) > 0)
				lastMove = Vector3.right;
		}

		//mouvement Vertical
		if (Mathf.Abs(swipe.x) < Math.Abs(swipe.y))
		{
			int step = Mathf.RoundToInt(swipe.y / stepDistance);

			if (Mathf.Sign(step) < 0)
				lastMove = Vector3.back;

			if (Mathf.Sign(step) > 0)
				lastMove = Vector3.forward;
		}


	}

	private float stepState = 0;

	public IEnumerator Move(Vector3 axe)
	{
		debugTick = 0;

		if (unableToMove || isMoving)
			yield break;

		if (!unableToRotate)
			transform.LookAt(transform.position + lastMove);

		lastMove = axe;
		isMoving = true;

		RaycastHit hit;
		if (Physics.Raycast(transform.position + Vector3.up, axe, out hit, 2, 9))
		{
			isMoving = false;
			freezing = false;
            if (hit.transform.tag == "Enemy")
            {
                moveTowardsEnemy = true;
                enemyConfronted = hit.transform.gameObject;
                enemyDir = axe;
                yield return new WaitForSecondsRealtime(0.5f);
                moveTowardsEnemy = false;
                enemyConfronted = null;

				yield break;
			}
			else if(hit.transform.tag == "PushableBloc")
			{
				hit.transform.GetComponent<PushableBloc>().StartCoroutine(hit.transform.GetComponent<PushableBloc>().MoveBloc(axe));
			}

			yield break;
		}

		MoveBox.SetActive(true);

		if(!freezing)
			anim.Play("Walk");

		//stepState = 0;
		//{
		//	Vector3 startPos = transform.localPosition;

		//	while(stepState < stepDuration)
		//	{
		//		transform.localPosition += axe * 2 * Time.deltaTime / stepDuration;
		//		stepState += Time.deltaTime;
		//		yield return new WaitForEndOfFrame();
		//	}


		//}
		Vector3 startPos = transform.localPosition;

		//Déplacement du perso sur chaque frame pendant "moveStep" frame
		for (float count = 0; count < TickManager.tickDuration - .05f; count += Time.deltaTime)
		{
			transform.localPosition = startPos + axe * (count/TickManager.tickDuration) * 2;
			if (count/ TickManager.tickDuration > .5f)
				MoveBox.SetActive(false);
			yield return new WaitForEndOfFrame();
		}
		transform.localPosition = startPos + axe * 2;

		////Déplacement du perso sur chaque frame pendant "moveStep" frame
		//for (int i = 0; i < Mathf.Abs(moveStep); i++)
		//{
		//	transform.localPosition = transform.localPosition + (axe / moveStep) * 2;
		//	if (i == Mathf.CeilToInt(Mathf.Abs(moveStep) / 2))
		//		MoveBox.SetActive(false);
		//	yield return new WaitForSeconds(TickManager.tickDuration / 2 / moveStep);
		//}

		buffer = Buffer.None;
		//if (!freezing)
		//	StartCoroutine(FreezePlayer(TickManager.tickDuration*9/10/2));

		isMoving = false;
        moveIsOver = true;
        yield return new WaitForSeconds(0.05f);
        moveIsOver = false;
    }

	void Attack()
	{
		if (freezing)
			return;

		AttackBox.GetComponent<DealDamage>().damage = attackStrength;
		AttackBox.SetActive(true);
		anim.Play("Attack");
		buffer = Buffer.None;
	}

	public void SetPlayerMovement(bool canMove, bool canRotate)
	{
		unableToMove = !canMove;
		unableToRotate = !canRotate;
	}

	public void GetPlayerMovement(out bool canMove, out bool canRotate)
	{
		canMove = !unableToMove;
		canRotate = !unableToRotate;
	}

	public IEnumerator FreezePlayer(float duration)
	{
		if (freezing)
			yield break;

		freezing = true;
		bool canMove;
		bool canRotate;

		GetPlayerMovement(out canMove, out canRotate);
		SetPlayerMovement(false, false);
		buffer = Buffer.None;
		yield return new WaitForSeconds(duration);
		SetPlayerMovement(canMove, canRotate);
		freezing = false;
	}

	public bool HandleTargetting()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction, Color.green, 1);
		RaycastHit hit;
		Physics.Raycast(ray.origin, ray.direction , out hit, 10000,9);

		if (hit.transform == null)
		{
			return false;
		}
		else if (hit.transform.tag == "skillTarget")
		{
			hit.transform.GetComponent<caseTarget>().ActivateTarget();
			return true;
		}
		else return false;
		

	}
}
