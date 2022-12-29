using UnityEngine;

public class RunStates : MovementBaseState
{
    public override void EnterState(PlayerController movement)
    {
        movement.anim.SetBool("Running",true);
    }

    public override void UpdateState(PlayerController movement)
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Walking);
        }
        else if (movement.dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }
        if (movement.vInput < 0)
        {
            movement.currentMoveSpeed = movement.runBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.runSpeed;
        }
    }
    void ExitState(PlayerController movement, MovementBaseState state)
    {
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
