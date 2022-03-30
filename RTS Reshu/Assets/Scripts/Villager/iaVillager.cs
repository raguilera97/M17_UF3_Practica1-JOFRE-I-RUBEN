using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iaVillager : MonoBehaviour
{
    private bool mining = false;
    private bool goAlmacen = false;
    private bool gathering = false;

    Almacen almacen;
    NavMeshAgent agent;
    public Resource resourceSelect;
    VillagerController villager;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        villager = GetComponent<VillagerController>();
        almacen = FindObjectOfType<Almacen>();
    }

    
    void Update()
    {
        DistanceAlmacenCheckout();
        DistanceMiningCheckout();
        DistanceGatheringCheckout();
    }

    public void Mining(Resource resource)
    {
        gathering = false;
        villager.SetGatheringAnimation(false);
        agent.destination = resource.transform.position;
        resourceSelect = resource;
        mining = true;
    }

    public void Gathering(Resource resource)
    {
        mining = false;
        villager.SetMiningAnimation(false);
        villager.SetGatheringAnimation(false);
        agent.destination = resource.transform.position;
        resourceSelect = resource;
        gathering = true;
    }

    private void DistanceGatheringCheckout()
    {
        if(gathering == true && agent.remainingDistance <1 && agent.remainingDistance != 0)
        {
            goAlmacen = false;
            villager.SetGatheringAnimation(true);
        }
    }

    private void DistanceMiningCheckout()
    {
        if (mining == true && agent.remainingDistance < 1 && agent.remainingDistance != 0)
        {
            goAlmacen = false;
            villager.SetMiningAnimation(true);
        }
    }

    private void DistanceAlmacenCheckout()
    {
        if (goAlmacen == true && agent.remainingDistance < 1 && agent.remainingDistance != 0)
        {
            almacen.FillAlmacen(villager.backpack);
            villager.backpack[0] = 0;
            villager.backpack[1] = 0;
            if (resourceSelect != null)
            {
                if (resourceSelect.id.Equals("Rock"))
                {
                    Mining(resourceSelect);
                }
                else if (resourceSelect.id.Equals("Bush"))
                {
                    Gathering(resourceSelect);
                }

            }
        }
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
                Almacenar();
            }

        }
        else
        {
            Destroy(resourceSelect.gameObject);
            Almacenar();
        }
    }

    private void Almacenar()
    {
        setgoAlmacen(true);
        villager.SetMiningAnimation(false);
        setMining(false);
        villager.SetGatheringAnimation(false);
        setGathering(false);
        agent.SetDestination(almacen.transform.position);
    }

    public void setMining (bool status)
    {
        mining = status;
    }

    public void setgoAlmacen(bool status)
    {
        goAlmacen = status;
    }

    public void setGathering(bool status)
    {
        gathering = status;
    }

    
}
