using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Almacen : MonoBehaviour
{
    public int rock;
    public int food;

    public Text foodT, rockT;
    
    private void Update()
    {
        foodT.text = food.ToString();
        rockT.text = rock.ToString();

    }

    internal void FillAlmacen(int[] backpack)
    {
        rock = rock + backpack[0];
        food = food + backpack[1];
    }
}
