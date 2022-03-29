using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera mainCamera;

    public LayerMask clickable;
    public LayerMask ground;
   
    void Start()
    {
        mainCamera = Camera.main;
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.ShiftClickSelect(hit.collider.gameObject.GetComponent<Unit>());
                }
                else
                {
                    UnitSelection.Instance.SelectClick(hit.collider.gameObject.GetComponent<Unit>());
                }
            }

            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.DeselectAll();
                }
                
            }

        }
    }
}
