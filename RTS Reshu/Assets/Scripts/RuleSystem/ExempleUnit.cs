using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExempleUnit : MonoBehaviour
{
    private delegate string GetName();
    private delegate bool OnAgentSpawned(string agentName, float health);

    private GetName getAgentName;
    private OnAgentSpawned onEnemySpawed;

    public float healthBase = 500;
    public float wood = 100;
    public float stone = 50;
    public float iron = 10;
    public float food = 200;

    private List<bool> civilsSpawned = new List<bool>();
    public int civils = 0;
    public int civils2 = 0;

    private void Awake()
    {
        getAgentName = GetAgentName;
        onEnemySpawed = OnEnemySpawned;
    }

    private void Start()
    {
        getAgentName();
        civilsSpawned.Add(true);
        civilsSpawned.Add(false);
        civilsSpawned.Add(true);
        civilsSpawned.Add(false);
        UpdateCivils();
    }

    private void Update()
    {
        
    }

    private void UpdateCivils()
    {
        civils = civilsSpawned.Count;
        //Debug.Log("Civils spwaned in terrain: " + civils);
        for(int i = 0; i < civilsSpawned.Count; i++)
        {
            if (civilsSpawned[i] == true)
            {
                civils2++;
            }
        }

        //Debug.Log("Civils spwaned in terrain P: " + civils2);
        //civils2 = 0;
    }

    private string GetAgentName()
    {
        return null;
    }

    private bool OnEnemySpawned(string agentName, float health)
    {
        return false;
    }
}
