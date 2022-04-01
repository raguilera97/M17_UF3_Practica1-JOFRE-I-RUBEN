using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface iState
{
    iState OnUpdate(iaVillager ia);
    void OnEnterState(iaVillager ia);
    void OnExitState(iaVillager ia);
}
