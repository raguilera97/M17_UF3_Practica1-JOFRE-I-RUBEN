using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlacement : MonoBehaviour
{
    //Variables construcción bàsicos.
    public GameObject ObjToMove;
    public GameObject ObjToPlace;
    public LayerMask mask;
    public int LastPosX, LastPosZ;
    public float LastPosY;
    public Vector3 mousepos;
    public Renderer rend;
    [SerializeField] GameObject grid;
    
    //bool condicionar construcción.
    public bool isBuilding = false;

    //Variables BuildingMenu construir una casa.
    [SerializeField] GameObject casaToMove;
    [SerializeField] GameObject housePrefab;

    //Variables BuildingMenu construir una taverna.
    [SerializeField] GameObject tavernToMove;
    [SerializeField] GameObject tavernPrefab;

   Almacen almacen;
    int rock;

    // mensaje de error de unitHUD
    public GameObject errMess;

    void Start()
    {
        almacen = FindObjectOfType<Almacen>();
        rock = almacen.rock;
    }
    public void BuildHouse()
    {
        if(rock >= 0)
        {
            ObjToPlace = housePrefab;
            ObjToMove = casaToMove;
            rend = GameObject.Find("Zuelo").GetComponent<Renderer>();
            isBuilding = true;
            grid.SetActive(true);
            casaToMove.SetActive(true);
        }
        else
        {
            errMess.SetActive(true);
            StartCoroutine("messageDis");
        }
        
    }

    public void BuildTavern()
    {
        if (rock >= 0)
        {
            ObjToPlace = tavernPrefab;
            ObjToMove = tavernToMove;

            rend = GameObject.Find("Zuelo").GetComponent<Renderer>();
            isBuilding = true;
            grid.SetActive(true);
            tavernToMove.SetActive(true);
        }
        else
        {
            errMess.SetActive(true);
            StartCoroutine("messageDis");
        }

    }


    void Update()
    {
        if(isBuilding == true)
        {
            mousepos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousepos);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                int PosX = (int)Mathf.Round(hit.point.x);
                int PosZ = (int)Mathf.Round(hit.point.z);

                if(PosX != LastPosX || PosZ != LastPosZ)
                {
                    LastPosX = PosX;
                    LastPosZ = PosZ;
                    ObjToMove.transform.position = new Vector3(PosX, LastPosY, PosZ);
                    
                }

                if(Input.GetMouseButtonDown(0))
                {
                    Instantiate(ObjToPlace, ObjToMove.transform.position, ObjToPlace.transform.rotation);
                    grid.SetActive(false);
                    casaToMove.SetActive(false);
                    isBuilding = false;

                }

            }
        }
    }

    IEnumerator messageDis()
    {
        yield return new WaitForSeconds(1f);
        errMess.SetActive(false);
    }
}


/*If your prefabs are located in the Resources Folder, then you can load your prefab in that ways.. it has been tested and works.

C#Version

GameObject testPrefab = (GameObject)Resources.Load("/Prefabs/yourPrefab");

 ________________________________________________________________________________________________________________________________________


 The path should be relative to the Assets/Resources folder. I have only got a success with this:

 GroundTile = (GameObject) Resources.Load("Prefabs/$$anonymous$$apTiles/GroundTile", typeof(GameObject));
__________________________________________________________________________________________________________________________________________

 I tried this in several ways but i could only make it work this way:

GameObject codeInstantiatedPrefab = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/myPrefab.prefab", typeof(GameObject)) ) as GameObject;*/