using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingBlocked : MonoBehaviour
{
    // Este script tienen que llevarlo las blueprints para poder bloquear la construccion si ya hay algun edificio colocado.
    public bool blocked = false;

    public void OnCollisionEnter(Collision otherBuilding)
    {
        if (otherBuilding.gameObject.tag == "Building")
        {
            blocked = true;
        }
    }
    public void OnCollisionExit(Collision otherBuilding)
    {
        if (otherBuilding.gameObject.tag == "Building")
        {
            blocked = false;
        }
    }
}
