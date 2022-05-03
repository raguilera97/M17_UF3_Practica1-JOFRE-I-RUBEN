using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSoldierState : iStateWarrior
{
    private float timeSinceLastAttack = Mathf.Infinity;

    public void OnEnterState(iaSoldier character)
    {
        
    }

    public void OnExitState(iaSoldier character)
    {
        
    }

    public iStateWarrior OnUpdate(iaSoldier character)
    {
        timeSinceLastAttack += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(character.target.position, character.transform.position);


        if (distanceToPlayer > character.attackDistance)
        {
            return new PursueSoldierState();
        }



        if (timeSinceLastAttack > character.attackCooldown)
        {
            Debug.Log("Attack");
            timeSinceLastAttack = 0.0f;
        }

        return null;
    }
}
