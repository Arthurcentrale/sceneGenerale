using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoPrincipal_Deplacement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public Animator animator;

    Vector3 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.z);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        if ((Agriculture.playerAbleToMove) && (!DialogUI.panelTouched))
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        DialogUI.panelTouched = false;
    }
}
