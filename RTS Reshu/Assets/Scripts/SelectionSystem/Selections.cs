using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selections : MonoBehaviour
{
    public List<Unit> unitList = new List<Unit>();
    public List<Unit> unitSelected = new List<Unit>();
    public Building building;
    public Resource resource;
    

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
    }

    

    public void ShiftClickSelect(Unit unitAdd)
    {
        if (!unitSelected.Contains(unitAdd))
        {
            unitSelected.Add(unitAdd);
            unitAdd.Selected();
        }
        else
        {
            unitSelected.Remove(unitAdd);
            unitAdd.Unselected();
        }
    }

    public void DragSelect (Unit unitAdd)
    {
        if (!unitSelected.Contains(unitAdd))
        {
            unitSelected.Add(unitAdd);
            unitAdd.Selected();
        }
    }

    public void DeselectAll()
    {

        foreach(Unit unit in unitSelected)
        {
            unit.Unselected();
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

    public void BuildingClick(Building buildingSelect)
    {
        building = buildingSelect;
        building.Selected();
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
