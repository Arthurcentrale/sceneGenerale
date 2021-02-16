using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player_script : MonoBehaviour
{
    /*
    NavMeshAgent agent;
    public float PlayerSpeed = 50f;
    public float PlayerAcceleration = 1f;
    public Camera mainCamera;
    */

    public static bool TouchScreenInput = false;

    /*
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

    }

    void Update()
    {

        if ((VerifyTouch()) && (Agriculture.playerAbleToMove) && (!DialogUI.panelTouched)) //si le joueur n'est pas en train d'interagir avec un champ ou une boite de dialogue
        {
            Touch touch = ImportTouch();
            TouchToMovement(touch);
            Agriculture.playerAbleToMove = false;
        }

        DialogUI.panelTouched = false;
    }
    */

    public static bool VerifyTouch()
    {
        if (TouchScreenInput) 
        {
            return (Input.touchCount > 0);
        }
        else
        {
            return (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0));
        }
    }

    public static Touch ImportTouch()
    {
        if (TouchScreenInput)
        {
            Debug.Log("Touch");
            return Input.touches[0];
        }
        else
        {
            //Debug.Log("Click");
            Touch touch = new Touch();
            touch.position = Input.mousePosition;

            //Debug.Log("accéleration : " + Input.acceleration.ToString());

            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("Ended");
                touch.phase = TouchPhase.Ended;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Began");
                touch.phase = TouchPhase.Began;
            }
            else
            {
                //Debug.Log("Stationary");
                touch.phase = TouchPhase.Stationary;
            }
            return touch;
        }
    }

    /*
    public void TouchToMovement(Touch touch) //Amène le joueur là où il à touché l'écran
    {
        //on récupère la position en pixel du toucher de l'utilisateur sur l'écran
        Vector2 touchPixelPosition = touch.position;

        //on transforme cette position en rayon perpendiculaire au plan de la camera
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(touchPixelPosition.x, touchPixelPosition.y, 0f));
        RaycastHit hit;
        Vector3 newTargetPos = new Vector3();
        //si ce rayon rencontre un obstable on récupère la position de l'impact et le jouer ce déplace vers cette position
        if (Physics.Raycast(ray, out hit))
        {
            newTargetPos = hit.point;

            // mouvement automatiques avec un NavMeshAgent
            agent.SetDestination(newTargetPos);
            agent.speed = PlayerSpeed;
            agent.acceleration = PlayerAcceleration;
        }
    }
    
    public void KeyboardToMovement()
    {
        //Vector3 mouvement = newTargetPos - transform.position;
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * PlayerSpeed;
        Vector3 moveAmount = velocity * Time.deltaTime;
        transform.position += moveAmount;
    }
    */
}
