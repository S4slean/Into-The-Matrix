using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
	[Header("References")]
	public GameObject AttackBox;
	private Animator anim;

	[Header ("Move Stats")]
	[Range (0, 300) ]public int swipeTolerance = 30;
	public float stepDistance;
	public int moveStep = 30;
	public float stepDuration = 1;
	public float delayBeforeRun = .7f;
	public Vector3 lastMove;

	[Header("Attack Stats")]
	public int attackStrength = 1;
	public float attackLength = 1;
	public float attackWidth = 1;
	public static Collider[] targets;

	[Header ("States")]
	[SerializeField] bool unableToMove = false;
	[SerializeField] bool unableToRotate = false;
	[SerializeField] public bool isMoving = false;
	[SerializeField] bool inUI = false;
	[SerializeField] public bool freezing = false;

	private Vector3 startMousePos;
	private Vector3 hitPosition;
	public Vector3 swipe;
	private float holdedTime;

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
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
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

		if (Input.GetMouseButtonUp(0) && !isMoving && !inUI)
		{
			if (swipe.magnitude < swipeTolerance)
			{
				if (HandleTargetting())
					return;

				Attack();
				return;
			}
			HandleMove();
		}

		if (Input.GetMouseButton(0) && holdedTime > delayBeforeRun && !inUI)
		{
			if (swipe.magnitude > swipeTolerance)
				HandleMove();
			else if(!isMoving)
				StartCoroutine(Move(lastMove));

			startMousePos = Input.mousePosition;
		}

		holdedTime += Time.deltaTime;
	}

	private void HandleFall()
	{
		if(!Physics.Raycast(transform.position + .1f * Vector3.up, Vector3.down)/* && anim.GetCurrentAnimatorStateInfo(0).IsName("Fall")*/)
		{
			anim.Play("Fall");
			GetComponent<PlayerStats>().KillPlayer();
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

			if(transform.rotation == Quaternion.LookRotation(Vector3.right * Mathf.Sign(step)) && !isMoving)
				StartCoroutine(Move(Vector3.right * Mathf.Sign(step)));
			if (Mathf.Sign(step) < 0)
			{
				lastMove = Vector3.left;
				if (!unableToRotate)
					transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
			}

			if (Mathf.Sign(step) > 0)
			{
				lastMove = Vector3.right;
				if (!unableToRotate)
					transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
			}



		}

		//mouvement Vertical
		if (Mathf.Abs(swipe.x) < Math.Abs(swipe.y))
		{
			int step = Mathf.RoundToInt(swipe.y / stepDistance);

			if (transform.rotation == Quaternion.LookRotation(Vector3.forward * Mathf.Sign(step)) && !isMoving)
				StartCoroutine(Move(Vector3.forward * Mathf.Sign(step)));
			if (Mathf.Sign(step) < 0)
			{
				lastMove = Vector3.back;
				if (!unableToRotate)
					transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
			}

			if (Mathf.Sign(step) > 0)
			{
				lastMove = Vector3.forward;
				if (!unableToRotate)
					transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			}
		}


	}

	public IEnumerator Move(Vector3 axe)
	{
		if (unableToMove || isMoving)
			yield break;

		lastMove = axe;
		isMoving = true;

		if (Physics.Raycast(transform.position + Vector3.up, axe, 2, 9))
		{
			isMoving = false;
			yield break;
		}

		//Déplacement du perso sur chaque frame pendant "moveStep" frame
		for (int i = 0; i < Mathf.Abs(moveStep); i++)
		{
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;

			yield return new WaitForSeconds(0);
		}
		
		isMoving = false;

		//StartCoroutine(FreezePlayer());
	}

	void Attack()
	{
		if (freezing)
			return;

		AttackBox.GetComponent<DealDamage>().damage = attackStrength;
		AttackBox.SetActive(true);
		anim.Play("Attack");
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

	IEnumerator FreezePlayer()
	{
		if (freezing)
			yield break;

		freezing = true;
		bool canMove;
		bool canRotate;

		GetPlayerMovement(out canMove, out canRotate);
		SetPlayerMovement(false, false);
		yield return new WaitForSeconds(0.05f);
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
			print("chier");
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
