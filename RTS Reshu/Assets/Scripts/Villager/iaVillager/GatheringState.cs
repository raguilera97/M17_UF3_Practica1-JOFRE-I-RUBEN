using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringState : iState
{
    public void OnEnterState(iaVillager ia)
    {
        ia.gameObject.transform.LookAt(ia.resourceSelect.transform);
        if (ia.resourceSelect.id.Equals("Rock"))
        {
            ia.villager.SetMiningAnimation(true);
        }
        else
        {
            ia.villager.SetGatheringAnimation(true);

        }
    }

    public void OnExitState(iaVillager ia)
    {
        if (ia.resourceSelect.id.Equals("Rock"))
        {
            ia.villager.SetMiningAnimation(false);
        }
        else
        {
            ia.villager.SetGatheringAnimation(false);

        }
    }

    public iState OnUpdate(iaVillager ia)
    {
        return null;   
    }
}
