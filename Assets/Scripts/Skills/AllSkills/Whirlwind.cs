using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : Skill
{
    GameObject skillUser;
    private new Collider collider;
    public int distWhirlwindInSquares = 1;
    public int damages;

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
        //collider.enabled = false;

        //instance = Instantiate(Resources.Load("Buffs/GlitchParticles") as GameObject, user.transform);

        if (skillUser.tag == "Player")
        {
            //skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);

            WaitForTarget();
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
        StartCoroutine(useSkill(player.gameObject.transform.forward * -1));
    }

    public IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(enemyLaunchTime);
    }

    public override IEnumerator useSkill(Vector3 glitchDir)
    {
        this.skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);
        while (TickManager.tick < TickManager.tickDuration)
        {
            yield return new WaitForEndOfFrame();
        }
        RaycastHit hit;
        Physics.Raycast(skillUser.transform.position, glitchDir, out hit, distGlitch * 2, 9);

        this.skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
        isActive = false;
        cooldown = coolDownDuration;
        yield break;

    }

    public override void OnDesequip()
    {
        FindObjectOfType<CharaController>().SetPlayerMovement(true, true);


        isActive = false;
    }
}
