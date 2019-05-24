using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Skill
{
    GameObject skillUser;
    private new Collider collider;

    public float counterTimerMaxInTicks;
    public float counterTimerInTicks;

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

        collider = skillUser.GetComponent<CapsuleCollider>();
        //instance = Instantiate(Resources.Load("Buffs/GlitchParticles") as GameObject, user.transform);

        if (skillUser.tag == "Player" && !isActive)
        {
            isActive = true;
            counterTimerInTicks = counterTimerMaxInTicks;
            //skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);
            WaitForTarget();
        }

        if (skillUser.tag == "Enemy")
        {
            isActive = true;
            StartCoroutine(WaitForAttack());
        }
    }


    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (!isActive)
            return;
        if (isActive)
        {
            if (TickManager.tick > TickManager.tickDuration)
            {
                counterTimerInTicks--;
                if (counterTimerInTicks == 0)
                {
                    isActive = false;
                    cooldown = coolDownDuration;
                }
            }
        }
    }

    public void WaitForTarget()
    {
    }

    public IEnumerator WaitForAttack()
    {
        isActive = false;
        SimpleEnemy enemy = skillUser.GetComponent<SimpleEnemy>();
        yield return new WaitForSeconds(enemyLaunchTime);
        if (Mathf.Abs(enemy.enemyToPlayer.x) > Mathf.Abs(enemy.enemyToPlayer.z))
        {
        }
        else
        {
        }
        yield break;
    }

    public override IEnumerator useSkill(Vector3 pos)
    {
        if (skillUser.tag == "Player")
            yield return new WaitForSeconds(TickManager.tickDuration);
        //Ici on mettra l'animation/FX de réapparition

        if (skillUser.GetComponent<SimpleEnemy>() != null)
            skillUser.GetComponent<SimpleEnemy>().StartCoroutine(skillUser.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));

        yield break;

    }

    public void Desactivation()
    {
        //if (!isActive)
        //break;
        if (skillUser.tag == "Player")
        {
            skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
        }
        collider.enabled = true;
        isActive = false;

    }

    public override void OnDesequip()
    {
        FindObjectOfType<CharaController>().SetPlayerMovement(true, true);


        isActive = false;
    }
}
