using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class minimap : MonoBehaviour
{
	public string savePath;

	public void Start()
	{
#if UNITY_EDITOR
		savePath = Application.dataPath;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR

		savePath = Application.persistentDataPath;
#endif

		if (PlayerPrefs.HasKey("LastDay"))
		{
			if (PlayerPrefs.GetInt("LastDay") == System.DateTime.Now.Day && File.Exists(savePath + "/map.json"))
			{
				//ClearMap();
				//LoadMap();
			}
			else
			{
				//ClearMap();
			}
		}
	}


	public void ClearMap()
	{

		foreach (Transform child in transform)
		{
			if (child.GetSiblingIndex() != 0)
				Destroy(child.gameObject);

		}
	}



	public void SaveMap()
	{
		SavedMap save = new SavedMap();


		Image[] images = GetComponentsInChildren<Image>(true);
		RectTransform[] rtransform = GetComponentsInChildren<RectTransform>(true);

		Sprite tf = transform.GetChild(0).GetComponent<Image>().sprite;
		for (int i = 0; i < transform.childCount; i++)
		{
			if (images[i].sprite == null)
				continue;
			save.roomPos.Add(rtransform[i].anchoredPosition);
			save.sprites.Add(images[i].sprite);
		}

#if UNITY_EDITOR
		savePath = Application.dataPath;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR

		savePath = Application.persistentDataPath;
#endif

		string json = JsonUtility.ToJson(save);
		File.WriteAllText(savePath + "/map.json", json);
		Debug.Log("Map saved !");
	}



	public void LoadMap()
	{
		SavedMap map = new SavedMap();

#if UNITY_EDITOR
		savePath = Application.dataPath;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR

		savePath = Application.persistentDataPath;
#endif

		string json = File.ReadAllText(savePath + "/map.json");
		JsonUtility.FromJsonOverwrite(json, map);

		for (int i = 1; i < map.sprites.Count - 1; i++)
		{
			GameObject instance = Instantiate(Resources.Load("Resources/UI/minimapRoom") as GameObject, transform);
			instance.GetComponent<Image>().sprite = map.sprites[i];
			instance.GetComponent<RectTransform>().anchoredPosition = map.roomPos[i];
		}

		Debug.Log("minimap Loaded !");
	}


	public class SavedMap
	{
		public List<Sprite> sprites;
		public List<Vector2> roomPos;
	}
}
