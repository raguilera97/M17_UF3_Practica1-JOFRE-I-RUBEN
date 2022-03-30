using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] SelectableObject ringSelector;
    [SerializeField] GameObject HUDPanel;
    [SerializeField] Transform spawnVillager;
    [SerializeField] GameObject villager;
    bool itsSelected = false;

    void Start()
    {
        
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
    }

    private void SpawnWarrior()
    {

    }
}
