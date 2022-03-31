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

        actions.Add(PerformAction1);
        actions.Add(PerformAction2);
        actions.Add(PerformAction3);
        actions.Add(PerformAction4);
        actions.Add(PerformAction5);
        actions.Add(PerformAction6);
    }

    private void Start()
    {
        float healthNow = exempleUnit.healthBase;
        float woodNow = exempleUnit.wood;
        float stoneNow = exempleUnit.stone;
        float ironNow = exempleUnit.iron;
        float foodNow = exempleUnit.food;
        float civilsNow = exempleUnit.civils2;
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
                return;
            }
        }
    }

    private bool CheckCondition1()
    {
        bool attack = false;
        
        if (exempleUnit.healthBase > 500 && exempleUnit.food > 50 && exempleUnit.civils2 > 4)
        {
            attack = true;
            
        }

        return attack;
    }

    private bool CheckCondition2()
    {
        bool attackBase = false;
        
        if (exempleUnit.healthBase > 250 && exempleUnit.civils2 > 3)
        {
            attackBase = true;
        }

        return attackBase;
    }

    private bool CheckCondition3()
    {
        bool dance = false;

        if (exempleUnit.healthBase > 300 && exempleUnit.civils2 > 2)
        {
            dance = true;
        }

        return dance;
    }

    private bool CheckCondition4()
    {
        bool huntedMeat = false;

        if (exempleUnit.food < 50 && exempleUnit.civils2 < 2)
        {
            huntedMeat = true;
        }

        return huntedMeat;
    }

    private bool CheckCondition5()
    {
        return false;
    }

    private bool CheckCondition6()
    {
        return false;
    }

    private void PerformAction1()
    {
        Debug.Log("Emotional damage");
    }

    private void PerformAction2()
    {
        Debug.Log("A l'attacker, no puedooor");
    }

    private void PerformAction3()
    {
        Debug.Log("Dale tu cuerpo alegria macarena he macarena");
    }

    private void PerformAction4()
    {
        Debug.Log("Necessito caçar");
    }

    private void PerformAction5()
    {

    }

    private void PerformAction6()
    {

    }

}
