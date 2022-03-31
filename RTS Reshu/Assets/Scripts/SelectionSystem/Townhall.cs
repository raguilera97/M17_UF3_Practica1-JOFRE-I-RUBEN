using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townhall : MonoBehaviour
{
    [SerializeField] SelectableObject ringSelector;
    [SerializeField] GameObject HUDPanel;
    [SerializeField] Transform spawnVillager;
    [SerializeField] GameObject villager;


    Almacen almacen;
    int food;
    
    // mensaje de error de buildingHUD (dentro del townhall)
    public GameObject errMess;

    bool itsSelected = false;

    void Start()
    {
        almacen = FindObjectOfType<Almacen>();
         food = almacen.food; 
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

        if (food >= 20) { 
        Instantiate(villager, spawnVillager.position, Quaternion.identity);
            food -= 20;
            Debug.Log(food);
        }
        else
        {
            errMess.SetActive(true);
            StartCoroutine("messageDis");
        }
    }

    IEnumerator messageDis()
    {
        yield return new WaitForSeconds(1f);
        errMess.SetActive(false);
    }
}
