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
                GameObject hitObject = hit.collider.gameObject;
                
                if (hitObject.tag.Equals("Building") && !hitObject.name.Contains("Enemy"))
                {
                    if (hitObject.name.Contains("townhall"))
                    {
                        Selections.Instance.TownhallClick(hit.collider.gameObject.GetComponent<Townhall>());
                    }
                    else
                    {
                        Selections.Instance.TavernClick(hit.collider.gameObject.GetComponent<Tavern>());
                    }
                }
                else if (hitObject.tag.Equals("Resource"))
                {
                    Selections.Instance.ResourceClick(hitObject.GetComponent<Resource>());
                }
                else
                {
                    if (!hitObject.name.Contains("Enemy"))
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            Selections.Instance.ShiftClickSelect(hitObject.GetComponent<Unit>());
                        }
                        else
                        {
                            Selections.Instance.SelectClick(hitObject.GetComponent<Unit>());
                        }
                    }
                   
                }

                if(hitObject.name.Contains("Warrior") && hitObject.name.Contains("Enemy"))
                {
                    Selections.Instance.CombatClick(hitObject.GetComponent<Unit>());
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
