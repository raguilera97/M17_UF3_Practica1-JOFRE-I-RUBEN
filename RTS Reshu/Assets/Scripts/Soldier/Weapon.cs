using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    iaSoldier soldierIa;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Warrior"))
        {
            other.gameObject.GetComponent<iaSoldier>().TakeDmg(soldierIa.attackDmg);
        }
    }
}
