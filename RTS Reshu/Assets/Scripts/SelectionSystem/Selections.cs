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
    public List<Unit> enemyUnitList = new List<Unit>();
    public List<Unit> unitSelected = new List<Unit>();
    public Townhall townhall;
    public Tavern tavern;
    public Resource resource;
    public GameObject unitHUD;
    public bool areVillagers = false;
    public bool areWarriors = false;
    public bool formTri = false;
    public bool formRec = false;

    public bool townhallIsSelected = false;
    public bool tavernllIsSelected = false;



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

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {

                formationPos = hitInfo.point;
                if(formTri != false)
                {
                    FormTri();
                }
                else if (formRec != false)
                {
                    FormRec();
                }
                else
                {
                    FormQuad();
                }


            }
        }
    }

    public void FormQuad()
    {
        formTri = false;
        formRec = false;
        int contUnit = 0;
        double lCuadrado = 0;

        lCuadrado = Math.Ceiling(Math.Sqrt(unitSelected.Count));

        foreach (Unit unit in unitSelected)
        {
            if (unit.name.Contains("Villager"))
            {
                unit.gameObject.GetComponent<iaVillager>().OrderIdle();
            }
            else
            {
                unit.gameObject.GetComponent<iaSoldier>().OrderIdle();
                unit.gameObject.GetComponent<iaSoldier>().itsMoving = true;
            }

            contUnit++;
            if (contUnit > lCuadrado)
            {
                contUnit = 1;
                formationPos.x = hitInfo.point.x;
                formationPos += new Vector3(0, 0, 2);
            }

            unit.gameObject.GetComponent<NavMeshAgent>().destination = formationPos;
            formationPos += new Vector3(2, 0);
        }
    }

    public void FormTri()
    {
        formTri = true;
        formRec = false;
        List<Vector3> unitPositions = new List<Vector3>();

        // Offset starts at 0, then each row is applied change for half of spacing
        float currentRowOffset = 0f;
        float x, z;

        for (int row = 0; unitPositions.Count < unitSelected.Count; row++)
        {
            // Current unit positions are the index of first unit in row
            var columnsInRow = row + 1;
            var firstIndexInRow = unitPositions.Count;

            for (int column = 0; column < columnsInRow; column++)
            {
                x = column * 2 + currentRowOffset;
                z = row * 2;

                // Check if centering is enabled and if row has less than maximum
                // allowed units within the row.
                if (
                    row != 0 &&
                    firstIndexInRow + columnsInRow > unitSelected.Count)
                {
                    // Alter the offset to center the units that do not fill the row
                    var emptySlots = firstIndexInRow + columnsInRow - unitSelected.Count;
                    x += emptySlots / 2f * 2;
                }

                unitPositions.Add(new Vector3(x, 0, -z));

                if (unitPositions.Count >= unitSelected.Count) break;
            }

            currentRowOffset -= 2 / 2;
        }

        int count = 0;
        
        foreach (Unit unit in unitSelected)
        {
            formationPos = hitInfo.point;
            formationPos += unitPositions.ToArray()[count];
            unit.gameObject.GetComponent<NavMeshAgent>().destination = formationPos;
            count++;
        }

    }

    public void FormRec()
    {
        formRec = true;
        formTri = false;
        int countUnit = 0;

        foreach (Unit unit in unitSelected){

            if (unit.name.Contains("Villager"))
            {
                unit.gameObject.GetComponent<iaVillager>().OrderIdle();
            }
            else
            {
                unit.gameObject.GetComponent<iaSoldier>().OrderIdle();
                unit.gameObject.GetComponent<iaSoldier>().itsMoving = true;
            }

            countUnit++;

            if (countUnit > 7)
            {
                countUnit = 1;
                formationPos.x = hitInfo.point.x;
                formationPos += new Vector3(0, 0, -2);
            }

            unit.gameObject.GetComponent<NavMeshAgent>().destination = formationPos;
            formationPos += new Vector3(2, 0);

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
        if (townhall != null)
        {
            townhallIsSelected = false;
            townhall.Unselected();
            townhall = null;
        }

        if(tavern != null)
        {
            tavernllIsSelected = false;
            tavern.Unselected();
            tavern = null;
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
        if(townhall != null)
        {
            townhallIsSelected = false;
            townhall.Unselected();
        }else if (tavern != null)
        {
            tavernllIsSelected = false;
            tavern.Unselected();
        }

        tavern = null;
        townhall = null;
                
    }

    public void TownhallClick(Townhall townhallSelect)
    {
        if(tavernllIsSelected == true)
        {
            tavern.Unselected();
        }
        
        townhall = townhallSelect;
        townhall.Selected();
        townhallIsSelected = true;
         
        foreach (Unit unit in unitSelected)
        {
            unit.Unselected();
        }
        unitHUD.transform.GetChild(1).gameObject.SetActive(false);
        unitHUD.transform.GetChild(2).gameObject.SetActive(false);
        unitHUD.SetActive(false);
        unitSelected.Clear();
    }

    public void TavernClick(Tavern tavernSelect)
    {
        if(tavernllIsSelected == true)
        {
            tavern.Unselected();
        }
        if(townhallIsSelected == true)
        {
            townhall.Unselected();
        }
        
        tavern = tavernSelect;
        tavern.Selected();
        tavernllIsSelected = true;

        foreach (Unit unit in unitSelected)
        {
            unit.Unselected();
        }
        unitHUD.transform.GetChild(1).gameObject.SetActive(false);
        unitHUD.transform.GetChild(2).gameObject.SetActive(false);
        unitHUD.SetActive(false);
        unitSelected.Clear();
    }

    public void CombatClick(Unit unitTarget)
    {
        foreach (Unit unit in unitSelected)
        {
            if (!unit.name.Contains("Villager"))
            {
                
                unitTarget.Selected();

                unit.GetComponent<iaSoldier>().OrderAttack(unitTarget);

                //StartCoroutine(waitDesactive());

                Deselect(unitTarget);

                break;
            }

        }

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
