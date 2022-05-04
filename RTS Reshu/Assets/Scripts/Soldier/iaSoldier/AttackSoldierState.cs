using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSoldierState : iStateWarrior
{
    private float timeSinceLastAttack = Mathf.Infinity;

    public void OnEnterState(iaSoldier character)
    {
        
    }

    public iStateWarrior OnUpdate(iaSoldier war)
    {
        timeSinceLastAttack += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(war.transform.position, war.unitToAttack.transform.position);


       /* if (distanceToPlayer > war.attackDistance)
        {
            return new PursueSoldierState();
        }*/
        if(distanceToPlayer < war.attackDistance)
        {
            war.soldier.AttackAnimation();
        }


        war.attackCooldown = Random.Range(1f, 3f);

        if (timeSinceLastAttack > war.attackCooldown)
        {
            
            timeSinceLastAttack = 0.0f;
        }

        return null;
    }

    public void OnExitState(iaSoldier character)
    {
        
    }
}
