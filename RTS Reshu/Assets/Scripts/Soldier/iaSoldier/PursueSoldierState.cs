using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueSoldierState : iStateWarrior
{
    public void OnEnterState(iaSoldier character)
    {
        
    }

    public void OnExitState(iaSoldier character)
    {
        
    }

    public iStateWarrior OnUpdate(iaSoldier character)
    {
        float distanceToPlayer = Vector3.Distance(character.target.position, character.transform.position);

        if (distanceToPlayer < character.attackDistance)
        {
            return new AttackSoldierState();
        }

        if (distanceToPlayer > character.pursueDistance)
        {
            return new IdleSoldierState();
        }

        character.agent.SetDestination(character.target.position);

        return null;
    }
}
