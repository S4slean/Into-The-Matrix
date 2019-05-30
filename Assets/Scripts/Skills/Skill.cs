using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Skill : MonoBehaviour
{
	[Header ("Skill Infos")]
	public new string name;
	public Sprite icon;
	public string description;
    public int itemSlot;

	[Header ("Skill Stats")]
	[Tooltip("The cost at the shop")]
	public int cost;
	[Tooltip("The time between two successive activations by the player")]
	public float coolDownDuration;
	[Tooltip("The player needs to select a specific target ?")]
	public bool requireTarget = false;
	[Tooltip("")]
	public float enemyActivationRange = 5;
	[Tooltip("The anticipation animation duration")]
	public int enemyLaunchTime = 1;
	[Tooltip("The Recover animation duration (Vulnerability Frame)")]
	public int enemyRecoverTime = 1;
    [Tooltip("The number of times the player can use this ability")]
    public int nbOfUse = 1;

	[Header ("Debug Status (NE PAS MODIFIER)")]
	public float cooldown;

    //public PNJ_DoorKeeper_MySkills skillsShop;
    //public PNJ_Merchant_InstantiateButtons buttonsShop;

    public void Start()
    {
        //buttonsShop = FindObjectOfType<PNJ_Merchant_InstantiateButtons>();
    }

    //Active the skill
    public virtual void Activate(GameObject user)
	{
		if (cooldown > 0)
			return;

		cooldown = coolDownDuration;
	}

	//Activation by enemy, contains placement functions if needed
	public virtual IEnumerator EnemyUse(SimpleEnemy enemy)
	{
		yield return new WaitForSeconds(enemyLaunchTime * TickManager.tickDuration);
		Activate(enemy.gameObject);
		yield break;
	}

	public virtual IEnumerator useSkill(Vector3 pos)
	{
		yield break;
	}

	public virtual void OnDesequip()
	{

	}

	public bool CheckIfInLobby()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
			return true;
		else
			return false;
	}

	private void Update()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
	}

    public virtual void PowerUsed()
    {
        nbOfUse--;
        if (nbOfUse == 0)
        {
            FindObjectOfType<SkillBar>().PlayerSkills[itemSlot] = null;
            /*skillsShop = FindObjectOfType<PNJ_DoorKeeper_MySkills>();
            skillsShop.UpdateMySkillNumber();
            buttonsShop.ResetShopButtons();*/
            Destroy(gameObject);
        }
    }

}
