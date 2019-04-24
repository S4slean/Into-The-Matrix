using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ_Merchant_InstantiateButtons : MonoBehaviour
{
    public GameObject ShopButtonPrefab;
    public skillList SL;

    public PNJ_DoorKeeper_MySkills PDMS;

    // Start is called before the first frame update
    void Start()
    {
        CreateShopButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateShopButton() // Instancie les boutons du shop d'après les skills dans la liste
    {
        for (int i = 0; i < SL.skills.Count ; i++)
        {
            Debug.Log("instantiated a shop button");
            GameObject instance = Instantiate(ShopButtonPrefab, transform);

            instance.GetComponent<Button>().onClick.AddListener( () => PDMS.UnlockItem() );

            instance.name = SL.skills[i].name;                                                              //Donne le nom du skill au bouton (nécéssaire pour le script d'achat)
            instance.transform.GetChild(0).GetComponent<Image>().sprite = SL.skills[i].icon;                //Donne l'icone du skill au bouton
            instance.transform.GetChild(0).GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f);         //Grise l'icone du skill
            instance.transform.GetChild(2).GetComponent<Text>().text = SL.skills[i].cost.ToString();        //Donne the prix du skill au bouton

        }
    }
}
