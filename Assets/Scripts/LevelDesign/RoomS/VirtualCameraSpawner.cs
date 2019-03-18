using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
