using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    [SerializeField] SelectableObject townhallRingSelector;
    [SerializeField] GameObject townhallHUDPanel;
    [SerializeField] Transform spawnVillager;
    [SerializeField] GameObject villager;

    [SerializeField] SelectableObject tavernRingSelector;
    [SerializeField] GameObject tavernHUDPanel;
    [SerializeField] Transform spawnWarrior;
    [SerializeField] GameObject warrior;

    Almacen almacen;

    public GameObject townhallErrMess;
    public GameObject tavernErrMess;

    private bool townhallItsSelected = false;
    private bool tavernItsSelected = false;

    void Start()
    {
        almacen = FindObjectOfType<Almacen>();

    }

    public void Selected()
    {
        if (this.name.Contains("townhall")){

        townhallRingSelector.TurnOnSelector();
        townhallItsSelected = true;
        townhallHUDPanel.SetActive(true);

        }
        else
        {
        tavernRingSelector.TurnOnSelector();
        tavernItsSelected = true;
        tavernHUDPanel.SetActive(true);
        }
    }

    public void Unselected()
    {
        townhallRingSelector.TurnOffSelector();
        townhallItsSelected = false;
        townhallHUDPanel.SetActive(false);

        tavernRingSelector.TurnOffSelector();
        tavernItsSelected = false;
        tavernHUDPanel.SetActive(false);

    }

    public void SpawnVillager()
    {
        //Instantiate(villager, spawnVillager.position, Quaternion.identity);

        if (almacen.food > 20)
        {

            VilSpawn();
        }
        else
        {
            townhallErrMess.SetActive(true);
            StartCoroutine("townMessageDis");
        }
    }

    public void VilSpawn()
    {
        Instantiate(villager, spawnVillager.position, Quaternion.identity);
        almacen.food -= 20;
    }

    IEnumerator townMessageDis()
    {
        yield return new WaitForSeconds(1f);
        townhallErrMess.SetActive(false);
    }

    public void SpawnWarrior()
    {
        //Instantiate(villager, spawnVillager.position, Quaternion.identity);

        if (almacen.food > 20 && almacen.rock > 10)
        {

            WarSpawn();
        }
        else
        {
            tavernErrMess.SetActive(true);
            StartCoroutine("tavernMessageDis");
        }
    }

    public void WarSpawn()
    {

        Instantiate(warrior, spawnWarrior.position, Quaternion.identity);
        almacen.food -= 20;
    }

    IEnumerator tavernMessageDis()
    {
        yield return new WaitForSeconds(1f);
        tavernErrMess.SetActive(false);
    }
}
