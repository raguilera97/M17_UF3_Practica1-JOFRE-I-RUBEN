using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almacen : MonoBehaviour
{
    public int rock;
    public int food;

    internal void FillAlmacen(int[] backpack)
    {
        rock = rock + backpack[0];
        food = food + backpack[1];
    }
}
