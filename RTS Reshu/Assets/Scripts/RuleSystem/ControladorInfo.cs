using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorInfo : MonoBehaviour
{

    // Recursos de IA
    public float healthBase = 500;
    public float wood = 100;
    public float food = 0;
    public float stone = 0;
    public float iron = 50;
    

    // Recursos Limits de IA
    public float maxHealthBase = 1000;
    public float maxWood = 500;
    public float maxStone = 500;
    public float maxIron = 250;
    public float maxFood = 200;

    // Civils de IA
    private List<bool> civilsSpawned = new List<bool>();
    
    //public int civils = 0;
    public int currentCivils = 0;
    public int maxCivils = 20;

    // Soldats de IA
    private List<bool> soldiersSpawned = new List<bool>();

    //public int civils = 0;
    public int currentSoldiers = 0;
    public int maxSoldiers = 40;

    // Civils de Player
    public int currentCivilsPlayer = 10;

    // Component Civils i Soldats
    public Selections unitats;
    public float civilss = 0;
    public List<Unit> civils = new List<Unit>();

    // Punts d'atac
    public Transform puntAttack;

    // Components de elements de recursos en el mapa
    public List<Resource> recursosMapa = new List<Resource>();

    // Component del ajuntament i Tavern
    public Townhall ajuntament;
    public Almacen ajuntamentRecursos;
    public Tavern tavernEnemy;

    // Component del temps de joc
    public TimeGameSimulator timeGameSimulator;

    private void Awake()
    {
        
    }

    private void Start()
    {
        civilsSpawned.Add(true);
        civilsSpawned.Add(false);
        civilsSpawned.Add(true);
        civilsSpawned.Add(false);
        soldiersSpawned.Add(false);
        soldiersSpawned.Add(true);
        soldiersSpawned.Add(false);
        soldiersSpawned.Add(true);
        soldiersSpawned.Add(false);
        soldiersSpawned.Add(true);
        soldiersSpawned.Add(true);
        soldiersSpawned.Add(false);
        soldiersSpawned.Add(false);
        soldiersSpawned.Add(true);
        UpdateCivils();
        UpdateSoldiers();
        AssignGUIDUnits();
        AssignGUIDResource();

        civils = unitats.enemyUnitList;
        
        //ajuntament.GetComponent<PersistentGameObject>().id = Guid.NewGuid().ToString();
    }

    private void Update()
    {
        //civilss = civils.Count;
    }

    private void UpdateCivils()
    {
        //civils = civilsSpawned.Count;
        //Debug.Log("Civils spwaned in terrain: " + civils);
        for(int i = 0; i < civilsSpawned.Count; i++)
        {
            if (civilsSpawned[i] == true)
            {
                currentCivils++;
            }
        }

        //Debug.Log("Civils spwaned in terrain P: " + civils2);
        //civils2 = 0;
    }

    private void UpdateSoldiers()
    {
        for (int i = 0; i < soldiersSpawned.Count; i++)
        {
            if (soldiersSpawned[i] == true)
            {
                currentSoldiers++;
            }
        }
    }

    private void AssignGUIDUnits()
    {
        foreach (Unit unit in civils)
        {
            Debug.Log("Unitat: " + unit.name);
            unit.GetComponent<PersistentGameObject>().id = Guid.NewGuid().ToString();
        }

    }

    private void AssignGUIDResource()
    {
        foreach (Resource resource in recursosMapa)
        {
            resource.GetComponent<PersistentGameObject>().id = Guid.NewGuid().ToString();
        }
        
    }

}
