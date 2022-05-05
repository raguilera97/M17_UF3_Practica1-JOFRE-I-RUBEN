using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Almacen : MonoBehaviour, ISaveable
{
    public int rock;
    public int food;

    public Text foodT, rockT;
    
    private void Update()
    {
        if (!this.gameObject.name.Contains("Enemy"))
        {
            foodT.text = food.ToString();
            rockT.text = rock.ToString();
        }
        

    }

    internal void FillAlmacen(int[] backpack)
    {
        rock = rock + backpack[0];
        food = food + backpack[1];
    }

    [Serializable]
    private class AlmacenData
    {
        public int rock;
        public int food;

    }

    public object CaptureState()
    {
        AlmacenData almacenData = new AlmacenData();
        almacenData.rock = rock;
        almacenData.food = food;

        return almacenData;
    }

    public void RestoreState(object data)
    {
        var almacenData = (AlmacenData)data;
        rock = almacenData.rock;
        food = almacenData.food;
    }
}
