using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    //Animator anim; Désactivé si besoin plus tard
    GameObject itemPrefab;
    GameObject player;
    public EquipmentList list;
    public Equipment equipment;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharaController>().gameObject;
        itemPrefab = Resources.Load("EquipmentItem") as GameObject;
        list = GameObject.Find("EquipmentList").GetComponent<EquipmentList>();
        //anim = GetComponent<Animator>(); Désactivé si besoin plus tard

        if (equipment == null)
            equipment = list.equipments[Random.Range(0, list.equipments.Count)];
    }

    //en cas de collision avec le joueur
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Player")
        {
            

            FindObjectOfType<EquipmentBar>().AddPlayerEquipment(equipment);                                                                                                                               //sinon Crée le boutton et détruit l'objet
            Destroy(gameObject);
        }
    }
}
