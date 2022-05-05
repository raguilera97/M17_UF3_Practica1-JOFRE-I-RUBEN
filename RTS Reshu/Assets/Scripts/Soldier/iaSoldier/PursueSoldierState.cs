using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueSoldierState : iStateWarrior
{
    public void OnEnterState(iaSoldier war)
    {
        war.agent.SetDestination(war.unitToAttack.transform.position);
    }

    public iStateWarrior OnUpdate(iaSoldier war)
    {
        float distanceToTarget = Vector3.Distance(war.unitToAttack.transform.position, war.transform.position);

        if (distanceToTarget < war.attackDistance)
        {
            
            return new AttackSoldierState();
        }

        return null;
    }

    public void OnExitState(iaSoldier character)
    {

    }
}
