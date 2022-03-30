using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour, ISaveable
{

    public GameObject pickaxe;
    public GameObject hammer;
    public Vector3 lookAtTargetPosition;
    public Almacen almacen;

    NavMeshAgent agent;
    Animator anim;
    RaycastHit hitInfo = new RaycastHit();
    Vector2 smothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    iaVillager iaRecolect;

    public int backpackSpace = 20;
    public int[] backpack = new int[] {0,0};
    private bool walking;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        iaRecolect = GetComponent<iaVillager>();
        agent.updatePosition = false;
        almacen = FindObjectOfType<Almacen>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (GetComponent<Unit>().ItsSelected())
        {
            ClickToMove();
        }
        AnimationController();
    }

    

    private void AnimationController()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smoth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smothDeltaPosition = Vector2.Lerp(smothDeltaPosition, deltaPosition, smoth);

        if (Time.deltaTime > 1e-5f){
            velocity = smothDeltaPosition / Time.deltaTime;
        }

        walking = velocity.magnitude > 0.5 && agent.remainingDistance > agent.radius;

        anim.SetBool("itsRunning", walking);
        anim.SetFloat("X", velocity.x);
        anim.SetFloat("Z", velocity.y);

        this.transform.LookAt(agent.steeringTarget + transform.forward);

    }

    

    void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }

    private void ClickToMove()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            iaRecolect.setMining(false);
            iaRecolect.setGathering(false);
            iaRecolect.setgoAlmacen(false);
            SetGatheringAnimation(false);
            SetMiningAnimation(false);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.destination = hitInfo.point;
            }
        }
    }

    public void SetGatheringAnimation(bool SP)
    {
        anim.SetBool("itsGathering", SP);
    }

    public void SetMiningAnimation(bool SP)
    {
        anim.SetBool("itsMining", SP);
    }

    public void TakePickaxe()
    {
        pickaxe.SetActive(true);
        hammer.SetActive(false);
    }

    public void TakeHammer()
    {
        pickaxe.SetActive(false);
        hammer.SetActive(true);
    }

    public void TakeNothing()
    {
        pickaxe.SetActive(false);
        hammer.SetActive(false);
    }

    [Serializable]
    private class CivilData
    {
        public float[] position = new float[3];

    }

    public object CaptureState()
    {
        CivilData civil = new CivilData();

        civil.position[0] = transform.position.x;
        civil.position[1] = transform.position.y;
        civil.position[2] = transform.position.z;

        return civil;
    }

    public void RestoreState(object data)
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        var civilData = (CivilData)data;
        
        Vector3 position;
        position.x = civilData.position[0];
        position.y = civilData.position[1];
        position.z = civilData.position[2];
        transform.position = position;

        gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }
}
