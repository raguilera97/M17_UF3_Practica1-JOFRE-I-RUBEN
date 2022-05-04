using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface iStateWarrior
{
    iStateWarrior OnUpdate(iaSoldier war);
    
    void OnEnterState(iaSoldier war);

    void OnExitState(iaSoldier war);
}
