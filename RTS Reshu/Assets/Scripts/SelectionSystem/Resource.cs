using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    [SerializeField] SelectableObject ringSelector;
    
    public string id;
    public int resource = 200;

    bool itsSelected = false;

    private void Update()
    {
        if(resource == 0)
        {
            Destroy(this);
        }
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
