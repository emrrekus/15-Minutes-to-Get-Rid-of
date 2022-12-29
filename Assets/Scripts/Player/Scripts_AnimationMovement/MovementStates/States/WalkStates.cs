using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStates : MovementBaseState
{
    public override void EnterState(PlayerController movement)
    {
        movement.anim.SetBool("Walking",true);
    }

    public override void UpdateState(PlayerController movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Running);
        }
        else if (movement.dir.magnitude <0.1f)
        {
            ExitState(movement, movement.Idle);
        }
        if (movement.vInput<0)
        {
            movement.currentMoveSpeed = movement.walkBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.walkSpeed;
        }
    }
    void ExitState(PlayerController movement,MovementBaseState state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
