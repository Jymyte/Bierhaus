using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM.Controllers;

public class MyCharacterController : BaseAgentController
{
    private bool isBusy = false;

    protected override void HandleInput() {
        if (!isBusy) {
            if (Input.GetKeyDown(KeyCode.P))
                pause = !pause;

            crouch = Input.GetKey(KeyCode.C);

            // Handle mouse input

            if (!Input.GetButton("Fire2"))
                return;

            // If mouse right click,
            // found click position in the world

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (!Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundMask.value))
                return;

            // Set agent destination to ground hit point

            agent.SetDestination(hitInfo.point);
        }
    }

    protected override void Animate()
    {
        if (animator == null)
            return;

        // Compute move vector length in local space
        var move = transform.InverseTransformDirection(moveDirection).magnitude;
        bool moving = move > float.Epsilon ? true : false;

        // Update the animator parameters

        animator.SetBool("isWalking", moving);
    }

    public void setIsBusy(bool temp) {
        isBusy = temp;
    }

    public bool getIsBusy() {
        return isBusy;
    }
}
