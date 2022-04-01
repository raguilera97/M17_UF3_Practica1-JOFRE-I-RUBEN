using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoGatheringState : iState
{
    public void OnEnterState(iaVillager ia)
    {
        ia.agent.SetDestination(ia.resourceSelect.transform.position);
    }

    

    public iState OnUpdate(iaVillager ia)
    {
       

        if (ia.agent.remainingDistance < 1 && ia.agent.remainingDistance != 0)
        {

            return new GatheringState();

        }

        return null;
    }

    public void OnExitState(iaVillager ia)
    {

    }
}
