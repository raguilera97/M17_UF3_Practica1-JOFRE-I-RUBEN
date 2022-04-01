using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selections : MonoBehaviour
{
    public List<Unit> unitList = new List<Unit>();
    public List<Unit> unitSelected = new List<Unit>();
    public Townhall building;
    public Resource resource;
    public GameObject unitHUD;
    public bool areVillagers = false;
    public bool areWarriors = false;


    private static Selections _instance;
    public static Selections Instance { get { return _instance; } }

    private void Awake()
    {
        
        if (_instance != null && Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SelectClick(Unit unitAdd)
    {
        DeselectAll();
        unitSelected.Add(unitAdd);
        unitAdd.Selected();
        unitHUD.SetActive(true);

        if (unitAdd.name.Contains("Villager"))
        {
            unitHUD.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (unitAdd.name.Contains("Warrior"))
        {
            unitHUD.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    

    public void ShiftClickSelect(Unit unitAdd)
    {
        if (!unitSelected.Contains(unitAdd))
        {
            unitSelected.Add(unitAdd);
            unitAdd.Selected();
            unitHUD.SetActive(true);

            if (unitAdd.name.Contains("Villager"))
            {
                unitHUD.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (unitAdd.name.Contains("Warrior")){
                unitHUD.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        else
        {
            unitSelected.Remove(unitAdd);
            unitAdd.Unselected();

            areVillagers = false;
            areWarriors = false;

            foreach (Unit unit in unitSelected)
            {
                if (unit.name.Contains("Villager"))
                {
                    areVillagers = true;
                }
                else if (unit.name.Contains("Warrior"))
                {
                    areWarriors = true;
                }
                
            }

            if (areVillagers == false && areWarriors == false)
            {
                unitHUD.transform.GetChild(1).gameObject.SetActive(false);
                unitHUD.transform.GetChild(2).gameObject.SetActive(false);
                unitHUD.SetActive(false);
            }
            else if (areVillagers == false)
            {
                unitHUD.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if(areWarriors == false)
            {
                unitHUD.transform.GetChild(2).gameObject.SetActive(false);
            }

            

            
        }
    }

    public void DragSelect (Unit unitAdd)
    {
        if (!unitSelected.Contains(unitAdd))
        {
            unitSelected.Add(unitAdd);
            unitAdd.Selected();

            foreach (Unit unit in unitSelected)
            {
                if (unit.name.Contains("Villager"))
                {
                    unitHUD.SetActive(true);
                    unitHUD.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (unit.name.Contains("Warrior"))
                {
                    unitHUD.SetActive(true);
                    unitHUD.transform.GetChild(2).gameObject.SetActive(true);
                }
            }
        }
    }

    public void DeselectAll()
    {

        foreach(Unit unit in unitSelected)
        {
            unit.Unselected();
            unitHUD.transform.GetChild(1).gameObject.SetActive(false);
            unitHUD.transform.GetChild(2).gameObject.SetActive(false);
            unitHUD.SetActive(false);

        }
        unitSelected.Clear();
        

        if(resource != null)
        {
            resource.Unselected();
            resource = null;
        }
        if (building != null)
        {
            building.Unselected();
            building = null;
        }
    }

    public void Deselect(GameObject objectDeselect)
    {

    }

    public void BuildingClick(Townhall buildingSelect)
    {
        building = buildingSelect;
        building.Selected();

        foreach (Unit unit in unitSelected)
        {
            unit.Unselected();
        }
        unitHUD.transform.GetChild(1).gameObject.SetActive(false);
        unitHUD.transform.GetChild(2).gameObject.SetActive(false);
        unitHUD.SetActive(false);
        unitSelected.Clear();
    }

    public void ResourceClick(Resource resourceSelect)
    {
        foreach (Unit unit in unitSelected)
        {
            if(unit.name.Contains("Villager"))
            {
                resource = resourceSelect;
                resource.Selected();
                
                unit.GetComponent<iaVillager>().OrderGathering(resource);
                
                StartCoroutine(waitDesactive());
                break;
            }
            
        }
        
    }

    IEnumerator waitDesactive()
    {
        yield return new  WaitForSeconds(0.2f);
        resource.Unselected();
    }

}
