using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraSpawner : MonoBehaviour
{
	GameObject virtualCamObject;
    // Start is called before the first frame update
    void Start()
    {
		virtualCamObject = Resources.Load("virtualCam") as GameObject;
		GameObject instance = Instantiate(virtualCamObject, transform.position, Quaternion.Euler(66,0,0), transform);
		transform.parent.GetComponent<RoomCameraTrigger>().virtualCam = instance;
		instance.SetActive(false);
		CinemachineVirtualCamera virtualCam = instance.GetComponent<CinemachineVirtualCamera>();
		virtualCam.Follow = FindObjectOfType<CharaController>().transform;
		instance.GetComponent<CinemachineConfiner>().m_BoundingVolume = GetComponent<Collider>();

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
