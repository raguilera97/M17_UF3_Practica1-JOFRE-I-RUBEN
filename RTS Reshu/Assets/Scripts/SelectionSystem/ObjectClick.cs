using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClick : MonoBehaviour
{
    private Camera mainCamera;

    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask UI;
    
    
   
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
                if (hit.collider.gameObject.tag.Equals("Building"))
                {
                    Selections.Instance.BuildingClick(hit.collider.gameObject.GetComponent<Townhall>());
                }
                else if (hit.collider.gameObject.tag.Equals("Resource"))
                {
                    Selections.Instance.ResourceClick(hit.collider.gameObject.GetComponent<Resource>());
                }
                else
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
                
            }

            else if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    Selections.Instance.DeselectAll();
                }
            }
            
        }
    }
}
