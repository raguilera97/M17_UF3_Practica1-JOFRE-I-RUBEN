using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] SelectableObject ringSelector;
    [SerializeField] GameObject HUDPanel;
    [SerializeField] Transform spawnVillager;
    [SerializeField] GameObject villager;
    Almacen almacen;
    int resources;
    
    bool itsSelected = false;

    void Start()
    {
        //resources = almacen.food;
    }


    void Update()
    {

    }

    public void Selected()
    {
        ringSelector.TurnOnSelector();
        itsSelected = true;
        HUDPanel.SetActive(true);

    }

    public void Unselected()
    {
        ringSelector.TurnOffSelector();
        itsSelected = false;
        HUDPanel.SetActive(false);

    }

    public void SpawnVillager()
    {
        Instantiate(villager, spawnVillager.position, Quaternion.identity);

        /*if (resources >= 20) { 
        Instantiate(villager, spawnVillager.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("No podes.");
        }*/
    }
}
