using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    [SerializeField] SelectableObject ringSelector;

    bool itsSelected = false;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void Selected()
    {
        ringSelector.TurnOnSelector();
        itsSelected = true;

    }

    public void Unselected()
    {
        ringSelector.TurnOffSelector();
        itsSelected = false;

    }
}
