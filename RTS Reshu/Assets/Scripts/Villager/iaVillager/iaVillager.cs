using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iaVillager : MonoBehaviour
{
    iState currentState = new StateIdle();

    public Almacen almacen;
    public NavMeshAgent agent;
    public Resource resourceSelect;
    public VillagerController villager;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        villager = GetComponent<VillagerController>();
        almacen = FindObjectOfType<Almacen>();
    }

    private void Update()
    {
        if(currentState!= null)
        {
            iState nexState = currentState.OnUpdate(this);
            if (nexState != null)
            {
                currentState.OnExitState(this);
                currentState = nexState;
                currentState.OnEnterState(this);
            }
        }
        
    }

    public void ChangeState(iState state)
    {
        currentState.OnExitState(this);
        currentState = state;
        currentState.OnEnterState(this);
    }

    public void OrderGathering(Resource resource)
    {
        resourceSelect = resource;
        iState newState = new GoGatheringState();
        ChangeState(newState);
    }

    public void OrderIdle()
    {
        iState newState = new StateIdle();
        ChangeState(newState);
    }

    public void Recollect()
    {

        if (resourceSelect.resource > 0)
        {
            resourceSelect.resource -= 10;
            if (villager.backpack[0] + villager.backpack[1] + 10 <= villager.backpackSpace)
            {
                if (resourceSelect.id.Equals("Rock"))
                {
                    villager.backpack[0] += 10;
                }
                else if (resourceSelect.id.Equals("Bush"))
                {
                    villager.backpack[1] += 10;
                }

            }
            else
            {
                villager.SetMiningAnimation(false);
                villager.SetGatheringAnimation(false);
                iState newState = new GoAlmacenState();
                ChangeState(newState);
            }

        }
        else
        {
            villager.SetMiningAnimation(false);
            villager.SetGatheringAnimation(false);
            iState newState = new GoAlmacenState();
            ChangeState(newState);
        }

    }

}
