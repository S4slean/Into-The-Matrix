using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{

	public GameObject SkillButtonPrefab;
    public PNJ_DoorKeeper_MySkills skillsShop;
    public PNJ_Merchant_InstantiateButtons buttonsShop;
	public List<Skill> PlayerSkills = new List<Skill>(3);


	//Création du boutton
    public void CreateButton(Skill skill)
	{
		if (PlayerSkills.Count > 3)							// si le joueur à déjà 3 skill ne fait rien
			return;

		bool gotThatSkill = false;

		for(int i =0; i<  PlayerSkills.Count; i++)
		{
			if (PlayerSkills[i] == null)
				continue;

			if(PlayerSkills[i].name == skill.name)
			{
				gotThatSkill = true;

				if (PlayerSkills[i].nbOfUse < 3)
				{
					PlayerSkills[i].nbOfUse++;
					
				}
			}

		}

		if (!gotThatSkill)
		{
			for(int i = 0; i < PlayerSkills.Count; i++)
			{
				if (PlayerSkills[i] == null)
				{
					GameObject Instance = Instantiate(SkillButtonPrefab, transform.GetChild(i));            //Crée le bouton
					SkillButton skillButton = Instance.GetComponent<SkillButton>();                         //récupère son script
					CopyComponent(skill, Instance);                                                         //attribue au Gameobject une copie du skill
					skill = Instance.GetComponent<Skill>();                                                 //met cette copie en référence dans le script
					skillButton.index = i;                                                                  //attribue au bouton son slot
					PlayerSkills[i] = skill;                                                                //ajoute le skill à la liste des skill équipé par le joueur
					/*Instance.transform.GetChild(0).GetComponent<Text>().text = skill.name;  */                //Edite le texte du bouton avec le nom du skill
					Instance.transform.GetChild(1).GetComponent<Text>().text = skill.nbOfUse.ToString();
					skill.itemSlot = i;
					break;
				}

			}
		}
	}

	//Permet de copier un component sur un autre gameobject
	Component CopyComponent(Component original, GameObject destination)
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		// Copied fields can be restricted with BindingFlags
		System.Reflection.FieldInfo[] fields = type.GetFields();
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return copy;
	}

	public void DesequipAll()
	{
		for(int i = 0; i < 3; i++)
		{
            
            
					
			if(PlayerSkills[i] != null)
				Destroy(PlayerSkills[i].gameObject);
            PlayerSkills[i] = null;

            
        }

        skillsShop = FindObjectOfType<PNJ_DoorKeeper_MySkills>();
        //buttonsShop = GameObject.Find("HubUIv2").transform.GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetComponent<PNJ_Merchant_InstantiateButtons>();
        skillsShop.UpdateMySkillNumber();
        buttonsShop.ResetShopButtons();
    }
}
