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
        Collider[] enemyArray = Physics.OverlapSphere(war.transform.position, range);
        foreach(Collider collider in enemyArray)
        {
            if(collider.TryGetComponent<Unit>(out Unit unit) && collider.gameObject != war.gameObject && !war.itsMoving)
            {
                if (unit.name.Contains("Warrior"))
                {
                    war.OrderAttack(unit);
                    break;
                }
            }
            
        }
        return null;
    }

    public void OnExitState(iaSoldier war)
    {
        
    }

}
