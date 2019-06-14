using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Skill
{
	public int distance = 4;
    public GameObject selectionArea;
    GameObject skillUser;
    GameObject instance;
    Vector3 bombPosition;
    public GameObject bombPrefab;
    public BombBehavior instantiatedBombScript;
    public bool bombThrown;
   

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

        if (skillUser.tag == "Player" && !bombThrown)
        {
            WaitForTarget();
        }
        else
        {
            instantiatedBombScript.Detonation();
            cooldown = coolDownDuration;
            bombThrown = false;
            transform.GetChild(0).GetComponent<Text>().text = this.name;
        }

        if (skillUser.tag == "Enemy")
        {
            StartCoroutine(WaitForAttack());
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
        if (!isActive)
        {
            isActive = true;
            CharaController player = skillUser.GetComponent<CharaController>();
            instance = Instantiate(selectionArea, player.transform.position + Vector3.up * .05f, Quaternion.identity, player.transform);
            instance.GetComponent<StarTarget>().distance = distance;
            instance.GetComponent<SelectionArea>().skill = this;
        }
        else if (isActive)
        {
            Desactivation();
        }
    }

    public IEnumerator WaitForAttack()
    {
        isActive = false;
        simpleEnemy enemy = skillUser.GetComponent<simpleEnemy>();
        yield return new WaitForSeconds(enemyLaunchTime);
        if (Mathf.Abs(enemy.enemyToPlayer.x) > Mathf.Abs(enemy.enemyToPlayer.z))
        {
            distance = Mathf.Abs(Mathf.RoundToInt(enemy.enemyToPlayer.x));
        }
        else
        {
            distance = Mathf.Abs(Mathf.RoundToInt(enemy.enemyToPlayer.z));
        }
        StartCoroutine(useSkill(bombPosition));
        yield break;
    }

    public override IEnumerator useSkill(Vector3 bombThrow)
    {
        while (TickManager.tick < TickManager.tickDuration)
        {
            yield return new WaitForEndOfFrame();
        }
        GameObject instantiatedBomb = Instantiate(bombPrefab, bombThrow + Vector3.up, Quaternion.identity);
        instantiatedBombScript = instantiatedBomb.GetComponent<BombBehavior>();
        bombThrown = true;
        Destroy(instance);
        transform.GetChild(0).GetComponent<Text>().text = "BOOM";


        if (skillUser.GetComponent<simpleEnemy>() != null)
            skillUser.GetComponent<simpleEnemy>().StartCoroutine(skillUser.GetComponent<simpleEnemy>().WaitForNewCycle(enemyRecoverTime));

        yield break;

    }

    public void Desactivation()
    {
        //if (!isActive)
        //break;

        Destroy(instance);
        if (skillUser.tag == "Player")
        {
            skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
        }
        isActive = false;

    }

    public override void OnDesequip()
    {
        if (instance == null)
            return;

        Destroy(instance);

        instantiatedBombScript.Detonation();
        bombThrown = false;
        transform.GetChild(0).GetComponent<Text>().text = this.name;


        isActive = false;
    }
}
