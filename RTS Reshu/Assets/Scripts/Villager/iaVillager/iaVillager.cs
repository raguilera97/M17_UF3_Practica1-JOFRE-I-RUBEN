using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iaVillager : MonoBehaviour
{
    iState currentState = new StateIdle();

    public Almacen[] almacenes;
    public NavMeshAgent agent;
    public Almacen almacen;
    public Resource resourceSelect;
    public VillagerController villager;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        villager = GetComponent<VillagerController>();
        almacenes = FindObjectsOfType<Almacen>();
        for(int i = 0; almacenes.Length > i; i++)
        {
            if (almacenes[i].gameObject.name.Contains("Enemy") && villager.name.Contains("Enemy"))
            {
                almacen = almacenes[i].GetComponent<Almacen>();
                break;
            }
            else if (almacenes[i].gameObject.name.Contains("Ally") && villager.name.Contains("Ally"))
            {
                almacen = almacenes[i].GetComponent<Almacen>();
                break;
            }
        }
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
