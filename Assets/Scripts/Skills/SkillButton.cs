using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
	public int index;
	GameObject player;
	GameObject itemPrefab;
	RectTransform rectTransform;
	SkillBar skillBar;
	Button btn;
	Image cooldownIMG;
	public Skill skill;
	public Text chargeUI;


	private void Start()
	{
		itemPrefab = Resources.Load("SkillItem" ) as GameObject;
		player = FindObjectOfType<CharaController>().gameObject;
		rectTransform = GetComponent<RectTransform>();
		skillBar = FindObjectOfType<SkillBar>();
		btn = GetComponent<Button>();
		cooldownIMG = transform.GetChild(1).GetComponent<Image>();
		skill = GetComponent<Skill>();

		btn.onClick.AddListener(delegate { skill.Activate(player); });												//Ajout de la fonction du skill à l'event du bouton
	}

	//Déplacement du skill lors d'un Drag
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	//Relachement du Drag
	public void OnEndDrag(PointerEventData eventData)
	{
		//Si relaché dans la scène: Drop l'item 
		if(Input.mousePosition.y > 285)
		{
			skillBar.PlayerSkills[index].OnDesequip();
			skillBar.PlayerSkills[index] = null; ;

			GameObject instance = Instantiate(itemPrefab, player.transform.position + Vector3.up, Quaternion.Euler(0,0,0));							//Instancie l'objet
			instance.GetComponent<SkillItem>().skill = skill;
			if(!Physics.Raycast(player.transform.position + Vector3.up, player.transform.forward, 2))
				instance.GetComponent<SkillItem>().StartCoroutine(instance.GetComponent<SkillItem>().Drop(8, player.transform.forward));			//Le drop devant s'il n' y a rien
			else if (!Physics.Raycast(player.transform.position + Vector3.up, player.transform.right, 2))
				instance.GetComponent<SkillItem>().StartCoroutine(instance.GetComponent<SkillItem>().Drop(8, player.transform.right));				//puis dans les autres direction si obstrué
			else if (!Physics.Raycast(player.transform.position + Vector3.up, -player.transform.right, 2))
				instance.GetComponent<SkillItem>().StartCoroutine(instance.GetComponent<SkillItem>().Drop(8, -player.transform.right));
			else if (!Physics.Raycast(player.transform.position + Vector3.up, -player.transform.forward, 2))
				instance.GetComponent<SkillItem>().StartCoroutine(instance.GetComponent<SkillItem>().Drop(8, -player.transform.forward));

			Destroy(gameObject);																													//Fait Disparaître le bouton
		}
		else
		{
			rectTransform.anchoredPosition = Vector3.zero;																							//Si relaché dans l'UI il retourne à sa place
		}
	}

	private void Update()
	{
		cooldownIMG.fillAmount = Mathf.Clamp01(skill.cooldown/skill.coolDownDuration);									//Affiche le cooldown
	}
}
