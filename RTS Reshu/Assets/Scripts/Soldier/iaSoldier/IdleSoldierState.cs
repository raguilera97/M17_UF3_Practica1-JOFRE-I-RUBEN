using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSoldierState : iStateWarrior
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

        if (distanceToPlayer < character.pursueDistance)
        {
            return new PursueSoldierState();
        }

        return null;
    }
}
