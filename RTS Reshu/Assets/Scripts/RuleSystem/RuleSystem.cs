using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleSystem : MonoBehaviour
{
    private float timeSinceLastEvaluation;
    private float evaluationRate = 5.0f;
    public ExempleUnit exempleUnit;

    private delegate bool CheckCondition();
    private delegate void PerformAction();

    List<CheckCondition> conditions = new List<CheckCondition>();
    List<PerformAction> actions = new List<PerformAction>();

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
        conditions.Add(CheckCondition10);
        conditions.Add(CheckCondition11);
        conditions.Add(CheckCondition12);

        actions.Add(PerformAction1);
        actions.Add(PerformAction2);
        actions.Add(PerformAction3);
        actions.Add(PerformAction4);
        actions.Add(PerformAction5);
        actions.Add(PerformAction6);
        actions.Add(PerformAction7);
        actions.Add(PerformAction8);
        actions.Add(PerformAction9);
        actions.Add(PerformAction10);
        actions.Add(PerformAction11);
        actions.Add(PerformAction12);
    }

    private void Start()
    {
        float healthNow = exempleUnit.healthBase;
        float woodNow = exempleUnit.wood;
        float stoneNow = exempleUnit.stone;
        float ironNow = exempleUnit.iron;
        float foodNow = exempleUnit.food;
        float civilsNow = exempleUnit.currentCivils;
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

    // Obligació d'obtenir recurs Menjar
    private bool CheckCondition1()
    {
        bool huntedMeat = false;

        if (exempleUnit.food < exempleUnit.maxFood && exempleUnit.currentCivils < exempleUnit.maxCivils && exempleUnit.currentCivils != 0)
        {
            huntedMeat = true;
        }

        return huntedMeat;
    }

    // Creació de nous civils
    private bool CheckCondition2()
    {
        bool needVillagers = false;

        if (exempleUnit.food > 50 && exempleUnit.currentCivils < exempleUnit.maxCivils)
        {
            needVillagers = true;
        }

        return needVillagers;
    }

    // Obligació d'obtenir recurs Fusta
    private bool CheckCondition3()
    {
        bool toolWood = false;

        if (exempleUnit.wood < exempleUnit.maxWood && exempleUnit.currentCivils >= 4 && exempleUnit.currentCivils != 0)
        {
            toolWood = true;
        }

        return toolWood;
    }

    // Obligació d'obtenir recurs Pedra
    private bool CheckCondition4()
    {
        bool pickStone = false;

        if (exempleUnit.stone < exempleUnit.maxStone && exempleUnit.currentCivils >= 8 && exempleUnit.currentCivils != 0)
        {
            pickStone = true;
        }

        return pickStone;
    }

    // Obligació d'obtenir recurs Ferro
    private bool CheckCondition5()
    {
        bool pickIron = false;

        if (exempleUnit.iron < exempleUnit.maxIron && exempleUnit.currentCivils > 10 && exempleUnit.currentCivils != 0)
        {
            pickIron = true;
        }

        return pickIron;
    }

    // Atacar a la base del player
    private bool CheckCondition6()
    {
        bool attackBasePlayer = false;

        float percent = UnityEngine.Random.Range(0.0f, 1.0f);
        Debug.Log(percent);

        if (exempleUnit.healthBase >= 500 && exempleUnit.currentCivilsPlayer > 8 && percent < 0.1)
        {
            
            Debug.Log("Entra");
            attackBasePlayer = true;
        }
        else
        {
            Debug.Log("No Entra");
        }

        return attackBasePlayer;
    }

    private bool CheckCondition7()
    {
        bool attackBase = false;
        
        if (exempleUnit.healthBase > 250 && exempleUnit.currentCivils > 3)
        {
            attackBase = true;
        }

        return attackBase;
    }

    private bool CheckCondition8()
    {
        bool attack = false;
        
        if (exempleUnit.healthBase > 500 && exempleUnit.food > 50 && exempleUnit.currentCivils > 4)
        {
            attack = true;
            
        }

        return attack;
    }

    private bool CheckCondition9()
    {
        return false;
    }

    private bool CheckCondition10()
    {
        return false;
    }

    private bool CheckCondition11()
    {
        return false;
    }

    private bool CheckCondition12()
    {
        return false;
    }

    private void PerformAction1()
    {
        Debug.Log("Necessito caçar");
    }

    private void PerformAction2()
    {
        Debug.Log("Spawned civil");
    }

    private void PerformAction3()
    {
        Debug.Log("Necessito fusta");
    }

    private void PerformAction4()
    {
        Debug.Log("Necessito pedra");
    }

    private void PerformAction5()
    {
        Debug.Log("Necessito ferro");
    }

    private void PerformAction6()
    {
        Debug.Log("Atacar Base de Player");
    }

    private void PerformAction7()
    {

    }

    private void PerformAction8()
    {

    }

    private void PerformAction9()
    {

    }

    private void PerformAction10()
    {

    }

    private void PerformAction11()
    {

    }

    private void PerformAction12()
    {

    }

}
