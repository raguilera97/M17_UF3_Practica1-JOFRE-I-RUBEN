using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierControler : MonoBehaviour
{
   
    public Vector3 lookAtTargetPosition;

    NavMeshAgent agent;
    Animator anim;
    Vector2 smothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    
    private bool walking;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updatePosition = false;

    }

    // Update is called once per frame
    void Update()
    {
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

        if (Time.deltaTime > 1e-5f)
        {
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
}
