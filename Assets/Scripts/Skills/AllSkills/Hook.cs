using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : Skill
{
    GameObject skillUser;
    private new Collider collider;
    public int distGrap = 4;
    public GameObject hookPrefab;


    [SerializeField] bool isActive = false;

    public override void Activate(GameObject user)
    {
        if (CheckIfInLobby())
            return;

        if (user.tag == "Player" && Input.mousePosition.y > 285)
            return;

        if (cooldown > 0)
        {
            if (user.GetComponent<simpleEnemy>() != null)
                user.GetComponent<simpleEnemy>().StartCoroutine(user.GetComponent<simpleEnemy>().WaitForNewCycle(enemyRecoverTime));
            return;
        }

        skillUser = user;

        if (skillUser.tag == "Player")
        {
			StartCoroutine(useSkill(Vector3.zero));
        }


    }

    private void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        if (!isActive)
            return;
    }


    public override IEnumerator useSkill(Vector3 pos)
    {
		charge--;
        RaycastHit hit;
        Physics.Raycast(skillUser.transform.position + Vector3.up, skillUser.transform.forward, out hit, distGrap*2, 9);
		GameObject hook = Instantiate(hookPrefab, skillUser.transform.position + Vector3.up + skillUser.transform.forward * 1.2f, skillUser.transform.rotation);
		cooldown = coolDownDuration;
		PowerUsed();
		yield break;

    }



    public override void OnDesequip()
    {
        FindObjectOfType<CharaController>().SetPlayerMovement(true, true);
        isActive = false;
    }
}
