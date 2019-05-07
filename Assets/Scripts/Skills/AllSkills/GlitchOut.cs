using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchOut : Skill
{
    public bool glitching;
    GameObject skillUser;
    GameObject instance;
    Vector3 dodgeDir;
    private new Collider collider;

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
        glitching = true;
        collider = skillUser.GetComponent<CapsuleCollider>();
        //collider.enabled = false;

        //instance = Instantiate(Resources.Load("Buffs/GlitchParticles") as GameObject, user.transform);
        isActive = true;

        if (skillUser.tag == "Player")
        {
            //skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);

            //WaitForTarget();

            glitching = true;
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
        instance.GetComponent<SelectionArea>().skill = this;
    }

    public IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(enemyLaunchTime);
    }

    public override IEnumerator useSkill(Vector3 dodgePos)
    {
        Debug.Log("SkillUse");
        while (TickManager.tick < TickManager.tickDuration)
        {
            yield return new WaitForEndOfFrame();
        }
        cooldown = coolDownDuration;
        //Ici on mettra l'animation/FX de disparition
        skillUser.SetActive(false);
        if (skillUser.tag == "Player")
        {skillUser.GetComponent<CharaController>().lastMove = Vector3.zero;}
        yield return new WaitForSeconds(TickManager.tickDuration);
        //Ici on mettra l'animation/FX de réapparition
        skillUser.transform.position = dodgePos;
        skillUser.SetActive(true);

        if (skillUser.tag == "Player")
        {
            this.skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
        }
        Destroy(instance);
        collider.enabled = true;

        if (skillUser.GetComponent<SimpleEnemy>() != null)
            skillUser.GetComponent<SimpleEnemy>().StartCoroutine(skillUser.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));

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
        collider.enabled = true;
        isActive = false;

    }

    public override void OnDesequip()
    {
        if (instance == null)
            return;

        Destroy(instance);


        FindObjectOfType<CharaController>().SetPlayerMovement(true, true);


        isActive = false;
    }
}
