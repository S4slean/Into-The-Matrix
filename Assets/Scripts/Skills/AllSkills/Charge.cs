using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : Skill
{
    GameObject skillUser;
    private new Collider collider;
    public float chargedTimerMax;
    public float chargedTimer;

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
        isActive = true;

        if (skillUser.tag == "Player")
        {
            //skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);
            chargedTimer = chargedTimerMax;
            WaitForTarget();
        }

        if (skillUser.tag == "Enemy")
        {
            StartCoroutine(WaitForAttack());
        }
    }


    private void Update()
    {

        if (!isActive)
            return;
        if (isActive)
        {
            chargedTimer -= Time.deltaTime;
            if (chargedTimer <= 0)
            {
                isActive = false;
                cooldown = coolDownDuration;
            }
        }
    }

    public void WaitForTarget()
    {
        if (!isActive)
        {
            CharaController player = skillUser.GetComponent<CharaController>();
            isActive = true;
        }
        else if (isActive)
        {
            Desactivation();
        }
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

    public override IEnumerator useSkill(Vector3 tpPos)
    {
        cooldown = coolDownDuration;
        //Ici on mettra l'animation/FX de disparition
        skillUser.SetActive(false);
        if (skillUser.tag == "Player")
        { skillUser.GetComponent<CharaController>().lastMove = Vector3.zero; }
        yield return new WaitForSeconds(TickManager.tickDuration);
        //Ici on mettra l'animation/FX de réapparition
        skillUser.transform.position = tpPos;
        skillUser.SetActive(true);

        if (skillUser.tag == "Player")
        {
            this.skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
        }
        collider.enabled = true;

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
    {FindObjectOfType<CharaController>().SetPlayerMovement(true, true);


        isActive = false;
    }
}
