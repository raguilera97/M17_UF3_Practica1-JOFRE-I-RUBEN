using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSoldierState : iStateWarrior
{
    private float timeSinceLastAttack = 0;

    public void OnEnterState(iaSoldier character)
    {
        character.transform.LookAt(character.unitToAttack.transform);
        character.agent.ResetPath();
        character.attackCooldown = Random.Range(2f, 5f);
    }

    public iStateWarrior OnUpdate(iaSoldier war)
    {
        if(war.unitToAttack != null)
        {
            timeSinceLastAttack += Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(war.transform.position, war.unitToAttack.transform.position);


            if (distanceToPlayer > war.attackDistance)
            {
                return new PursueSoldierState();
            }

            if (timeSinceLastAttack > Mathf.Round(war.attackCooldown) && distanceToPlayer < war.attackDistance)
            {
                war.attackCooldown = Random.Range(3f, 5f);
                war.soldier.AttackAnimation();
                timeSinceLastAttack = 0.0f;
            }
        }
        else
        {
            return new IdleSoldierState();
        }
        

        return null;
    }

    public void OnExitState(iaSoldier character)
    {
        
    }
}
