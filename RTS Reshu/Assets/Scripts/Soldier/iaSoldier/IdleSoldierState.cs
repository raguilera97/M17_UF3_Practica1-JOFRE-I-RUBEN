using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSoldierState : iStateWarrior
{
    private float range = 20;

    public void OnEnterState(iaSoldier war)
    {
        
    }

   public iStateWarrior OnUpdate(iaSoldier war)
    {
        /*Collider[] enemyArray = Physics.OverlapSphere(war.transform.position, range);
        foreach(Collider collider in enemyArray)
        {
            if(collider.TryGetComponent<Unit>(out Unit unit))
            {
                war.unitToAttack.transform.position = unit.transform.position;
                return new PursueSoldierState();
            }
        }*/

        return null;
    }

    public void OnExitState(iaSoldier war)
    {
        
    }

}
