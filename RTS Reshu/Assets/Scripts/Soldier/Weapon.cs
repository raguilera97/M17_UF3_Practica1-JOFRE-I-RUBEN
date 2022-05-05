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

            Debug.Log("hit");

            soldierIa = other.gameObject.GetComponent<iaSoldier>();
            soldierIa.TakeDmg(soldierIa.attackDmg);
        }
    }
}
