using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAlmacenState : iState
{
    public void OnEnterState(iaVillager ia)
    {
        ia.agent.SetDestination(ia.almacen.transform.position);
    }

    public void OnExitState(iaVillager ia)
    {
        
    }

    public iState OnUpdate(iaVillager ia)
    { 

        if (ia.agent.remainingDistance < 1 && ia.agent.remainingDistance != 0)
        {
            return new AlmacenarState();
        }
         return null;
    }
}
