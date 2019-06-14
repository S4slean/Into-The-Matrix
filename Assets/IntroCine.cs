using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class IntroCine : MonoBehaviour
{
	public List<GameObject> UItoActivate;
	public GameObject loading;
	public GameObject map;


	public List<CinemachineVirtualCamera> Cams;
	int camIndex = 0 ;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			if(camIndex == 0)
			{
				if (PlayerPrefs.HasKey("TutoDone") != true)
				{
					PlayerPrefs.SetInt("TutoDone", 0);
				}

				if (PlayerPrefs.GetInt("TutoDone") != 1)
				{
					Debug.Log("You will play the tutorial now");
				}
				else
				{
					loading.SetActive(true);
					//minimap.SetActive(true);
					//loading.GetComponent<Animator>().Play("Appear");
					foreach(GameObject obj in UItoActivate)
					{
						if(obj.GetComponent<RoomCameraTrigger>() == null)
							obj.SetActive(true);
					}
					Debug.Log("You passed the tutorial");
					map.SetActive(true);
					SceneManager.LoadSceneAsync("Lobby");
					
				return;
				}
			}

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
