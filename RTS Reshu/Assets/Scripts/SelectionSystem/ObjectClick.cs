using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
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
                    Selections.Instance.ShiftClickSelect(hit.collider.gameObject.GetComponent<Unit>());
                }
                else
                {
                    Selections.Instance.SelectClick(hit.collider.gameObject.GetComponent<Unit>());
                }
            }

            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    Selections.Instance.DeselectAll();
                }
                
            }

        }
    }
}
