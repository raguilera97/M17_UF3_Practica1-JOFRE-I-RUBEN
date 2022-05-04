using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, ISaveable
{

    [SerializeField] SelectableObject ringSelector;
    
    public string id;
    public int resource = 200;
    public bool resourceOcu = false;

    private bool itsSelected = false;

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

    [Serializable]
    private class ResourceData
    {
        public float[] position = new float[3];
        public int resourceAmmo;
        public bool resourceOccupied;

    }

    public object CaptureState()
    {
        ResourceData resourceData = new ResourceData();
        resourceData.position[0] = transform.position.x;
        resourceData.position[1] = transform.position.y;
        resourceData.position[2] = transform.position.z;
        resourceData.resourceAmmo = resource;
        resourceData.resourceOccupied = false;

        return resourceData;
    }

    public void RestoreState(object data)
    {
        var resourceData = (ResourceData)data;

        Vector3 position;
        position.x = resourceData.position[0];
        position.y = resourceData.position[1];
        position.z = resourceData.position[2];
        resource = resourceData.resourceAmmo;
        resourceOcu = resourceData.resourceOccupied;
    }
}
