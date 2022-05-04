using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleSystem : MonoBehaviour
{
    private float timeSinceLastEvaluation;
    private float evaluationRate = 5.0f;
    public ControladorInfo controladorIA;

    private delegate bool CheckCondition();
    private delegate void PerformAction();

    List<CheckCondition> conditions = new List<CheckCondition>();
    List<PerformAction> actions = new List<PerformAction>();

    List<Unit> civils;

    private void Awake()
    {
        conditions.Add(CheckCondition1);
        conditions.Add(CheckCondition2);
        conditions.Add(CheckCondition3);
        conditions.Add(CheckCondition4);
        conditions.Add(CheckCondition5);
        conditions.Add(CheckCondition6);
        conditions.Add(CheckCondition7);
        conditions.Add(CheckCondition8);
        conditions.Add(CheckCondition9);

        actions.Add(PerformAction1);
        actions.Add(PerformAction2);
        actions.Add(PerformAction3);
        actions.Add(PerformAction4);
        actions.Add(PerformAction5);
        actions.Add(PerformAction6);
        actions.Add(PerformAction7);
        actions.Add(PerformAction8);
        actions.Add(PerformAction9);
    }

    private void Start()
    {
        float healthNow = controladorIA.healthBase;
        float woodNow = controladorIA.wood;
        float stoneNow = controladorIA.stone;
        float ironNow = controladorIA.iron;
        float foodNow = controladorIA.food;
        float civilsNow = controladorIA.currentCivils;
        civils = controladorIA.unitats.unitList;
        
        Debug.Log("Unitats desplegades" + civils.Count);
        Debug.Log("Vida Base: " + healthNow);
        Debug.Log("Fusta: " + woodNow);
        Debug.Log("Pedra: " + stoneNow);
        Debug.Log("Ferro: " + ironNow);
        Debug.Log("Menjar: " + foodNow);
        Debug.Log("Civils spawned: " + civilsNow);
    }

    private void Update()
    {
        timeSinceLastEvaluation += Time.deltaTime;

        if(timeSinceLastEvaluation > evaluationRate)
        {
            timeSinceLastEvaluation = 0.0f;
            Evaluate();
        }
        
    }

    private void Evaluate()
    {
        for(int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i]())
            {
                actions[i]();
                //return;
            }
        }
    }

    // Obligació d'obtenir Multicontrol de recurs
    private bool CheckCondition1()
    {
        bool needResources = false;

        if (controladorIA.ajuntamentRecursos.food < controladorIA.maxFood || controladorIA.ajuntamentRecursos.rock < controladorIA.maxStone)
        {
            needResources = true;
        }

        return needResources;
    }

    // Creació de nous civils
    private bool CheckCondition2()
    {
        bool needVillagers = false;

        if (controladorIA.food > 25 && controladorIA.currentCivils < controladorIA.maxCivils)
        {
            needVillagers = true;
        }

        return needVillagers;
    }

    // 
    private bool CheckCondition3()
    {
        bool toolWood = false;

        if (controladorIA.wood < controladorIA.maxWood && controladorIA.currentCivils >= 4 && controladorIA.currentCivils != 0)
        {
            toolWood = true;
        }

        return toolWood;
    }

    // 
    private bool CheckCondition4()
    {
        bool pickStone = false;

        if (controladorIA.ajuntamentRecursos.rock < controladorIA.maxStone && controladorIA.currentCivils >= 8 && controladorIA.currentCivils != 0)
        {
            pickStone = true;
            
        } /*else
        {
            foreach(Unit unit in civils)
            {
                unit.GetComponent<iaVillager>().OrderIdle();
                
            }
            
        }*/
        return pickStone;
    }

    // 
    private bool CheckCondition5()
    {
        bool pickIron = false;

        if (controladorIA.iron < controladorIA.maxIron && controladorIA.currentCivils > 10 && controladorIA.currentCivils != 0)
        {
            pickIron = true;
        }

        return pickIron;
    }

    // Generar Soldats
    private bool CheckCondition6()
    {
        bool needSoldiers = false;

        float percentSpawnSoldier = UnityEngine.Random.Range(0.0f, 1.0f);

        if (controladorIA.food > 50 && controladorIA.stone > 100 && controladorIA.currentSoldiers < controladorIA.maxSoldiers && percentSpawnSoldier < 0.2f)
        {
            needSoldiers = true;
        }

        return needSoldiers;
    }

    // Atacar a la base del player
    private bool CheckCondition7()
    {
        bool attackBasePlayer = false;

        float percent = UnityEngine.Random.Range(0.0f, 1.0f);
        //Debug.Log(percent);

        if (controladorIA.healthBase >= 500 && controladorIA.currentCivilsPlayer > 8 && percent < 0.1f && controladorIA.currentSoldiers > 10)
        {
            
            //Debug.Log("Entra");
            attackBasePlayer = true;
        }
        else
        {
            //Debug.Log("No Entra");
        }

        return attackBasePlayer;
    }

    private bool CheckCondition8()
    {
        bool attackBase = false;
        
        if (controladorIA.healthBase > 250 && controladorIA.currentCivils > 3)
        {
            attackBase = true;
        }

        return attackBase;
    }

    private bool CheckCondition9()
    {
        bool attack = false;
        
        if (controladorIA.healthBase > 500 && controladorIA.food > 50 && controladorIA.currentCivils > 4)
        {
            attack = true;
            
        }

        return attack;
    }

    private void PerformAction1()
    {
        Debug.Log("Buscar recursos");

        foreach (Unit unit in civils)
        {
            unit.civilOcupat = true;
            if (unit.name.Contains("Villager"))
            {
                Resource resource = GetClosestResource(unit);
                //foreach (Resource resource in controladorIA.recursosMapa)
                //{
                    if (controladorIA.currentCivils < 4)
                    {

                        if (resource.resourceOcu == false && unit.civilOcupat == true)
                        {
                            if (unit.GetComponent<iaVillager>().resourceSelect == null)
                            {
                                if (resource == null)
                                {
                                    resource.resourceOcu = false;
                                    unit.GetComponent<iaVillager>().OrderIdle();
                                    unit.civilOcupat = false;
                                    break;
                                }
                                else
                                {
                                    resource.resourceOcu = true;
                                    unit.GetComponent<iaVillager>().OrderGathering(resource);
                                    unit.civilOcupat = false;
                                    break;
                                }

                            }

                        }

                    }
                    else
                    {
                        if (controladorIA.ajuntamentRecursos.food < controladorIA.maxFood && resource.id.Equals("Bush"))
                        {

                            if (resource.resourceOcu == false && unit.civilOcupat == true)
                            {
                                if (unit.GetComponent<iaVillager>().resourceSelect == null)
                                {
                                    if (resource == null)
                                    {
                                        resource.resourceOcu = false;
                                        unit.GetComponent<iaVillager>().OrderIdle();
                                        unit.civilOcupat = false;
                                        break;
                                    }
                                    else
                                    {
                                        resource.resourceOcu = true;
                                        unit.GetComponent<iaVillager>().OrderGathering(resource);
                                        unit.civilOcupat = false;
                                        break;
                                    }

                                }

                            }

                        }
                        else if (controladorIA.ajuntamentRecursos.rock < controladorIA.maxStone && resource.id.Equals("Rock"))
                        {
                            if (resource.resourceOcu == false && unit.civilOcupat == true)
                            {
                                if (unit.GetComponent<iaVillager>().resourceSelect == null)
                                {
                                    resource.resourceOcu = true;
                                    if (resource == null)
                                    {
                                        resource.resourceOcu = false;
                                        unit.GetComponent<iaVillager>().OrderIdle();
                                        unit.civilOcupat = false;
                                        break;
                                    }
                                    else
                                    {
                                        unit.GetComponent<iaVillager>().OrderGathering(resource);
                                        unit.civilOcupat = false;
                                        break;
                                    }

                                }

                            }
                        }
                    }

                //}
            }
            if(controladorIA.timeGameSimulator.currentTime < 30f)
            {
                Debug.Log("More villagers");
                pas5seconds();
                //break;
            }
            else
            {
                //break;
            }
            
        }

        // Control dels recursos Menjar
        float foodNow = controladorIA.food;
        float foodOverfloat = foodNow;
        if (foodOverfloat >= controladorIA.maxFood)
        {
            controladorIA.food = controladorIA.maxFood;
        }
        else
        {
            controladorIA.food = controladorIA.ajuntamentRecursos.food;
        }

        // Control dels recursos Pedra
        float stoneNow = controladorIA.stone;
        float stoneOverfloat = stoneNow;
        if (stoneOverfloat >= controladorIA.maxStone)
        {
            controladorIA.stone = controladorIA.maxStone;
        }
        else
        {
            controladorIA.stone = controladorIA.ajuntamentRecursos.rock;
        }
        
    }

    public void pas5seconds()
    {
        StartCoroutine(twoVillagerResource());
    }

    public IEnumerator twoVillagerResource()
    {
        yield return new WaitForSeconds(5f);
    }

    // Buscar el recurs més proper
    
    public Resource GetClosestResource(Unit unitSelect)
    {
        Resource resourceSelect = null;
        float antPos = 0;
        foreach (Resource resource in controladorIA.recursosMapa)
        {
            
            if (resource != null)
            {
                if(controladorIA.currentCivils < 4)
                {
                    if (controladorIA.ajuntamentRecursos.food < controladorIA.maxFood && resource.id.Equals("Bush"))
                    {

                        if (antPos == 0)
                        {
                            antPos = Vector3.Distance(resource.transform.position, unitSelect.transform.position);
                            resourceSelect = resource;
                            //Debug.Log("Primera posició: " + antPos);
                        }
                        else if (antPos > Vector3.Distance(resource.transform.position, unitSelect.transform.position))
                        {
                            antPos = Vector3.Distance(resource.transform.position, unitSelect.transform.position);
                            resourceSelect = resource;
                            //Debug.Log("Darreres posicions: " + antPos + " " + resourceSelect.name);
                        }

                    }
                }
                else
                {
                    if (controladorIA.ajuntamentRecursos.food < controladorIA.maxFood && resource.id.Equals("Bush"))
                    {

                        if (antPos == 0)
                        {
                            antPos = Vector3.Distance(resource.transform.position, unitSelect.transform.position);
                            resourceSelect = resource;
                            //Debug.Log("Primera posició: " + antPos);
                        }
                        else if (antPos > Vector3.Distance(resource.transform.position, unitSelect.transform.position))
                        {
                            antPos = Vector3.Distance(resource.transform.position, unitSelect.transform.position);
                            resourceSelect = resource;
                            //Debug.Log("Darreres posicions: " + antPos + " " + resourceSelect.name);
                        }

                    }
                    else if (controladorIA.ajuntamentRecursos.rock < controladorIA.maxStone && resource.id.Equals("Rock"))
                    {
                        if (antPos == 0)
                        {
                            antPos = Vector3.Distance(resource.transform.position, unitSelect.transform.position);
                            resourceSelect = resource;
                            //Debug.Log("Primera posició: " + antPos);
                        }
                        else if (antPos > Vector3.Distance(resource.transform.position, unitSelect.transform.position))
                        {
                            antPos = Vector3.Distance(resource.transform.position, unitSelect.transform.position);
                            resourceSelect = resource;
                            //Debug.Log("Darreres posicions: " + antPos + " " + resourceSelect.name);
                        }
                    }
                }
                
            }
            
        }
        Debug.Log("Recurs més aprop: " + resourceSelect);
        return resourceSelect;
    }
    
    private void PerformAction2()
    {
        Debug.Log("Spawned civil");
        if (controladorIA.ajuntamentRecursos.food >= 25)
        {
            controladorIA.ajuntament.GetComponent<Townhall>().SpawnVillager();

            controladorIA.ajuntamentRecursos.food -= 0;
            controladorIA.currentCivils += 1;
            controladorIA.food = controladorIA.ajuntamentRecursos.food;
        }
    }

    private void PerformAction3()
    {
        Debug.Log("Necessito fusta");
        float woodNow = controladorIA.wood;
        float woodOverfloat = woodNow += 100;
        if(woodOverfloat >= controladorIA.maxWood)
        {
            controladorIA.wood = controladorIA.maxWood;
        }
        else
        {
            controladorIA.wood += 100;
        }
        
    }

    private void PerformAction4()
    {
        
    }

    private void PerformAction5()
    {
        Debug.Log("Necessito ferro");
        float ironNow = controladorIA.iron;
        float ironOverfloat = ironNow += 100;
        if (ironOverfloat >= controladorIA.maxIron)
        {
            controladorIA.iron = controladorIA.maxIron;
        }
        else
        {
            controladorIA.iron += 50;
        }
        
    }

    private void PerformAction6()
    {
        Debug.Log("Spawned soldier");
        if (controladorIA.ajuntamentRecursos.food >= 50 && controladorIA.ajuntamentRecursos.rock >= 100)
        {
            controladorIA.ajuntamentRecursos.food -= 50;
            controladorIA.ajuntamentRecursos.rock -= 100;
            controladorIA.currentSoldiers += 1;
            controladorIA.food = controladorIA.ajuntamentRecursos.food;
            controladorIA.stone = controladorIA.ajuntamentRecursos.rock;
        }
    }

    private void PerformAction7()
    {
        Debug.Log("Atacar Base de Player");
    }

    private void PerformAction8()
    {

    }

    private void PerformAction9()
    {

    }

}
