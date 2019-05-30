using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class minimapRoom : MonoBehaviour, IPointerDownHandler
{
	
	public DungeonDoor DJdoor;
	public bool isTP = false;
	public bool selected = false;

	Animator anim;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isTP && SceneManager.GetActiveScene().buildIndex == 0)
		{
			DJdoor.SelectRoom(this);
		}
	}

	public void Start()
	{
		DJdoor = FindObjectOfType<DungeonDoor>();
		anim = GetComponent<Animator>();

		if( isTP == true && DJdoor != null )
		{
			DJdoor.availableTP.Add(gameObject);
		}
	}


	private void Update()
	{
		anim.SetBool("Selected", selected);
	}
}
