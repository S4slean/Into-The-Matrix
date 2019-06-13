using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TempsPlongee : MonoBehaviour
{
    public bool plongee;
    public float timeMax;
    public float timer;
    public Image timeBar;
    public bool timeChange;
    public PlayerStats statsScript;
	public Animator anim;

    void Start()
    {
		timeBar = GetComponent<Image>();
        timer = timeMax;
        statsScript = FindObjectOfType<PlayerStats>();
    }
    
    void Update()
    {
        if (plongee)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
        timeBar.fillAmount = timer / timeMax;
        if (!timeChange)
        { timeBar.color = Color.Lerp(Color.red, Color.white, timer / timeMax); }
        if (timer > timeMax)
        { timer = timeMax; }

        //commandes de Test
        if (Input.GetKeyDown(KeyCode.UpArrow))
        { StartCoroutine(TimeGain(5)); }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        { StartCoroutine(TimeLoss(5)); }

		if (timer < 60)
		{
			anim.SetBool("Glitch", true);
			if (timer < 30)
			{
				anim.SetBool("Hurry", true);
			}
		}
		else
		{
			anim.SetBool("Glitch", false);
			anim.SetBool("Hurry", false);
			anim.Play("Empty");
		}

		if (timer <= 0)
        {

            Debug.Log("Plus de temps");
            statsScript.Death();
        }
    }

	public void LoseTime(int t)
	{
		StartCoroutine(TimeLoss(t));
	}

	public void GainTime(int t)
	{
		StartCoroutine(TimeGain(t));
	}


    public IEnumerator TimeLoss(float timeLost)
    {
        timer -= timeLost;
        timeChange = true;
        timeBar.color = Color.red;
        yield return new WaitForSecondsRealtime(0.05f);
        timeBar.color = Color.white;
        yield return new WaitForSecondsRealtime(0.05f);
        timeBar.color = Color.red;
        yield return new WaitForSecondsRealtime(0.05f);
        timeBar.color = Color.white;
        yield return new WaitForSecondsRealtime(0.05f);
        timeChange = false;
    }

    public IEnumerator TimeGain(float timeGained)
    {
        timer += timeGained;
        timeChange = true;
        timeBar.color = Color.green;
        yield return new WaitForSecondsRealtime(0.05f);
        timeBar.color = Color.white;
        yield return new WaitForSecondsRealtime(0.05f);
        timeBar.color = Color.green;
        yield return new WaitForSecondsRealtime(0.05f);
        timeBar.color = Color.white;
        yield return new WaitForSecondsRealtime(0.05f);
        timeChange = false;
    }
}
