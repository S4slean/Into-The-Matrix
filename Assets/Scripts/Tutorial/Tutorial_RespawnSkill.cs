using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_RespawnSkill : MonoBehaviour
{
    public GameObject skill;
    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

            Debug.Log("Tag = " + other.tag.ToString());
            if( other.GetComponent<CharaController>() != null
                && FindObjectOfType<SkillBar>().PlayerSkills.Count != 0)
            {
                Instantiate(skill, spawnPosition, Quaternion.identity);
            }


    }

    public void RespawnSkill()
    {

    }
}
