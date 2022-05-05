using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : MonoBehaviour
{
    [SerializeField] SelectableObject ringSelector;
    [SerializeField] GameObject HUDPanel;
    [SerializeField] Transform spawnWarrior;
    [SerializeField] GameObject warrior;
    [SerializeField] GameObject shieldWarrior;

    public Almacen almacen;
    Townhall[] townhalls;
    Townhall townhall;

    // mensaje de error de buildingHUD (dentro del townhall)
    public GameObject errMess;

    private bool itsSelected = false;

    void Start()
    {
        
        townhalls = FindObjectsOfType<Townhall>();

        for(int i = 0; townhalls.Length > i; i++)
        {
            if(townhalls[i].gameObject.name.Contains("Ally") && this.gameObject.name.Contains("Ally"))
            {
                townhall = townhalls[i];
                break;
            }
            else if(townhalls[i].gameObject.name.Contains("Enemy") && this.gameObject.name.Contains("Enemy")){
                townhall = townhalls[i];
            }
        }

        almacen = townhall.gameObject.GetComponent<Almacen>();

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

    public void SpawnWarrior()
    {
        //Instantiate(villager, spawnVillager.position, Quaternion.identity);

        if (almacen.food > 20 && almacen.rock > 10 && (townhall.maxPopulation - townhall.currentPopulation) >= 1)
        {

            WarSpawn();
        }
        else
        {
            errMess.SetActive(true);
            StartCoroutine("messageDis");
        }
    }

    public void WarSpawn()
    {

        Instantiate(warrior, spawnWarrior.position, Quaternion.identity);
        almacen.food -= 20;
        almacen.rock -= 10;
        townhall.currentPopulation += 1;
    }

    public void ShieldSpawnWarrior()
    {
        //Instantiate(villager, spawnVillager.position, Quaternion.identity);

        if (almacen.food > 30 && almacen.rock > 20 && (townhall.maxPopulation - townhall.currentPopulation) >= 1)
        {

            ShieldWarSpawn();
        }
        else
        {
            errMess.SetActive(true);
            StartCoroutine("messageDis");
        }
    }

    public void ShieldWarSpawn()
    {

        Instantiate(shieldWarrior, spawnWarrior.position, Quaternion.identity);
        almacen.food -= 30;
        almacen.rock -= 20;
        townhall.currentPopulation += 1;
    }

    IEnumerator messageDis()
    {
        yield return new WaitForSeconds(1f);
        errMess.SetActive(false);
    }
}

