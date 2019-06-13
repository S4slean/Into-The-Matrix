using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class minimapRoom : MonoBehaviour, IPointerDownHandler
{
	
	public DungeonDoor DJdoor;
	public RuneUI runeUI;
	public bool isTP = false;
	public bool isSelectable = false;
	public bool selected = false;

	Animator anim;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isTP && SceneManager.GetActiveScene().name == "Lobby")
		{
			FindObjectOfType<PlayerStats>().startingRoom = gameObject;
			DJdoor.SelectRoom(this);
		}
		if(isSelectable && SceneManager.GetActiveScene().name == "Lobby")
		{
			runeUI.SelectRoom(this);
		}
	}

	public void Start()
	{
		runeUI = FindObjectOfType<RuneUI>();
		DJdoor = FindObjectOfType<DungeonDoor>();
		anim = GetComponent<Animator>();

		if( isTP == true && DJdoor != null )
		{
			DJdoor.availableTP.Add(gameObject);
		}
	}


	private void Update()
	{
		if (transform.name != "PlayerOnMap" && selected == true && anim.GetBool("Selected") == false)
			anim.SetBool("Selected", true);

		if (transform.name != "PlayerOnMap" && selected == false && anim.GetBool("Selected") == true)
			anim.SetBool("Selected", false);
	}
}
