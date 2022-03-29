using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleSystem : MonoBehaviour
{
    private float timeSinceLastEvaluation;
    private float evaluationRate = 5.0f;
    private ExempleUnit exempleUnit;

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

        actions.Add(PerformAction1);
        actions.Add(PerformAction2);
        actions.Add(PerformAction3);
        actions.Add(PerformAction4);
    }

    private void Start()
    {
        exempleUnit = GetComponent<ExempleUnit>();
        float healthNow = exempleUnit.healtBase;
        Debug.Log(healthNow);
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
        
        if (exempleUnit.healtBase > 250 && exempleUnit.food > 50)
        {
            attack = true;
            
        }

        return attack;
    }

    private bool CheckCondition2()
    {
        bool attackBase = false;
        
        if (exempleUnit.healtBase < 100)
        {
            attackBase = true;
        }

        return attackBase;
    }

    private bool CheckCondition3()
    {
        return false;
    }

    private bool CheckCondition4()
    {
        return false;
    }

    private void PerformAction1()
    {
        Debug.Log("Pots attacar");
    }

    private void PerformAction2()
    {

    }

    private void PerformAction3()
    {

    }

    private void PerformAction4()
    {

    }

}
