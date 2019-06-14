using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroCine : MonoBehaviour
{
	public List<GameObject> UItoActivate;


	public List<CinemachineVirtualCamera> Cams;
	int camIndex = 0 ;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			if (Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera != null)
				Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
			if(camIndex < Cams.Count)
				Cams[camIndex].gameObject.SetActive(true);
			camIndex++;
		}

		if(camIndex == Cams.Count )
		{
			foreach (GameObject obj in UItoActivate)
			{
				obj.SetActive(true);
			}
			StartCoroutine(Wait());
		}
    }

	public IEnumerator Wait()
	{
		yield return new WaitForSeconds(2f);
		Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = .7f;
		yield return new WaitForSeconds(2f);

		this.enabled = false;
	}
}
