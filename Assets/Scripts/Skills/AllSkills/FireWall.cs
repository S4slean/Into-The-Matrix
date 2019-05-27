using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : Skill
{
    GameObject skillUser;
    private new Collider collider;

    public int damages;
    public int fireWallLength;
    public int squaresCrossed;
    public CharaController playerController;
    public GameObject fireWallSquare;

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
            playerController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CharaController>();
            CharaController player = skillUser.GetComponent<CharaController>();
            isActive = true;
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
            if (playerController.moveIsOver)
            {
                GameObject currentFireWallSquare = Instantiate(fireWallSquare, playerController.transform.position - playerController.transform.forward*2, Quaternion.identity);
                currentFireWallSquare.GetComponent<FireWallSquareBehavior>().damages = damages;
                squaresCrossed++;
                if (squaresCrossed == fireWallLength)
                {
                    isActive = false;
                    squaresCrossed = 0;
                    cooldown = coolDownDuration;
                }
                playerController.moveIsOver = false;
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
