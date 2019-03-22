﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class RoomCameraTrigger : MonoBehaviour
{

	public GameObject virtualCam;
	public Sprite minimapSprite;
	GameObject minimap;
	GameObject minimapRoomPrefab;

	private void Start()
	{
		minimap = GameObject.FindObjectOfType<minimap>().gameObject;
		minimapRoomPrefab = Resources.Load("minimapRoom") as GameObject;
		
	}

	//Si le joueur entre dans le trigger: désactive la virtual cam actuelle et active celle de la salle
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if(Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera != null)
				Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
			virtualCam.SetActive(true);

			GameObject instance = Instantiate(minimapRoomPrefab, minimap.transform);
			instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.position.x * 21 / 14, transform.position.z * 31.5f / 20);
			instance.GetComponent<Image>().sprite = minimapSprite;

			minimap.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.position.x * 21 / 14, transform.position.z * 31.5f / 20);
			minimap.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-transform.position.x * 21 / 14, -transform.position.z * 31.5f / 20);
		}
	}
}