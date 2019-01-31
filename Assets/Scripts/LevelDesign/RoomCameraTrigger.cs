using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class RoomCameraTrigger : MonoBehaviour
{

	public GameObject virtualCam;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if(Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera != null)
				Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
			virtualCam.SetActive(true);
		}
	}
}
