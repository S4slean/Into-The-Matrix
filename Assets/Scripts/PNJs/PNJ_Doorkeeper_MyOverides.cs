using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ_Doorkeeper_MyOverides : MonoBehaviour
{
    public PlayerStats ps;
    public MoneyBank mb;

    public int PriceEnemy;
    public int PriceTraps;
    public int PriceSpawn;

    public Image icon;
    public Text title;
    public Text desc;
    public int indexnb;
    public GameObject popup;

    public List<Sprite> icons;

    // Start is called before the first frame update
    void Start()
    {
        ps = FindObjectOfType<PlayerStats>();
    }

    public void FillPopup(int index){

        popup.SetActive(true);
        indexnb = index;
        if(index == 0)
        {
            icon.sprite = icons[index];
            title.text = "Overide enemies";
            desc.text = "Disable all enemies and turrets of a room in the dungeon";
        }
        else if(index == 1)
        {
            icon.sprite = icons[index];
            title.text = "Overide traps";
            desc.text = "Disable all traps and lightning gates of a room in the dungeon";
        }
        else if(index == 2)
        {
            icon.sprite = icons[index];
            title.text = "Overide Cabin";
            desc.text = "Replace one room with a cabin room in the dungeon";
        }
    }

    public void BuyOveride()
    {
        if(indexnb == 0 && mb.BankMoney >= PriceEnemy)
        {
            popup.SetActive(false);
            mb.BankMoney -= PriceEnemy;
            mb.ActualizeBankMoney();
            ps.enmyOvrd += 1;
        }
        else if (indexnb == 1 && mb.BankMoney >= PriceTraps)
        {
            popup.SetActive(false);
            mb.BankMoney -= PriceTraps;
            mb.ActualizeBankMoney();
            ps.trapOvrd += 1;
        }
        else if (indexnb == 2 && mb.BankMoney >= PriceSpawn)
        {
            popup.SetActive(false);
            mb.BankMoney -= PriceSpawn;
            mb.ActualizeBankMoney();
            ps.phoneOvrd += 1;
        }
    }

    public void closePopup()
    {
        popup.SetActive(false);
    }
}
