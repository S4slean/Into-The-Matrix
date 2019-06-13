using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehavior : MonoBehaviour
{
	Vector3 startPos;
	 float hookSpeed =20 ;
	public float range = 8;
	public bool isObject = false;

	public bool gotSmthing = false;

	public Transform playerHand;
	GameObject objectCatched;
	Vector3 catchPos;
	GameObject player;
	Vector3 comparePos;
	Vector3 bringPos;
	Vector3 temp;


	private void Start()
	{
		startPos = transform.position;
		player = FindObjectOfType<CharaController>().gameObject;
		player.GetComponent<CharaController>().FreezePlayer(1);
		player.GetComponent<CharaController>().hooked = true;
	}

	void Update()
	{
		if(!gotSmthing)
			transform.position += transform.forward * hookSpeed * Time.deltaTime;
		else if (!isObject)
		{
			player.transform.position += player.transform.forward * hookSpeed* Time.deltaTime;

			
			comparePos = catchPos - (player.transform.position);
			if(comparePos.magnitude < .2f)
			{
				Debug.Log("hooked to pos");
				player.transform.position = catchPos;
				player.GetComponent<CharaController>().freezing = false;
				player.GetComponent<CharaController>().hooked = false;
				Destroy(gameObject);
			}

		}
		else
		{
			transform.position -= transform.forward * hookSpeed * Time.deltaTime;
			
			comparePos = temp - transform.position;
			if(comparePos.magnitude < .1f)
			{
				if (objectCatched != null)
				{
					objectCatched.transform.parent = null;
					objectCatched.transform.position = bringPos;
					player.GetComponent<CharaController>().freezing = false;
					Debug.Log(objectCatched.name + " of parent " + objectCatched.transform.parent);
				}
				Debug.Log("Object at pos");
				Debug.Log(bringPos);
				player.GetComponent<CharaController>().hooked = false;
				Destroy(gameObject);
			}
		}



		CheckRange();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" || gotSmthing )
			return;


		if (other.GetComponent<RoomCameraTrigger>() != null || other.GetComponent<DealDamage>() != null || other.GetComponent<TP>() != null || other.GetComponent<PNJ_DialogueHint>()!= null)
			return;

		if (other.GetComponent<CapsuleCollider>() != null && other.tag != "Player")
		{
			Debug.Log("destroyed by capsule");
			Destroy(gameObject);
		}

		objectCatched = other.gameObject;
		gotSmthing = true; 

		if (other.GetComponent<Turret>() != null || other.GetComponent<SkillItem>() != null || other.GetComponent<PushableBloc>() != null || other.GetComponent<PickUpScript>())
		{
			isObject = true;
			other.transform.parent = transform;
			
			if(other.GetComponent<PushableBloc>() || other.GetComponent<Turret>() != null)
			{
				bringPos = player.transform.position + player.transform.forward * 2;
				if(other.transform.position == bringPos)
				{
					other.transform.parent = null;
					Destroy(gameObject);
				}
			}
			else
			{
				bringPos = player.transform.position+ Vector3.up;

			}

			temp = new Vector3(bringPos.x, 1, bringPos.z);

		}
		else
		{

			catchPos = other.transform.position - (transform.forward*2);
			catchPos = new Vector3(catchPos.x, 0, catchPos.z);
			Debug.Log(other.transform.name + catchPos);
		}
	}

	private void CheckRange()
	{
		if (Vector3.Magnitude(transform.position - startPos) > range)
		{
			player.GetComponent<CharaController>().freezing = false;
			player.GetComponent<CharaController>().hooked = false;
			gameObject.SetActive(false);

		}

	}

	void ResleaseObject()
	{
		objectCatched.transform.parent = null;
		Debug.Log("destroy by release");
		player.GetComponent<CharaController>().hooked = false;
		Destroy(gameObject);
	}
}
