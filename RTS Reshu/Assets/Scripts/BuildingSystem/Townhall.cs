using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Townhall : MonoBehaviour
{
    [SerializeField] SelectableObject ringSelector;
    [SerializeField] GameObject HUDPanel;
    [SerializeField] Transform spawnVillager;
    [SerializeField] GameObject villager;

    Almacen almacen;    

    // mensaje de error de buildingHUD (dentro del townhall)
    public GameObject errMess;

    public int maxPopulation;
    public int currentPopulation;
    public Text tCurrentPop, tMaxPop;

    private bool itsSelected = false;

    void Start()
    {
        almacen = FindObjectOfType<Almacen>();
         
    }

    private void Update()
    {
        tCurrentPop.text = currentPopulation.ToString();
        tMaxPop.text = maxPopulation.ToString();
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
        //Instantiate(villager, spawnVillager.position, Quaternion.identity);

        if (almacen.food >= 20 && (maxPopulation - currentPopulation) >= 1) {
            
            Spawn();
        }
        else
        {
            errMess.SetActive(true);
            StartCoroutine("messageDis");
        }
    }

    public void Spawn()
    {
        Instantiate(villager, spawnVillager.position, Quaternion.identity);
        almacen.food -= 20;
        currentPopulation += 1;
        
    }

    IEnumerator messageDis()
    {
        yield return new WaitForSeconds(1f);
        errMess.SetActive(false);
    }
}
