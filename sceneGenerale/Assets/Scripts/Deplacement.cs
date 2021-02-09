using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Deplacement : MonoBehaviour
{
    public Animator animator;

    private NavMeshAgent agent;
   
    // Start is called before the first frame update
    void Start() {

        agent = GetComponent<NavMeshAgent> ();
        
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit))
            {
                Vector3 newTargetPos = hit.point;
                agent.SetDestination(newTargetPos);
            }
            //animator.SetFloat("Horizontal", newTargetPos.x);
            //animator.SetFloat("Vertical", newTargetPos.z);
            //animator.SetFloat("Speed", newTargetPos.sqrMagnitude);
        }
        if (Input.touchCount > 0)
        {
            RaycastHit hit2;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit2))
            {
                Vector3 newTargetPos = hit2.point;
                agent.SetDestination(newTargetPos);
            }
        }


    }
}
