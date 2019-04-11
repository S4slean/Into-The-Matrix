using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchOut : Skill
{
    public int distance = 4;
    public GameObject selectionArea;
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

        collider = skillUser.GetComponent<CapsuleCollider>();
        //collider.enabled = false;

        //instance = Instantiate(Resources.Load("Buffs/GlitchParticles") as GameObject, user.transform);
        isActive = true;

        if (skillUser.tag == "Player")
        {
            //skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);

            //WaitForTarget();
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
}
