using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iaSoldier : MonoBehaviour
{
    public Transform target;
    public float movementSpeed = 10.0f;
    public float pursueDistance = 10.0f;
    public float attackDistance = 2.0f;
    public float attackCooldown = 2.0f;

    public NavMeshAgent agent;

    iStateWarrior currentState = new IdleSoldierState();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //target = GameObject.FindWithTag("Player").GetComponent<Transform>();
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
}
