using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStates : MovementBaseState
{
    public override void EnterState(PlayerController movement)
    {
        
    }

    public override void UpdateState(PlayerController movement)
    {
        if (movement.dir.magnitude>0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.SwitchState(movement.Running);
            }
            else
            {
                movement.SwitchState(movement.Walking);
            }
        }
        
    }
}
