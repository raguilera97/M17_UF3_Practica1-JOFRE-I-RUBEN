using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iStateWarrior
{
    void OnEnterState(iaSoldier character);

    iStateWarrior OnUpdate(iaSoldier character);

    void OnExitState(iaSoldier character);
}
