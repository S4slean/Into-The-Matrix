using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : MonoBehaviour
{
	CharaController player;

    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<CharaController>();
    }


	private void OnTriggerEnter(Collider other)
	{
		player.StartCoroutine(player.FreezePlayer(TickManager.tickDuration/2));
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player" && other.GetComponent<CharaController>())
		{
			player.freezing = true;
			player.StartCoroutine(player.Move(player.lastMove));
	
		}
		if(other.GetComponent<PushableBloc>() != null)
		{
			PushableBloc bloc = other.GetComponent<PushableBloc>();
			bloc.StartCoroutine(bloc.MoveBloc(bloc.lastMove));
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			player.freezing = false;
		}
	}
}
