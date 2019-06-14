using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvrdItem : MonoBehaviour
{
	public enum Ovrd { trap, enmy, spawn, key };
	public Ovrd ovrd;
	public SpriteRenderer sprite;
	public List<Sprite> spr;
    public  testson SoundDj;

    PlayerStats player;

	private void Start()
	{
        SoundDj = GameObject.FindGameObjectWithTag("SoundDj").GetComponent<testson>();
        player = FindObjectOfType<PlayerStats>();
		int rd = Random.Range(0, 3);
		if (ovrd == Ovrd.key)
		{
			if(player.key == 0)
				sprite.sprite = spr[3];
			else if(player.key == 1)
				sprite.sprite = spr[4];
			else
				sprite.sprite = spr[5];

			return;
		}

		if (rd == 0)
		{
			sprite.sprite = spr[0];
			ovrd = Ovrd.trap;
		}

		if (rd == 1)
		{
			sprite.sprite = spr[1];
			ovrd = Ovrd.enmy;
		}

		if (rd == 2)
		{
			sprite.sprite = spr[2];
			ovrd = Ovrd.spawn;

		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<CharaController>() != null)
		{
			switch (ovrd)
			{
				case Ovrd.trap:
					{
                        SoundDj.Bonus.Play();
                        player.trapOvrd++;
						break;
					}
				case Ovrd.enmy:
					{
                        SoundDj.Bonus.Play();
                        player.enmyOvrd++;
						break;
					}
				case Ovrd.spawn:
					{
                        SoundDj.Bonus.Play();
						player.phoneOvrd++;
						break;
					}
				case Ovrd.key:
					{
                        SoundDj.GetRuneSound.Play();
						player.key++;
						break;
					}
			}

			Destroy(gameObject);
		}
	}
}
