using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Selections : MonoBehaviour
{

    RaycastHit hitInfo = new RaycastHit();
    public Vector3 formationPos = new Vector3(0, 0);

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

    private void Update()
    {
        moveUnits();
    }

    public void moveUnits()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int contUnit = 0;
            double lCuadrado = 0;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {

                formationPos = hitInfo.point;
                lCuadrado = Math.Ceiling(Math.Sqrt(unitSelected.Count));

                foreach (Unit unit in unitSelected)
                {
                    if (unit.name.Contains("Villager"))
                    {
                        unit.gameObject.GetComponent<iaVillager>().OrderIdle();
                    }

                    contUnit++;
                    if (contUnit > lCuadrado)
                    {
                        contUnit = 1;
                        formationPos.x = hitInfo.point.x;
                        formationPos += new Vector3(0, 0, -2);
                    }
                    

                    Debug.Log(formationPos);

                    unit.gameObject.GetComponent<NavMeshAgent>().destination = formationPos;
                    formationPos += new Vector3(2, 0);
                }
            }
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
        DeselectBuilding();


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
        DeselectBuilding();
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

    public void Deselect(Unit unit)
    {
        bool vill = false;
        bool war = false;

        unit.Unselected();
        unitSelected.Remove(unit);

        foreach (Unit unitL in unitSelected)
        {
            if (unitL.name.Contains("Villager")){
                vill = true;
            }
            if (unitL.name.Contains("Warrior"))
            {
                war = true;
            }
        }

        if(!vill && !war)
        {
            unitHUD.transform.GetChild(1).gameObject.SetActive(false);
            unitHUD.transform.GetChild(2).gameObject.SetActive(false);
            unitHUD.SetActive(false);
        }
        else if (!vill)
        {
            unitHUD.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (!war)
        {
            unitHUD.transform.GetChild(2).gameObject.SetActive(false);
        }

    }

    public void DeselectBuilding()
    {
        if(building != null)
        {
            building.Unselected();
        }
        building = null;
        
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

                Deselect(unit);

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
