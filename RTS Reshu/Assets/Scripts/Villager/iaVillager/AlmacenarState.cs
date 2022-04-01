using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmacenarState : iState
{
    public void OnEnterState(iaVillager ia)
    {
        ia.almacen.FillAlmacen(ia.villager.backpack);
        ia.villager.backpack[0] = 0;
        ia.villager.backpack[1] = 0;
    }

    public void OnExitState(iaVillager ia)
    {
        
    }

    public iState OnUpdate(iaVillager ia)
    {

        if (ia.resourceSelect != null)
        {
            return new GoGatheringState();
        }
        else
        {
            return new StateIdle();
        }
    }
}
