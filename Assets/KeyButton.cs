using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
	PlayerStats stats;
	Image img;
	public List<Sprite> sprites;

	public GameObject incompleteBP;
	public GameObject BPused;
	public GameObject BPremoved;

	public bool bypass = false;


	private void OnEnable()
	{
		img = GetComponent<Image>();
		stats = FindObjectOfType<PlayerStats>();
		if (stats.key > 3)
			stats.key = 3;
		if (stats.byPass)
		{
			bypass = true;
			img.sprite = sprites[4];
			return;
		}
		img.sprite = sprites[stats.key];
	}

	public void ActivateKey()
	{
		if(stats.key == 3)
		{
			bypass = !bypass;

			if (bypass)
			{
				img.sprite = sprites[4];
				stats.byPass = bypass;
				BPused.SetActive(true);
			}
			else
			{
				BPremoved.SetActive(true);
				stats.byPass = bypass;
				img.sprite = sprites[3];
			}
		}
		else
		{
			incompleteBP.SetActive(true);
		}
				
	}
}
