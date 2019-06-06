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
            if (user.GetComponent<SimpleEnemy>() != null)
                user.GetComponent<SimpleEnemy>().StartCoroutine(user.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));
            return;
        }

        skillUser = user;

        if (skillUser.tag == "Player")
        {
			StartCoroutine(useSkill(Vector3.zero));
        }

        if (skillUser.tag == "Enemy")
        {
            //StartCoroutine(WaitForAttack());
        }
    }

    private void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        if (!isActive)
            return;
    }

    public void WaitForTarget()
    {
        CharaController player = skillUser.GetComponent<CharaController>();
        isActive = true;
        StartCoroutine(useSkill(player.gameObject.transform.forward));
    }

    public IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(enemyLaunchTime);
    }

    public override IEnumerator useSkill(Vector3 pos)
    {
        RaycastHit hit;
        Physics.Raycast(skillUser.transform.position + Vector3.up, skillUser.transform.forward, out hit, distGrap*2, 9);
		GameObject hook = Instantiate(hookPrefab, skillUser.transform.position + Vector3.up + skillUser.transform.forward * 1.2f, skillUser.transform.rotation);
		yield break;

    }



    public override void OnDesequip()
    {
        FindObjectOfType<CharaController>().SetPlayerMovement(true, true);
        isActive = false;
    }
}
