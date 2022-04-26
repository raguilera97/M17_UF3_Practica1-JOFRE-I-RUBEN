using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    [SerializeField] SelectableObject ringSelector;
    
    public string id;
    public int resource = 200;
    public bool resourceOcu = false;

    bool itsSelected = false;

    private void Update()
    {
        if(resource < 1)
        {
            Destroy(this.gameObject);
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
