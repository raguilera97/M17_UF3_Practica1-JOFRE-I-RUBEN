using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iaSoldier : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float pursueDistance = 10.0f;
    public float attackDistance = 2.0f;
    public float attackCooldown = 0;

    public int attackDmg;
    public bool itsMoving = false;

    public int maxHealth = 100;
    public int currentHealth;

    public NavMeshAgent agent;

    public Unit unitToAttack;
    public SoldierControler soldier;

    public GameObject sword;

    iStateWarrior currentState = new IdleSoldierState();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        soldier = GetComponent<SoldierControler>();

    }

    void Start()
    {
        if(this.gameObject.name.Contains("SwordWarrior"))
        {
            currentHealth = maxHealth;
            maxHealth = 100;
            attackDmg = 40;
        }else if (this.gameObject.name.Contains("ShieldWarrior"))
        {
            currentHealth = maxHealth;
            maxHealth = 300;
            attackDmg = 15;
        }
    }

    void Update()
    {

       
        iStateWarrior nextState = currentState.OnUpdate(this);

        if (nextState != null)
        {
            currentState.OnExitState(this);
            currentState = nextState;
            currentState.OnEnterState(this);
        }
    }

    public void ChangeState(iStateWarrior state)
    {
        currentState.OnExitState(this);
        currentState = state;
        currentState.OnEnterState(this);
    }

    public void OrderIdle()
    {
        iStateWarrior newState = new IdleSoldierState();
        ChangeState(newState);
    }

    public void OrderAttack(Unit unit)
    {
        unitToAttack = unit;
        iStateWarrior newState = new PursueSoldierState();
        ChangeState(newState);
    }

    public void TakeDmg(int dmg)
    {
        if (currentHealth > 0 && currentHealth - dmg > 0)
        {
            currentHealth -= dmg;
            soldier.HurtAnimation();
        }
        else
        {
            Dead();
        }
    }

    public void ActivarColl()
    {
        Debug.Log("mi puta estampa");
        sword.GetComponent<BoxCollider>().enabled = true;
    }

    public void DesactivarColl()
    {
        sword.GetComponent<BoxCollider>().enabled = false;
    }

    public void Dead()
    {
        
        soldier.DeathAnimation();
        
    }
    
    public void DestroyPost()
    {
        Destroy(this.gameObject);

    }
}
